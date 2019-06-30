using Microsoft.Bot.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;

namespace FluentExtension
{
    public class TurnEventArgs : EventArgs
    {
        public ITurnContext TurnContext { get; protected internal set; }

        public CancellationToken CancellationToken { get; protected internal set; }

        public BotSessionStorage Storage { get; protected internal set; }

        public DialogContext DialogContext { get; protected internal set; }

        public IConversationUpdateActivity ConversationUpdateActivit { get; set; }
    }
}
