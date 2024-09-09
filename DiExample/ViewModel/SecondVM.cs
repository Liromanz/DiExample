using DiExample._Services.Interfaces;
using DiExample.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiExample.ViewModel
{
    internal class SecondVM : NotifyHelper
    {
        #region Свойства
        private readonly IMessageService _messageService;


        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                Notify();
            }
        }
        #endregion


        #region Команды

        #endregion


        public SecondVM(IMessageService messageService)
        {
            _messageService = messageService;
            LoadMessage();
        }


        #region Методы
        private void LoadMessage()
        {
            Message = _messageService.GetMessage();
        }
        #endregion
    }
}
