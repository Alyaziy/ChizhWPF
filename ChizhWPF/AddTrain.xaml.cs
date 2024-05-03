using ChizhWPF.API;
using ChizhWPF.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AddTrain.xaml
    /// </summary>
    public partial class AddTrain : Window, INotifyPropertyChanged
    {
        public AdminDTO Admin;
        public TrainDTO trainDTO = new TrainDTO();

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public TrainDTO TrainDTO { get =>  trainDTO; set => trainDTO = value; }

        public List<PozeDTO> Pozes {  get; set; }

        private ObservableCollection<PozeDTO> selectedPoze = new();
        public ObservableCollection<PozeDTO> SelectedPozes
        {
            get => selectedPoze;
            set
            {
                selectedPoze = value;
                Signal();
            }
        }

        public PozeDTO SelectPozeAdd { get; set; }
        public PozeDTO SelectPozeRemove { get; set; }
        
        //public List<MuscleDTO> Muscles { get; set; }
        //public MuscleDTO SelectedMuscles { get; set; }

        public AddTrain(AdminDTO admin)
        {
            InitializeComponent();
            this.Admin = admin;
            //LoadMuscles();
            LoadPozes();
            DataContext = this;
        }

        //private async Task LoadMuscles()
        //{
        //    var client = new Client();
        //    Muscles = await client.GetMuscle();
            
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Muscles)));
        //}

        private async Task LoadPozes()
        {
            var client = new Client();
            Pozes = await client.GetPoze();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pozes)));
        }

        private async void SaveClose(object sender, RoutedEventArgs e)
        {
            TrainDTO.Pozes = SelectedPozes.ToList();
            await Client.Instance.AddTrainAsync(TrainDTO);

            Close();
        }

        private void AddPoClick(object sender, RoutedEventArgs e)
        {
            if (SelectPozeAdd != null)
            {
                SelectedPozes.Add(SelectPozeAdd);
            }
            else
            {
                MessageBox.Show("Выберите позу");
            }
        }

        private void RemovePoClick(object sender, RoutedEventArgs e)
        {
            if (SelectPozeRemove != null)
            {
                SelectedPozes.Remove(SelectPozeRemove);
            }
            else
            {
                MessageBox.Show("Выберите позу");
            }
        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            SelectedPozes.Clear();
        }
    }
}
