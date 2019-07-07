using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluentExtension
{
    public interface ISendTextActivity
    {
        void SendText(string textActivity);
        Task SendTextAsync(string textActivity);
    }
}
