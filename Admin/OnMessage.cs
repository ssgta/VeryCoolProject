using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VeryCoolBot.Admin
{
    public class OnMessage : ModuleBase<SocketCommandContext>

    {
        private static DiscordSocketClient _client;

        public OnMessage(DiscordSocketClient client)
        {
            _client = client;
        }

        public async Task MessageContainsAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            await BannedWordsCheck(context);
        }

        public async Task UpdatedMessageContainsAsync(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            var messageAfter = after as SocketUserMessage;
            var context = new SocketCommandContext(_client, messageAfter);
            await BannedWordsCheck(context);
        }

        private async Task BannedWordsCheck(SocketCommandContext context)
        {
            try
            {
                if (context.Message.Content != null)
                {
                    if (await BannedWords.CheckForBannedWords(context.Message.Content)) await BannedWords.PunishBannedWord(context);
                }

            }
            catch (Exception ex)
            {
                // do nothing;
            }
        }



    }
}