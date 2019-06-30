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

namespace FluentExtension
{
    public static class IConversationUpdateActivityExtension
    {
        public static bool HasNewMember(this IConversationUpdateActivity activity)
        {
            return activity.MembersAdded.Any(member => member.Id != activity.Recipient.Id);
        }
    }
}
