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
    public class SimpleShellBot : TypeBindBot
    {
        public SimpleShellBot(BotSessionStorage storage)
            : this(storage, null)
        {
        }

        public SimpleShellBot(BotSessionStorage storage, IRecognizer recognizer)
            : base(storage, recognizer)
        {
            var builder = new BotBuilder(this);
            builder.TypedInstance = builder;

            Builder = builder.TypedInstance;
        }

        public override void Build(ActivityBuilder activityBuilder) => Storage.BuildMethod(activityBuilder);      
    }
}
