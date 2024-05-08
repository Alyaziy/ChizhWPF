using ChizhWPF.API;
using ChizhWPF.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window, INotifyPropertyChanged
    {
        public UserDTO userDTO = new UserDTO();
        public UserDTO UserDTO { get => userDTO; set => userDTO = value; }

        public Register()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private async void SaveClose(object sender, RoutedEventArgs e)
        {
            await Client.Instance.RegisterAsync(new UserDTO
            {
                Name = userDTO.Name,
                Password = userDTO.Password,
                Weight = userDTO.Weight,
                Height = userDTO.Height,
            });
            new MainWindow().Show();
            Close();
        }
    }
}
