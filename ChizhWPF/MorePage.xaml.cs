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
    /// Логика взаимодействия для MorePage.xaml
    /// </summary>
    public partial class MorePage : Window, INotifyPropertyChanged
    {
        private TrainDTO selectedTrain;

        private ObservableCollection<PozeDTO> selectedPoze;
        public List<PozeDTO> Pozes { get; set; }
        public ObservableCollection<PozeDTO> SelectedPozes
        {
            get => selectedPoze;
            set
            {
                selectedPoze = value;
                Signal();
            }
        }

        private MuscleDTO selectedMuscle;
        public List<MuscleDTO> Muscles { get; set; }
        public MuscleDTO SelectedMuscles
        {
            get => selectedMuscle;
            set
            {
                selectedMuscle = value;
                Signal();
            }
        }

        public TrainDTO SelectedTrain
        {
            get => selectedTrain;
            set
            {
                selectedTrain = value;
            }
        }

        public MorePage(TrainDTO train)
        {
            InitializeComponent();
            SelectedTrain = train;
            LoadPoze();
            //LoadMuscle();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        

        private async Task LoadPoze()
        {
            var client = new Client();
            Pozes = await client.GetPoze();
            SelectedPozes = new ObservableCollection<PozeDTO>(SelectedTrain.Pozes);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pozes)));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new AdmTrainPage().Show();
            Close();
        }
    }
}
