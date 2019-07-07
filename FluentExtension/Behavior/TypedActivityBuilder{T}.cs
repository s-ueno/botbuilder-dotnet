using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace FluentExtension
{
    public class TypedActivityBuilder<T> : ActivityBuilder
    {
        public TypedActivityBuilder(TypeBindBot bot)
            : base(bot)
        {
        }

        public T TypedInstance { get; set; }

        public override object Instance
        {
            get
            {
                if (TypedInstance == null)
                {
                    TypedInstance = Activator.CreateInstance<T>();
                }

                return TypedInstance;
            }
        }


        public TypedActivityBuilder<T> TextPrompt(Action<T, TurnEventArgs> func)
        {
            return TextPrompt((x, e) => Task.Run(() => func(x, e)));
        }

        public TypedActivityBuilder<T> TextPrompt(Func<T, TurnEventArgs, Task> func)
        {
            return TextPromptRaw((x, e) => func((T)x, e));
        }

        protected TypedActivityBuilder<T> TextPromptRaw(Func<object, TurnEventArgs, Task> func)
        {
            var invoker = new ActionInvoker(this, func);
            invoker.PromptType = typeof(TextPrompt);
            Actions.Add(invoker);
            return this;
        }
    }
}

