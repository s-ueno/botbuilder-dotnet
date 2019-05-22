﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Adapters;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Xunit;

namespace Microsoft.BotBuilderSamples.Tests.Bots
{
    public class DialogAndWelcomeBotTests
    {
        [Fact]
        public async Task ReturnsWelcomeCardOnConversationUpdate()
        {
            // Arrange
            var mockRootDialog = DialogUtils.CreateMockDialog<Dialog>(expectedResult: null, "mockRootDialog");

            // TODO: do we need state here?
            var memoryStorage = new MemoryStorage();
            var sut = new DialogAndWelcomeBot<Dialog>(new ConversationState(memoryStorage), new UserState(memoryStorage), mockRootDialog.Object, null);
            var conversationUpdateActivity = new Activity
            {
                Type = ActivityTypes.ConversationUpdate,
                MembersAdded = new List<ChannelAccount>
                {
                    new ChannelAccount { Id = "theUser" },
                },
                Recipient = new ChannelAccount { Id = "theBot" },
            };
            var testAdapter = new TestAdapter();

            // Act
            // Note: it is kind of obscure that we need to use OnTurnAsync to trigger OnMembersAdded so we get the card
            await testAdapter.ProcessActivityAsync(conversationUpdateActivity, sut.OnTurnAsync, CancellationToken.None);
            var reply = testAdapter.GetNextReply();

            // Assert
            var m = (IMessageActivity)reply;
            Assert.Equal(1, m.Attachments.Count);
            Assert.Equal("application/vnd.microsoft.card.adaptive", m.Attachments.FirstOrDefault()?.ContentType);
        }
    }
}
