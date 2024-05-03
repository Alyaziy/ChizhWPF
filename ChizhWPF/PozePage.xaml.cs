using ChizhWPF.API;
using ChizhWPF.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для PozePage.xaml
    /// </summary>
    public partial class PozePage : Window, INotifyPropertyChanged
    {
        public ObservableCollection<PozeDTO> Pozes { get; set; }

        public PozePage()
        {
            InitializeComponent();
            DataContext = this;
            LoadPoze();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private async Task LoadPoze()
        {
            Client client = new Client();
            await LoadPoze(client);
        }

        private async Task LoadPoze(Client client)
        {
            Pozes = new ObservableCollection<PozeDTO>(await client.GetPoze());
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pozes)));
        }

        private void buttonTrain(object sender, RoutedEventArgs e)
        {
            new TrainPage().Show();
            Close();
        }

        private void buttonPoze(object sender, RoutedEventArgs e)
        {
            new PozePage().Show();
            Close();
        }

        private void logOut(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
