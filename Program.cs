using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using VeryCoolBot.Modules;
using VeryCoolBot.Admin;

namespace VeryCoolBot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private OnMessage _onMessage;
        private BotInitialization _botInitialization;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _onMessage = new OnMessage(_client);
            _botInitialization = new BotInitialization(_client);

            _services = new ServiceCollection() 
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .AddSingleton(_botInitialization)
                .AddSingleton<ConfigHandler>()
                .BuildServiceProvider(); 

            await _services.GetService<ConfigHandler>().PopulateConfig();
            await _client.LoginAsync(TokenType.Bot, _services.GetService<ConfigHandler>().GetToken());

            _client.MessageUpdated += _onMessage.UpdatedMessageContainsAsync;
            _client.Ready += BotInitialization.BotInitializationTasks;
            _client.Log += Client_Log;

            await RegisterCommandsAsync();
            await _client.StartAsync();
            await Task.Delay(-1); 
        }

        private Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage; 
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return; 

            _ = Task.Run(() => _onMessage.MessageContainsAsync(arg));
            int argPos = 0;
            if (message.HasStringPrefix("!", ref argPos))  
            {
                if (await BannedWords.CheckForBannedWords(message.Content)) await BannedWords.PunishBannedWord(context);
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
                {
                    return;
                }
            }
        }
    }
}