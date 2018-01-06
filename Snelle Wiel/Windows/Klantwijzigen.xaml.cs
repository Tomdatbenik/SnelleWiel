using Snelle_Wiel.Classes;
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
using System.Windows.Shapes;

namespace Snelle_Wiel.Windows
{
    /// <summary>
    /// Interaction logic for Klantwijzigen.xaml
    /// </summary>
    public partial class Klantwijzigen : Window
    {
        Database db;
        int id;
        public Klantwijzigen(Database database, int Id)
        {
            InitializeComponent();
            this.db = database;
            this.id = Id;
        }
    }
}
