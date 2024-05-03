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
    /// Логика взаимодействия для TrainPage.xaml
    /// </summary>
    public partial class TrainPage : Window, INotifyPropertyChanged
    {
        public ObservableCollection<TrainDTO> Trains {  get; set; }

        public TrainPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadTrain();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private async Task LoadTrain()
        {
            Client client = new Client();
            await LoadTrain(client);
        }

        private async Task LoadTrain(Client client)
        {
            Trains = new ObservableCollection<TrainDTO>(await client.GetTrains()); 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Trains)));
        }

        private void buttonPoze(object sender, RoutedEventArgs e)
        {
            new PozePage().Show();
            Close();
        }

        private void buttonTrain(object sender, RoutedEventArgs e)
        {
            new TrainPage().Show();
            Close();
        }

        private void logOut(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
