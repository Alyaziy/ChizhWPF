using ChizhWPF.API;
using ChizhWPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChizhWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public UserDTO User { get; set; }
        public string NameUser { get; set; }
        public string Password { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void SignIn(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = await Client.Instance.UserLogin(User, NameUser, Password);
                TrainPage trainPage = new TrainPage();
                trainPage.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
