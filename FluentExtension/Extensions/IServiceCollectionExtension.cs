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
using System.Threading;
using System.Threading.Tasks;

namespace FluentExtension
{
#pragma warning disable

    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddShellBot(
            this IServiceCollection services, 
            Action<ActivityBuilder> build,
            Action<BotFrameworkOptions> configureBotAction = null
            )
        {
            services.AddSingleton<ICredentialProvider, ConfigurationCredentialProvider>();

            services.AddSingleton<IBotFrameworkHttpAdapter, BotFrameworkHttpAdapter>();

            services.AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;

                var userState = options?.State?.OfType<UserState>().FirstOrDefault();
                var conversationState = options?.State?.OfType<ConversationState>().FirstOrDefault();

                return new BotSessionStorage(userState, conversationState, build);
            });

            services.AddBot<SimpleShellBot>(options =>
            {
                if (configureBotAction != null)
                {
                    configureBotAction(options);
                }
                else
                {
                    IStorage dataStore = new MemoryStorage();
                    var userState = new UserState(dataStore);
                    var conversationState = new ConversationState(dataStore);
                    options.State.Add(userState);
                    options.State.Add(conversationState);
                }           
            });         
            return services;
        }
    }
#pragma warning restore
}
