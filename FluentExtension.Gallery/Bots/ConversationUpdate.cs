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

namespace FluentExtension.Gallery.Bots
{
    // Class design should be Poco
    public class ConversationUpdate
    {
        public async Task Welcome(TurnEventArgs e)
        {
            var turnContext = e.TurnContext;
            var dialogContext = e.DialogContext;
            var activity = e.ConversationUpdateActivit;
            
            if (activity.HasNewMember())
            {
                // とりま、この実装コストを下げるようにする

                // await turnContext.SendActivityAsync(MessageFactory.Text($"ようこそ！"));
                // await dialogContext.BeginDialogAsync(nameof(MenuDialog), null, cancellationToken);
            }
        }


        public async Task NextBuild(ActivityBuilder activityBuilder)
        {
            activityBuilder
                    .TextPrompt()
                    .NumberPrompt()
                    .ConfirmPrompt()
                    .ChoicePrompt()
        }

    }
}
