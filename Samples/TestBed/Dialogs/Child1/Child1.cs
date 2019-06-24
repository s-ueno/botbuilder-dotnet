using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Adaptive;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Input;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Steps;

namespace Microsoft.BotBuilderSamples
{
    public class Child1 : ComponentDialog
    {
        public Child1()
            : base(nameof(Child1))
        {
            var childDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
            {
                Steps = new List<IDialog>() {
                    new SendActivity()
                    {
                        Activity = new ActivityTemplate("Options passed in .. {dialog.options}")
                    },
                    new SendActivity()
                    {
                        Activity = new ActivityTemplate("User scope:: {if(equals(user.name, null), 'null', 'user.name')}")
                    },
                    new TextInput()
                    {
                        Prompt = new ActivityTemplate("What is your name?"),
                        Property = "user.name",
                        AlwaysPrompt = false
                    },
                    new SendActivity()
                    {
                        Activity = new ActivityTemplate("Hello, {user.name}, nice to meet you!")
                    }
                }
            };

            AddDialog(childDialog);
            InitialDialogId = nameof(AdaptiveDialog);

        }
    }
}
