using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace FluentExtension
{
    public abstract class ActivityBuilder
    {
        public ActivityBuilder(TypeBindBot bot)
        {
            this.Owner = bot;
        }
        public TypeBindBot Owner { get; protected set; }

        public abstract object Instance { get; }

        public string ActivityType { get; protected set; }

        public ActivityBuilderCollection Children { get; set; } = new ActivityBuilderCollection();

        public TypedActivityBuilder<T> ChainConversationUpdate<T>()
        {
            var builder = new TypedActivityBuilder<T>(Owner);
            builder.ActivityType = ActivityTypes.ConversationUpdate;
            Children.Add(builder);
            return builder;
        }

        public ActionInvokerCollection Actions { get; set; } = new ActionInvokerCollection();

        public virtual async Task DoAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            foreach (var each in Actions)
            {
                if (turnContext.Activity.Type == ActivityType)
                {
                    // ConversationUpdate 
                    if (ActivityType == ActivityTypes.ConversationUpdate)
                    {
                        var conversationUpdateActivity = turnContext.Activity.AsConversationUpdateActivity();
                        if (conversationUpdateActivity.MembersAdded.Any(member => member.Id != conversationUpdateActivity.Recipient.Id))
                        {
                            await each.DoAsync(turnContext, cancellationToken);
                        }
                    }                    
                }
            }
            foreach (var each in Children)
            {
                await each.DoAsync(turnContext, cancellationToken);
            }
        }
    }
}
