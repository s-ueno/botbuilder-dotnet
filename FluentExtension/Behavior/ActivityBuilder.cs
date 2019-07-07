using System;
using System.Collections.Generic;
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
        protected TypeBindBot Owner { get; set; }

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
                    await each.DoAsync(turnContext, cancellationToken);
                }
            }
            foreach (var each in Children)
            {
                await each.DoAsync(turnContext, cancellationToken);
            }
        }
    }
}
