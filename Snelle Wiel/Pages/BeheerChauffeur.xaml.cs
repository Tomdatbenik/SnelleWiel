﻿using Snelle_Wiel.Classes;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for BeheerChauffeur.xaml
    /// </summary>
    public partial class BeheerChauffeur : Page
    {
        Database db;
        public BeheerChauffeur(Database database)
        {
            InitializeComponent();
            this.db = database;
        }

        public void Setup()
        {
            LvChauffeurs.Items.Clear();
            DataTable dt = db.ExecuteStringQuery("SELECT UserId, RoleId, UNaam, UWoonplaats, UAdres, UPostcode, UEmail, UTelefoon FROM Users WHERE RoleId = 2");
            foreach(DataRow dr in dt.Rows)
            {
                LvChauffeurs.Items.Add(dr["UNaam"].ToString());
            }
        }
    }
}
