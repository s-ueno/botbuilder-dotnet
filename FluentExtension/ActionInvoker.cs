using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace FluentExtension
{
    public class ActionInvoker
    {
        public ActionInvoker(
            ActivityBuilder builder,
            Func<object, TurnEventArgs, Task> func)
        {
            Builder = builder;
            this.Func = func;
            Id = $"{GetType().Name}-{Guid.NewGuid().ToString().Replace("-", "")}";
        }
        public string Id { get; protected set; }

        public Func<object, TurnEventArgs, Task> Func { get; private set; }

        public Type PromptType { get; set; }

        public ActivityBuilder Builder { get; set; }

        public virtual async Task DoAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var e = new TurnEventArgs(Builder.Owner.Storage, turnContext, cancellationToken);

            await Func(Builder.Instance, e);
        }
    }
}
