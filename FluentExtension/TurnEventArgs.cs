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
    public class TurnEventArgs : EventArgs, ISendTextActivity
    {
        public TurnEventArgs(BotSessionStorage storage, DialogContext dialogContext, CancellationToken cancellationToken)
            : this(storage, dialogContext?.Context, cancellationToken)
        {
            DialogContext = dialogContext;
        }
        public DialogContext DialogContext { get; set; }

        public TurnEventArgs(BotSessionStorage storage, ITurnContext turnContext, CancellationToken cancellationToken)
        {
            Storage = storage;
            TurnContext = turnContext;
            CancellationToken = cancellationToken;
        }
        public BotSessionStorage Storage { get; protected set; }

        public CancellationToken CancellationToken { get; protected set; }

        public ITurnContext TurnContext { get; protected set; }

        public IActivity Activity
        {
            get
            {
                return TurnContext?.Activity;
            }
        }

        public ChannelAccount Account
        {
            get { return Activity?.Recipient; }
        }

        public string UserName
        {
            get { return Account?.Name; }
        }

        #region ISendTextActivity

        public void SendText(string textActivity)
        {
            SendTextAsync(textActivity).Wait();
        }

        public async Task SendTextAsync(string textActivity)
        {
            await TurnContext.SendActivityAsync(MessageFactory.Text(textActivity), CancellationToken);
        }

        #endregion 





        /*

        protected IList<(Guid id, PromptOptions options)> Steps
            = new List<(Guid id, PromptOptions options)>();

        public void WriteChoices(string message, IEnumerable<string> choices)
        {
            Steps.Add((Guid.NewGuid(), new PromptOptions
            {
                Prompt = MessageFactory.Text(message),
                Choices = ChoiceFactory.ToChoices(choices.ToList()),
            }));
        }
        */
    }
}
