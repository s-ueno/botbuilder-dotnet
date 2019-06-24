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
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Input;
using Microsoft.Bot.Builder.Expressions.Parser;

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
            //var rootDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
            //{
            //    // Create a LUIS recognizer.
            //    // The recognizer is built using the intents, utterances, patterns and entities defined in ./RootDialog.lu file
            //    Recognizer = CreateRecognizer(),
            //    Generator = new TemplateEngineLanguageGenerator(_lgEngine),
            //    Rules = new List<IRule>()
            //    {
            //        new IntentRule()
            //        {
            //            Intent = "Greeting",
            //            Steps = new List<IDialog>()
            //            {
            //                new BeginDialog(nameof(Child1))
            //                {
            //                    Options = new {
            //                        Caller = "RootDialog"
            //                    }
            //                },
            //                new SendActivity()
            //                {
            //                    Activity = new ActivityTemplate("Hello {user.name}!! This is a greeting from root dialog.")
            //                }
            //            }
            //        },
            //        new IntentRule()
            //        {
            //            Intent = "Cancel",
            //            Steps = new List<IDialog>()
            //            {
            //                new SendActivity("Cancelling ... "),
            //                new CancelAllDialogs()
            //            }
            //        }
            //    }
            //};

            var rootDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
            {
                Generator = new TemplateEngineLanguageGenerator(),
                Steps = new List<IDialog>()
                {
                    new IfCondition()
                    {
                        Condition = new ExpressionEngine().Parse("user.name != null"),
                        Steps = new List<IDialog>()
                        {
                            new SendActivity("Hello, {user.name}")
                        }
                    },
                    new TextInput()
                    {
                        Prompt = new ActivityTemplate("What is your name?"),
                        Property = "user.name",
                    },
                    new SendActivity()
                    {
                        Activity = new ActivityTemplate("Hello {user.name}, nice to meet you!")
                    }
                }
            };

            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(rootDialog);
            AddDialog(new Child1());

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
