using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Integration;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Configuration;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace FluentExtension
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection DefaultInjection(this IServiceCollection services)
        {
            // Create the credential provider to be used with the Bot Framework Adapter.
            services.AddSingleton<ICredentialProvider, ConfigurationCredentialProvider>();

            // Create the Bot Framework Adapter.
            services.AddSingleton<IBotFrameworkHttpAdapter, BotFrameworkHttpAdapter>();

#pragma warning disable CS0618 

            services.AddSingleton(sp =>
            {
                // AddBot で登録した options を取得。
                var options = sp.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;

                var userState = options?.State?.OfType<UserState>().FirstOrDefault();
                var conversationState = options?.State?.OfType<ConversationState>().FirstOrDefault();

                return new BotSessionStorage(userState, conversationState);
            });

#pragma warning restore CS0618 

            return services;
        }

        public static IServiceCollection AddBuilder<TBot>(
            this IServiceCollection services, 
            Action<BotFrameworkOptions> configureAction = null)
        {
            if (configureAction == null)
            {

            }
            return services;
        }


    }
}
