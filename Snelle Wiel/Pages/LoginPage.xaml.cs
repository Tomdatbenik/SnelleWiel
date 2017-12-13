using Snelle_Wiel;
using Snelle_Wiel.Classes;
using Snelle_Wiel.Objects;
using Snelle_Wiel.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnelleWiel.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        Database db = new Database();
        cLogin lg;
        public LoginPage()
        {
            InitializeComponent();
            lg = new cLogin(this.db);
        }

        public void BtLogin_Click(object sender, RoutedEventArgs e)
        {
            User u = lg.Login(TbName.Text, PbPass.Password);
            //TextBlockHide(TbError);
            if (u.Id != 0)
            {
                db.Userid = u.Id;
                pagefade(this);
            }
            else
            {
                TbError.Visibility = System.Windows.Visibility.Visible;
            }
        }

        bool tbenter = true;

        private void Mouse_enter(object sender, RoutedEventArgs e)
        {
            if (tbenter == true)
            {
                tbenter = false;
                TbName.Text = "";
                PbPass.Password = "";
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

        private void pagefade(Page page)
        {
           var a = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                FillBehavior = FillBehavior.Stop,
                Duration = new Duration(TimeSpan.FromSeconds(0.2))
            };
        var storyboard = new Storyboard();

        storyboard.Children.Add(a);
        Storyboard.SetTarget(a, page);
        Storyboard.SetTargetProperty(a, new PropertyPath(OpacityProperty));
        storyboard.Completed += delegate { this.Visibility = Visibility.Hidden; this.NavigationService.Navigate(new Home(db)); };
        storyboard.Begin();
        }

        public void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtLogin_Click(this, new RoutedEventArgs());
            }
        }
    }
}
