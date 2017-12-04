using Snelle_Wiel;
using Snelle_Wiel.Classes;
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
        Database db = new Database();
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtLogin_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Home(db));
        }


        bool tbenter = true;

        private void Mouse_enter(object sender, RoutedEventArgs e)
        {
            if (tbenter == true)
            {
                tbenter = false;
                TbName.Text = "";
            }
        }

        bool pbenter = true;
        private void PbPass_MouseEnter(object sender, MouseEventArgs e)
        {
            if (pbenter == true)
            {
                pbenter = false;
                PbPass.Password = "";
            }
        }

        private void wachtwoordvergeten_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Test");
        }
    }
}
