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
using FluentExtension.Gallery.Bots;

namespace FluentExtension.Gallery
{
    public class ShellBot : TypeBindBot
    {
        public ShellBot(BotSessionStorage storage, IRecognizer recognizer)
            : base(storage, recognizer)
        {
        }

        public override void Build(ActivityBuilder activityBuilder)
        {
            activityBuilder
                .ConversationUpdate<ConversationUpdate>()
                .Notify(x => x.Welcome)
                .Push<Menu>()
                    .Notify(x => x.Choices)





            activityBuilder
                .MessageWithAttachments<>()




        }
    }
}
