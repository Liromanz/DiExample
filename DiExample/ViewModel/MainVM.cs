using DiExample._Services.Interfaces;
using DiExample.View;
using DiExample.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiExample.ViewModel
{
    internal class MainVM : NotifyHelper
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
        public RelayCommand LoadMessageCommand { get; set; }
        #endregion


        public MainVM(IMessageService messageService)
        {
            _messageService = messageService;

            LoadMessageCommand = new RelayCommand(LoadMessage);
        }


        #region Методы
        private void LoadMessage()
        {
            Message = _messageService.GetMessage();
        }

        #endregion
    }
}