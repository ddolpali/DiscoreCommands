using Discore;
using Discore.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DiscoreCommands
{
    public class CommandFactory
    {
        private static Dictionary<string, Command> _commands = new Dictionary<string, Command>();

        public static void RegisterCommand(string commandKey, Command cmd)
        {
            _commands.Add(commandKey, cmd);
        }

        public static bool CommandExists(string key)
        {
            if (_commands.ContainsKey(key))
                return true;
            return false;
        }

        public static void ExecuteCommand(string key, MatchCollection content, MessageEventArgs e)
        {
            if (!CommandExists(key))
                return;

            Shard shard = e.Shard;
            ITextChannel Channel = (ITextChannel)shard.Cache.Channels.Get(e.Message.ChannelId);
            if (content.Count < (_commands[key]._args + 1) - (_commands[key]._optionalArgs))
            {
                Channel.CreateMessage("Incorrect number of arguments.");
                return;
            }

            _commands[key].Execute(Channel, e, content);
        }
    }
}
