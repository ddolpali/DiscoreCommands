# DiscoreCommands
Very basic command handler for the Discore library. There is probably a better way of doing things; this was just something put together for my bot project.

## TODO
* Add a Command Builder

## Example Usage
The prefix is ! by default. You can change it by doing `CommandParser.SetPrefix(".")`

Program.cs
```csharp
using System;
using System.Threading;
using System.Threading.Tasks;
using Discore;
using Discore.WebSocket;
using DiscoreCommands;
using ExampleBot.Commands.Implementation;

namespace ExampleBot
{
    class Bot
    {
        static void Main(string[] args) => Start().Wait();

        public static async Task Start()
        {
            DiscordBotUserToken token = new DiscordBotUserToken("TOKEN");
            DiscordWebSocketApplication app = new DiscordWebSocketApplication(token);

            Shard shard = app.ShardManager.CreateSingleShard();
            await shard.StartAsync(CancellationToken.None);

            shard.Gateway.OnMessageCreated += CommandParser.ProcessCommand;

            // Commands
            CommandFactory.RegisterCommand("ping", new PingCommand());

            while (shard.IsRunning)
                await Task.Delay(1000);
        }
    }
}
```

Commands/Implementation/PingBot.cs
```csharp
using System.Text.RegularExpressions;
using Discore;
using Discore.WebSocket;
using DiscoreCommands;

namespace ExampleBot.Commands.Implementation
{
    class PingCommand : Command
    {
        public PingCommand() : base()
        {
            Name = "ping";
            Desc = "Checks if bot is alive.";
        }

        public override async void Execute(ITextChannel Channel, MessageEventArgs e, MatchCollection args)
        {
            await Channel.CreateMessage("pong");
        }
    }
}
```
