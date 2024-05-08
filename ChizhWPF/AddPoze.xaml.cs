using ChizhWPF.API;
using ChizhWPF.DTO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AddPoze.xaml
    /// </summary>
    public partial class AddPoze : Window, INotifyPropertyChanged
    {
        public AdminDTO Admin;
        public PozeDTO pozeDTO = new PozeDTO();
        
        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public PozeDTO PozeDTO { get => pozeDTO; set => pozeDTO = value; }

        public List<MuscleDTO> Muscles { get; set; }
        public MuscleDTO SelectedMuscles { get; set; }

        public AddPoze(AdminDTO admin)
        {
            InitializeComponent();
            this.Admin = admin;
            LoadMuscles();
            DataContext = this;
        }

        private async Task LoadMuscles()
        {
            var client = new Client();
            Muscles = await client.GetMuscle();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Muscles)));
        }

        private async void SaveClose(object sender, RoutedEventArgs e)
        {
            await Client.Instance.AddPozeAsync(new PozeDTO
            {
                Tittle = pozeDTO.Tittle,
                IdMuscle = SelectedMuscles.Id,
                Muscle = SelectedMuscles.MuTittle,
                Description = pozeDTO.Description,
                Time = pozeDTO.Time,
                Image = pozeDTO.Image,
            });
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
                PozeDTO.Image = File.ReadAllBytes(newFile);
                Signal("PozeDTO");
            }
        }
    }
}
