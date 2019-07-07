using Microsoft.Bot.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Builder.Dialogs.Choices;

namespace FluentExtension.Gallery.Bots
{
    // Class design should be Poco
    public class WelcomeBot
    {
        public string Welcome(TurnEventArgs e)
        {
            // hack まだ実装コストが高い
            var activity = e.Activity.AsConversationUpdateActivity();
            if (activity.MembersAdded.Any(member => member.Id != activity.Recipient.Id))
            {
                return $"ようこそ！ {e.UserName}さん";
            }

            return null;
        }

        // 非同期で何かしらからデータ取得してもOK
        // public async Task<string> WelcomeAsync(TurnEventArgs e)
        // {
        //    return await Task.Run(() => $"ようこそ！ {e.UserName}さん");
        // }
    }
}
