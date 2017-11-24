﻿using Snelle_Wiel;
using Snelle_Wiel.Pages;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnelleWiel.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        MainWindow Main;
        public LoginPage(MainWindow main)
        {
            InitializeComponent();
            this.Main = main;
        }

        private void BtLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Main.Content = new Home();
        }
    }
}