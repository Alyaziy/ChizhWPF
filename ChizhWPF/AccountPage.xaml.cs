﻿using ChizhWPF.API;
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
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Window, INotifyPropertyChanged
    {
        public UserDTO User { get; set; }
        //public User user { get; set; }

        public AccountPage(UserDTO User)
        {
            InitializeComponent();
            this.User = User;
            DisplayUserInfo();

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private async void DisplayUserInfo()
        {
            if (User != null)
            {
                textUserName.Text = User.Name;
                textHeight.Text = User.Height;
                textWeight.Text = User.Weight;
            }
            else
            {
                textUserName.Text = "Юзер не найден";
                textHeight.Text = "Юзер не найден";
                textWeight.Text = "Юзер не найден";
            }
            Signal(nameof(User));
        }
    }
}
