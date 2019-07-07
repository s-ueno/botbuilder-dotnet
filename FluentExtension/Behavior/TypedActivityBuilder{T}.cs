using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluentExtension
{
    public class TypedActivityBuilder<T> : ActivityBuilder
    {
        public TypedActivityBuilder(TypeBindBot bot)
            : base(bot)
        {
        }

        // refer <see cref="Microsoft.Extensions.DependencyInjection"/> for instance.
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

        public TypedActivityBuilder<T> TextPrompt(Func<T, string> func)
        {
            return TextPrompt((x, e) => func(x));
        }

        public TypedActivityBuilder<T> TextPrompt(Func<T, TurnEventArgs, string> func)
        {
            return TextPrompt((x, e) => Task.Run(() => func(x, e)));
        }

        public TypedActivityBuilder<T> TextPrompt(Func<T, Task<string>> func)
        {
            return TextPrompt((x, e) => func(x));
        }

        public TypedActivityBuilder<T> TextPrompt(Func<T, TurnEventArgs, Task<string>> func)
        {
            return TextPromptRaw((x, e) => func((T)x, e));
        }

        protected TypedActivityBuilder<T> TextPromptRaw(Func<object, TurnEventArgs, Task<string>> func)
        {
            var invoker = new TextPromptInvoker(this, func);
            Actions.Add(invoker);
            return this;
        }
    }
}

