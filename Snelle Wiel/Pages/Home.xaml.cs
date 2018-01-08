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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snelle_Wiel.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        Navigator Nav;
        Database Data;
        public Home(Database database,MainWindow main)
        {
            InitializeComponent();
            this.Data = database;
            this.Nav = new Navigator(this, this.Data,main);
            this.Nav.NavigateTo("Planning");
        }

        public void navigatorbutton_click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            this.Nav.NavigateTo(b.Content.ToString());
        }
    }
}
