using Discord;
using Discord.Commands;
using System;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace VeryCoolBot.Modules
{
    public class BotInitialization : ModuleBase<SocketCommandContext>
    {
        private static DiscordSocketClient _client;

        public BotInitialization(DiscordSocketClient client)
        {
            _client = client;
        }

        public static async Task BotInitializationTasks()
        {
            try
            {
                ITextChannel chnl = null;

                while (chnl == null)
                {
                    chnl = _client.GetChannel(368195799210917890) as ITextChannel;
                    if (chnl != null)
                    {
                        await chnl.SendMessageAsync("Very Cool Bot is starting...");
                        await chnl.SendMessageAsync("Ready. Type !commands to see a list of available commands");
                    }

                    Task.Delay(1000).Wait();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to initialize Very Cool Bot {ex.Message}");
                throw ex;
            }

        }

    }

}