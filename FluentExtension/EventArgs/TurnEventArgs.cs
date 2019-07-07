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
    public class TurnEventArgs : EventArgs
    {
        public BotSessionStorage Storage { get; set; }

        public DialogContext DialogContext { get; protected set; }

        public ITurnContext TurnContext { get; set; }

        public IActivity Activity { get; set; }

        public ChannelAccount Account
        {
            get { return Activity?.Recipient; }
        }

        public string UserName
        {
            get { return Account?.Name; }
        }

        /*
        public ITurnContext TurnContext { get; protected internal set; }

        public CancellationToken CancellationToken { get; protected internal set; }

        public BotSessionStorage Storage { get; protected internal set; }

        public IConversationUpdateActivity ConversationUpdateActivit { get; set; }

        public Task WriteTextAsync(string message)
        {
            return TurnContext.SendActivityAsync(
                MessageFactory.Text(message));
        }

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
