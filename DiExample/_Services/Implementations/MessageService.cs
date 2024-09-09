using DiExample._Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiExample._Services.Implementations
{
    internal class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Это сервис сделал!";
        }
    }
}
