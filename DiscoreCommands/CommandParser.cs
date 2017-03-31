﻿using Discore;
using Discore.WebSocket;
using System;
using System.Text.RegularExpressions;

namespace DiscoreCommands
{
    public class CommandParser
    {
        public static string _prefix { get; private set; } = "!";

        public static void ProcessCommand(object sender, MessageEventArgs e)
        {
            string body = e.Message.Content;
            if (!body.StartsWith(_prefix))
                return;

            Regex pattern = new Regex("([\"'])(?:(?=(\\\\?))\\2.)*?\\1|([^\\s]+)");
            var args = pattern.Matches(body);

            Shard shard = e.Shard;
            DiscordMessage message = e.Message;
            ITextChannel textChannel = (ITextChannel)shard.Cache.Channels.Get(message.ChannelId);
            string cmd = args[0].Value.Substring(1, args[0].Value.Length - 1);

            CommandFactory.ExecuteCommand(cmd, args, e);
        }

        public static void SetPrefix(string prefix)
        {
            _prefix = prefix;
        }
    }
}