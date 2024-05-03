using ChizhWPF.API;
using ChizhWPF.DTO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Логика взаимодействия для EditPoze.xaml
    /// </summary>
    public partial class EditPoze : Window, INotifyPropertyChanged
    {
        private PozeDTO selectedPoze = new PozeDTO();
        public AdminDTO Admin { get; set; }

        private MuscleDTO selectedMuscle;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

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

        public PozeDTO SelectedPoze
        {
            get => selectedPoze;
            set
            {
                selectedPoze = value;
                Signal();

            }
        }

        public EditPoze(AdminDTO admin, PozeDTO poze, MuscleDTO selectedMuscle)
        {
            InitializeComponent();
            SelectedPoze = poze;
            SelectedMuscles = selectedMuscle;
            this.Admin = admin;
            LoadMuscles();
            DataContext = this;
        }

        private async Task LoadMuscles()
        {
            var client = new Client();
            Muscles = await client.GetMuscle();
            SelectedMuscles = Muscles.FirstOrDefault(s => s.Id == SelectedPoze.IdMuscle);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Muscles)));
        }

        private void SaveClose(object sender, RoutedEventArgs e)
        {
            if (SelectedPoze == null)
            {
                MessageBox.Show("Не все поля заполнены!!");
                return;
            }
            MuscleDTO muscleGroup = (MuscleDTO)EditMusculeText.SelectedItem;
            SelectedPoze.Tittle = PozeTittle.Text;
            SelectedPoze.Description = Description.Text;
            

            //SelectedPoze.IdMuscle = SelectedPoze.IdMuscle;
            //SelectedPoze.Muscle = SelectedMuscles.MuTittle;
            //Client.Instance.EditPoze(SelectedPoze, SelectedPoze.Id);
            Client.Instance.EditPoze(muscleGroup, SelectedPoze,SelectedPoze.Id);
            Close();
        }

        private void SelectPhoto(object sender, RoutedEventArgs e)
        {
            string dir = Environment.CurrentDirectory + @"\Images\";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Images|*.jpg;";
            if (dlg.ShowDialog() == true)
            {
                var test = new BitmapImage(new Uri(dlg.FileName));
                if (test.PixelWidth > 2000 || test.PixelHeight > 2000)
                {
                    MessageBox.Show("Картинка слишком большая");
                    return;
                }
                string newFile = dir + new FileInfo(dlg.FileName).Name;
                File.Copy(dlg.FileName, newFile, true);
                SelectedPoze.Image = File.ReadAllBytes(newFile);
                Signal("SelectedPoze");
            }
        }
    }
}
