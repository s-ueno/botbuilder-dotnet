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
    public abstract class TypeBindBot : ActivityHandler
    {
        public TypeBindBot(BotSessionStorage storage, IRecognizer recognizer)
        {
            this.Storage = storage;
            this.Recognizer = recognizer;
            this._dialogs = new DialogSet(storage?.ConversationDialogState);
        }

        public BotSessionStorage Storage { get; protected set; }

        public IRecognizer Recognizer { get; set; }

        protected DialogSet _dialogs { get; set; }

        public virtual void AddDialog(Dialog dialog)
        {
            this._dialogs.Add(dialog);
        }

        public override Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            if (!_isInitialized)
            {
                Build(new ActivityBuilder(this));
                _isInitialized = true;
            }

            return base.OnTurnAsync(turnContext, cancellationToken);
        }

        private bool _isInitialized = false;

        public abstract void Build(ActivityBuilder activityBuilder);

        protected override Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var files = turnContext.Activity.Attachments;
            if (files != null)
            {
                return OnAttachmentsMessageActivityAsync(turnContext, cancellationToken);
            }
            else
            {
                return OnTextMessageActivityAsync(turnContext, cancellationToken);
            }
        }

        protected override Task OnConversationUpdateActivityAsync(ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            return base.OnConversationUpdateActivityAsync(turnContext, cancellationToken);
        }

        protected virtual Task OnTextMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnAttachmentsMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
