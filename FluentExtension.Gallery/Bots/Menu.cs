using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentExtension.Gallery.Bots
{
    public class Menu
    {
        public async Task Choices(TurnEventArgs e)
        {
            // この実装コストを下げる
            // return await e.DialogContext.PromptAsync(
            //    "choice",
            //    new PromptOptions
            //    {
            //        Prompt = MessageFactory.Text("次のメニューから選んでね！"),
            //        Choices = choices,
            //    },
            //    e.CancellationToken);

            // いや、非同期じゃないとダメ
            // ここは実行
            // await e.WriteChoicesAsync(
            //    "次のメニューから選んでね！",
            //    new[] { string.Empty, string.Empty, string.Empty });
        }
    }
}
