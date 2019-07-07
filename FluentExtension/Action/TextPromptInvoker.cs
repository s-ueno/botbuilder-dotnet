using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace FluentExtension
{
    public class TextPromptInvoker : ActionInvoker
    {
        public TextPromptInvoker(
            ActivityBuilder builder,
            Func<object, TurnEventArgs, Task<string>> func)
            : base(builder)
        {
            Func = func;
        }

        public Func<object, TurnEventArgs, Task<string>> Func { get; private set; }

        public async override Task<object> DoAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            // hack このイベント実装は適当なので見直す
            var e = new TurnEventArgs();
            e.TurnContext = turnContext;
            e.Activity = turnContext.Activity;

            var result = await Func(Builder.Instance, e);

            if (!string.IsNullOrWhiteSpace(result))
            {
                await turnContext.SendActivityAsync(MessageFactory.Text(result), cancellationToken);
            }
            
            return result;
        }
    }
}
