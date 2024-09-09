using DiExample._Services.Implementations;
using DiExample._Services.Interfaces;
using DiExample.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiExample.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMessageService _service;
        public MainWindow(IMessageService messageService)
        {
            _service = messageService;  

            InitializeComponent();
            DataContext = new MainVM(_service);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var startupWindow = App.AppHost!.Services.GetRequiredService<SecondWindow>();
            startupWindow.Show();
        }
    }
}