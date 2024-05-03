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
    /// Логика взаимодействия для AdmTrainPage.xaml
    /// </summary>
    public partial class AdmTrainPage : Window, INotifyPropertyChanged
    {
        public ObservableCollection<TrainDTO> Trains
        {
            get => trains;
            set { trains = value; Signal(nameof(Trains)); }
        }

        public TrainDTO SelectedTrain { get; set; }
        public AdminDTO Admin { get; set; }

        private ObservableCollection<TrainDTO> trains;

        public AdmTrainPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadTrain();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private void LoadTrain()
        {
            Task.Run(async () =>
            {
                Client client = new Client();
                await LoadTrain(client);
            });
        }

        private async Task LoadTrain(Client client)
        {
            Trains = new ObservableCollection<TrainDTO>(await client.GetTrains());
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Trains)));
        }

        private void buttonPozeAdm(object sender, RoutedEventArgs e)
        {
            new AdmPozePage().Show();
            Close();
        }

        private void buttonTrainAdm(object sender, RoutedEventArgs e)
        {
            new AdmTrainPage().Show();
            Close();
        }

        private void logOut(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private async void AddTrain(object sender, RoutedEventArgs e)
        {
            new AddTrain(Admin).ShowDialog();
            LoadTrain();
        }

        private void EditTrain(object sender, RoutedEventArgs e)
        {
            if (SelectedTrain == null)
            {
                MessageBox.Show("Выберите треню!");
                return;
            }
            new EditTrain(Admin, SelectedTrain).ShowDialog();
            LoadTrain();

        }

        private async void DeleteTrain(object sender, RoutedEventArgs e)
        {
            await Client.Instance.DeleteTrain(SelectedTrain.Id);
            LoadTrain();
        }
    }
}
