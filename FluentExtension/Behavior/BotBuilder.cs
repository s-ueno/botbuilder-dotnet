using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluentExtension
{
    public class BotBuilder : TypedActivityBuilder<BotBuilder>
    {
        public BotBuilder(TypeBindBot bot)
            : base(bot)
        {
        }
    }
}
