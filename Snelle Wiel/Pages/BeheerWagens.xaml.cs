﻿using Snelle_Wiel.Classes;
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

namespace Snelle_Wiel.Pages
{
    /// <summary>
    /// Interaction logic for BeheerWagens.xaml
    /// </summary>
    public partial class BeheerWagens : Page
    {
        Database db;
        public BeheerWagens(Database database)
        {
            InitializeComponent();
            this.db = database;
        }
    }
}
