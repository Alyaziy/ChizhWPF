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
    /// Логика взаимодействия для AdmPozePage.xaml
    /// </summary>
    public partial class AdmPozePage : Window, INotifyPropertyChanged
    {
        public ObservableCollection<PozeDTO> Pozes
        {
            get => pozes;
            set { pozes = value; Signal(nameof(Pozes)); }
        }

        public PozeDTO SelectedPoze { get; set; }
        public MuscleDTO selectedMuscle { get; set; }
        public AdminDTO Admin { get; set; }
        public List<MuscleDTO> Muscles { get; set; }

        public MuscleDTO SelectedMuscles
        {
            get => selectedMuscle;
            set
            {
                selectedMuscle = value;
                LoadPoze(SelectedMuscles);
            }
        }

        private ObservableCollection<PozeDTO> pozes;

        public AdmPozePage()
        {
            InitializeComponent();
            DataContext = this;
            LoadPoze();
            LoadMuscle();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private void LoadPoze()
        {
            Task.Run(async () =>
            {
                Client client = new Client();
                await LoadPoze(client);
            });
        }

        private async Task LoadPoze(Client client)
        {
            Pozes = new ObservableCollection<PozeDTO>(await client.GetPoze());
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pozes)));
        }

        private async Task LoadPoze(MuscleDTO muscle)
        {
            Client client = new Client();
            if (muscle == null || muscle.Id == 0)
            {
                LoadPoze(client);
                return;
            }
            Pozes = new ObservableCollection<PozeDTO>(await client.GetPoze1(muscle.Id));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pozes)));
        }

        private async Task LoadMuscle()
        {
            try
            {
                var client = new Client();
                Muscles = await client.GetMuscle();
                Muscles.Insert(0, new MuscleDTO { MuTittle = "Все позы" });
                SelectedMuscles = Muscles.First();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Muscles)));
            }
            catch { }
        }

        private void buttonTrainAdm(object sender, RoutedEventArgs e)
        {
            new AdmTrainPage().Show();
            Close();
        }

        private void buttonPozeAdm(object sender, RoutedEventArgs e)
        {
            new AdmPozePage().Show();
            Close();
        }

        private void logOut(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private async void AddPoze(object sender, RoutedEventArgs e)
        {
            new AddPoze(Admin).ShowDialog();
            LoadPoze();
        }

        private void EditPoze(object sender, RoutedEventArgs e)
        {
           
            if (SelectedPoze == null)
            {
                MessageBox.Show("Выберите позу!");
                return;
            }
            new EditPoze(Admin, SelectedPoze).ShowDialog();
            LoadPoze();

        }

        private async void DeletePoze(object sender, RoutedEventArgs e)
        {
            await Client.Instance.DeletePoze(SelectedPoze.Id);
            LoadPoze();
        }
    }
}
