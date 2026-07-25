# ============================== #
#                                #
#  NE PAS MODIFIER CE FICHIER    #
#   DO NOT MODIFY THIS FILE      #
#                                #
# ============================== #

import json
import threading
import time
from typing import Callable

from signalrcore.hub_connection_builder import HubConnectionBuilder

from client.message_protocol import ActionBase, GameState


class GameClient:
    def __init__(self, url: str, token: str):
        self._connection = (
            HubConnectionBuilder()
            .with_url(f"{url}/api/hub?token={token}&type=bot")
            .with_automatic_reconnect(
                {
                    "type": "raw",
                    "keep_alive_interval": 10,
                    "reconnect_interval": 5,
                    "max_attempts": 5,
                }
            )
            .build()
        )
        self._authenticated = threading.Event()
        self._auth_error: Exception | None = None
        self._current_state = GameState()
        self._action_gate = threading.Lock()
        self._last_action_tick_sent = -1
        self._on_update: Callable[[GameState], ActionBase | None] | None = None

        self._connection.on("Authenticated", self._on_authenticated)
        self._connection.on("Error", self._on_error)

    def connect(self) -> bool:
        try:
            self._connection.start()
            print("[INFO] Network connection established, waiting for token validation...")

            if not self._authenticated.wait(timeout=5):
                raise TimeoutError("No authentication response from the server.")

            if self._auth_error is not None:
                raise self._auth_error

            return True
        except Exception as ex:
            print(f"[ERROR] Connection failed: {ex}")
            return False

    def backend_listening(self, on_update: Callable[[GameState], ActionBase | None]) -> None:
        self._on_update = on_update
        self._connection.on("Tick", self._on_tick)
        self._connection.on("ReceiveVisibleMap", self._on_receive_visible_map)
        self._connection.on("ReceivePlayerInfo", self._on_receive_player_info)

    def stop(self) -> None:
        self._connection.stop()

    def _on_authenticated(self, *args) -> None:
        print("[AUTH] Successfully authenticated.")
        self._authenticated.set()

    def _on_error(self, *args) -> None:
        message = self._first_arg(args)
        if not self._authenticated.is_set():
            self._auth_error = Exception(str(message))
            print(f"[AUTH] Authentication error: {message}")
            self._authenticated.set()
            return
        print(f"[SERVER] Error: {message}")

    def _on_tick(self, *args) -> None:
        try:
            data = self._first_arg(args)
            if isinstance(data, dict):
                self._current_state.CurrentTick = int(data.get("tick", self._current_state.CurrentTick))
            self._try_send_action()
        except Exception as ex:
            print(f"[ERROR] Tick: {ex}")

    def _on_receive_visible_map(self, *args) -> None:
        try:
            data = self._first_arg(args)
            if isinstance(data, dict):
                self._current_state.update_vision_from_server(data)
            self._try_send_action()
        except Exception as ex:
            print(f"[ERROR] ReceiveVisibleMap: {ex}")

    def _on_receive_player_info(self, *args) -> None:
        data = self._first_arg(args)
        if isinstance(data, dict):
            self._current_state.update_player(data)

    def _try_send_action(self) -> None:
        if self._on_update is None:
            return

        with self._action_gate:
            self._try_send_action_core()

    def _try_send_action_core(self) -> None:
        if self._current_state.Bot is None:
            print("[BOT] Waiting for bot state initialization...")
            return

        if self._current_state.CurrentTick <= self._last_action_tick_sent:
            return

        action = self._on_update(self._current_state) if self._on_update else None
        if action is None:
            return

        self._last_action_tick_sent = self._current_state.CurrentTick

        envelope = {
            "type": "COMMAND",
            "action": action.to_server_payload(),
            "tick": self._current_state.CurrentTick,
        }

        print("=== [BOT->SERVER] Sending JSON ===")
        print(json.dumps(envelope, indent=2, ensure_ascii=False))
        print("===============================")

        self._connection.send("SubmitAction", [envelope])

    @staticmethod
    def _first_arg(args):
        if len(args) == 1 and isinstance(args[0], list) and len(args[0]) > 0:
            return args[0][0]
        if len(args) > 0:
            return args[0]
        return None


