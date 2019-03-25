using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DiscordBot
{
    class CommandHandler
    {
        //Importing services needed for commands and importing client.
        DiscordSocketClient _client;
        CommandService _service;

        public async Task InitializeAsync(DiscordSocketClient client)
        {
            _client = client;
            _service = new CommandService();

            //Adding modules into service 
            await _service.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
            _client.MessageReceived += HandleCommandAsync;
        }

        //A Task for what happens when a message is recieved.
        private async Task HandleCommandAsync(SocketMessage s)
        {
            //Changing SocketMessage to userMessage.
            var msg = s as SocketUserMessage;
            //If msg is blank return nothing.
            if (msg == null) return;
            //Information reguarding context of pages.
            var context = new SocketCommandContext(_client, msg);
            //Creating arg for reference 
            int argPos = 0;
            //Calling for the bot.
            if(msg.HasStringPrefix(Config.bot.cmdPrefix, ref argPos)
                || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                //Creating result var to log what happens when there is an issue.
                var result =  await _service.ExecuteAsync( context,  argPos, services: null);
                //If a command has an error we will log the problem for later.
                if(!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
}
