using ChizhWPF.API;
using ChizhWPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChizhWPF
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        public AdminDTO Admin { get; set; }
        public string AdmName { get; set; }
        public string AdmPassword { get; set; }

        public AdminPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void SignIn(object sender, RoutedEventArgs e)
        {
            try
            {
                var admin = await Client.Instance.AdminLogin(Admin, AdmName, AdmPassword);
                AdmTrainPage admTrainPage = new AdmTrainPage();
                admTrainPage.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
