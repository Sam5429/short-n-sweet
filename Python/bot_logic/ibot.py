# ============================== #
#                                #
#  NE PAS MODIFIER CE FICHIER    #
#   DO NOT MODIFY THIS FILE      #
#                                #
# ============================== #

from abc import ABC, abstractmethod

from client.message_protocol import ActionBase, GameState


class IBot(ABC):
    TOKEN = ""

    @abstractmethod
    def get_next_action(self, state: GameState) -> ActionBase | None:
        pass
