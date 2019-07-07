using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace FluentExtension
{
    public abstract class ActionInvoker
    {
        public ActionInvoker(ActivityBuilder builder)
        {
            Builder = builder;           
        }

        public ActivityBuilder Builder { get; set; }

        public abstract Task<object> DoAsync(ITurnContext turnContext, CancellationToken cancellationToken);
    }
}
