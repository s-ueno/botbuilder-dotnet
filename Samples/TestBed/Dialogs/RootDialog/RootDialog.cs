using System;
using System.Collections.Generic;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Adaptive;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Extensions.Configuration;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Rules;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Steps;
using Microsoft.Bot.Builder.LanguageGeneration;
using System.IO;

namespace Microsoft.BotBuilderSamples
{
    public class RootDialog : ComponentDialog
    {
        private static IConfiguration Configuration;
        private TemplateEngine _lgEngine;

        public RootDialog(IConfiguration configuration)
            : base(nameof(RootDialog))
        {
            Configuration = configuration;
            string[] paths = { ".", "Dialogs", "RootDialog", "RootDialog.lg" };
            string fullPath = Path.Combine(paths);
            _lgEngine = new TemplateEngine().AddFile(fullPath);
            // Create instance of adaptive dialog. 
            var rootDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
            {
                // Create a LUIS recognizer.
                // The recognizer is built using the intents, utterances, patterns and entities defined in ./RootDialog.lu file
                Recognizer = CreateRecognizer(),
                Generator = new TemplateEngineLanguageGenerator(_lgEngine),
                Rules = new List<IRule>()
                {
                    new EventRule() {
                        Events = new List<string>() { 
                            AdaptiveEvents.ConversationMembersAdded
                        },
                        Steps = new List<IDialog>() {
                            new SendActivity("ConversationMembersAdded!"),
                            new SendActivity("Event type:: {turn.dialogEvent.name}"),
                            new SendActivity("Event payload:: {turn.dialogEvent}")
                        }
                    },
                    //new EventRule()
                    //{
                    //    Events = new List<string>()
                    //    {
                    //        AdaptiveEvents.BeginDialog
                    //    },
                    //    Steps = new List<IDialog>()
                    //    {
                    //        new SendActivity("BeginDialog!"),
                    //        new SendActivity("Event type [AD]:: {turn.dialogEvent.name}"),
                    //        new SendActivity("Event payload [AD]:: {turn.dialogEvent}")
                    //    }

                    //}
                }
            };

            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(rootDialog);

            // The initial child Dialog to run.
            InitialDialogId = nameof(AdaptiveDialog);
        }

        public static IRecognizer CreateRecognizer()
        {
            if (string.IsNullOrEmpty(Configuration["LuisAppId"]) || string.IsNullOrEmpty(Configuration["LuisAPIKey"]) || string.IsNullOrEmpty(Configuration["LuisAPIHostName"]))
            {
                throw new Exception("Your LUIS application is not configured. Please see README.MD to set up a LUIS application.");
            }
            return new LuisRecognizer(new LuisApplication()
            {
                Endpoint = Configuration["LuisAPIHostName"],
                EndpointKey = Configuration["LuisAPIKey"],
                ApplicationId = Configuration["LuisAppId"]
            });
        }
    }
}
