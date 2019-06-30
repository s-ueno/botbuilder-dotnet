using System;
using System.Collections.Generic;
using System.Text;

namespace FluentExtension
{
    public class ActivityBuilder
    {
        public ActivityBuilder(TypeBindBot bot)
        {
            this.Owner = bot;
        }
        protected TypeBindBot Owner { get; set; }
    }
}
