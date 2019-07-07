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
    public class ConversationUpdateEventArgs : TurnEventArgs
    {
        public IConversationUpdateActivity ConversationUpdateActivity { get; set; }

        public CancellationToken CancellationToken { get; set; }

        public bool IsMembersAdded
        {
            get
            {
                return ConversationUpdateActivity.MembersAdded
                    .Any(member => member.Id != ConversationUpdateActivity.Recipient.Id);
            }
        }
    }
}
