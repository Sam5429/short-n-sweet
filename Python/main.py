from bot import Bot
from bot_logic.bot_runner import BotRunner

URL_REMOTE = "https://jg26.jdis.ca"

if __name__ == "__main__":
    print("Starting JDIS Bot Client...")
    BotRunner.run(URL_REMOTE, Bot.TOKEN)
