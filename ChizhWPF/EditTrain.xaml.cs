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
    /// Логика взаимодействия для EditTrain.xaml
    /// </summary>
    public partial class EditTrain : Window, INotifyPropertyChanged
    {
        private TrainDTO selectedTrain = new TrainDTO();        
        public AdminDTO Admin { get; set; }
        public List<PozeDTO> Pozes { get; set; }

        private ObservableCollection<PozeDTO> selectedPoze;
        public ObservableCollection<PozeDTO> SelectedPozes
        {
            get => selectedPoze;
            set
            {
                selectedPoze = value;
                Signal();
            }
        }

        public PozeDTO SelectPoAdd { get; set; }
        public PozeDTO SelectPoRemove { get; set; }

        //private MuscleDTO selectedMuscle;
        //public List<MuscleDTO> Muscles { get; set; }
        //public MuscleDTO SelectedMuscles
        //{
        //    get => selectedMuscle;
        //    set
        //    {
        //        selectedMuscle = value;
        //        Signal();
        //    }
        //}

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public TrainDTO SelectedTrain
        {
            get => selectedTrain;
            set
            {
                selectedTrain = value;

            }
        }

        public EditTrain(AdminDTO admin, TrainDTO train)
        {
            InitializeComponent();
            SelectedTrain = train;
            this.Admin = admin;
            //LoadMuscles();
            LoadPozes();
            DataContext = this;
        }

        //private async Task LoadMuscles()
        //{
        //    var client = new Client();
        //    Muscles = await client.GetMuscle();
        //    SelectedMuscles = Muscles.FirstOrDefault(s => s.Id == SelectedTrain.IdMuscle);

        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Muscles)));
        //}

        private async Task LoadPozes()
        {
            var client = new Client();
            Pozes = await client.GetPoze();
            SelectedPozes = new ObservableCollection<PozeDTO>(SelectedTrain.Pozes);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pozes)));
        }

        private void SaveClose(object sender, RoutedEventArgs e)
        {
            if (SelectedTrain == null)
            {
                MessageBox.Show("Не все поля заполнены!!");
                return;
            }
            //SelectedTrain.IdMuscle = SelectedTrain.IdMuscle;
            //SelectedTrain.MuTittle = SelectedTrain.MuTittle;
            SelectedTrain.Pozes = SelectedPozes.ToList();
            Client.Instance.EditTrain(SelectedTrain, SelectedTrain.Id);
            Close();
        }

        private void AddPoClick(object sender, RoutedEventArgs e)
        {
            if (SelectPoAdd != null)
            {
                SelectedPozes.Add(SelectPoAdd);
            }
            else
            {
                MessageBox.Show("Выберите позу");
            }
        }

        private void RemovePoClick(object sender, RoutedEventArgs e)
        {
            if (SelectPoRemove != null)
            {
                SelectedPozes.Remove(SelectPoRemove);
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
