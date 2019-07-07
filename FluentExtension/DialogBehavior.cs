using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentExtension
{
    public class DialogBehavior : ComponentDialog
    {
        public DialogBehavior(string identity)
            : base(identity)
        {
        }

        public ActionInvokerCollection ActionInvokers { get; set; } = new ActionInvokerCollection();

        public DialogBehaviorCollection DialogBehaviors { get; set; } = new DialogBehaviorCollection();
    }
}
