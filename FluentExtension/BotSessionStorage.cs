using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace FluentExtension
{
    public class BotSessionStorage
    {
        public BotSessionStorage(
            UserState userState,
            ConversationState conversationState)
        {
            UserState = userState;
            ConversationState = conversationState;
        }

        /// <summary>
        /// Show the current state of the user and the Bot application.
        /// </summary>
        public virtual UserState UserState { get; }

        /// <summary>
        /// Indicates the current conversation continuation status with the user.
        /// </summary>
        public virtual ConversationState ConversationState { get; }
        /// <summary>
        /// Get <see cref="ConversationState"/> about dialog
        /// </summary>
        public virtual IStatePropertyAccessor<DialogState> ConversationDialogState
        {
            get
            {
                if (_conversationDialogState == null)
                {
                    _conversationDialogState = this.ConversationState.CreateProperty<DialogState>(nameof(DialogState));
                }
                return _conversationDialogState;
            }
        }
        private IStatePropertyAccessor<DialogState> _conversationDialogState;


    }
}
