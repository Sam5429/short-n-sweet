from client.message_protocol import *

class Bot:
    # EDIT THIS FOR YOUR OWN BOT TOKEN
    TOKEN = "BOTA-abcd-1234-ABCD"

    def get_next_action(self, state: GameState) -> ActionBase | None:
        return None  # Placeholder for the bot's logic implementation
