using Snelle_Wiel.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Snelle_Wiel.Classes
{
    class Navigator
    {
        BeheerChauffeur Beheerchauffeur;
        BeheerFacturatie Beheerfacturatie;
        BeheerKlanten Beheerklanten;
        BeheerWagens Beheerwagens;
        BeheerApplicatie Beheerapplicatie;
        BeheerPlanning Beheerplanning;
        Home home;
        Database db;

        public Navigator(Home givenhome, Database database)
        {
            this.home = givenhome;
            this.db = database;
            this.Beheerchauffeur = new BeheerChauffeur(this.db);
            this.Beheerfacturatie = new BeheerFacturatie(this.db);
            this.Beheerklanten = new BeheerKlanten(this.db);
            this.Beheerwagens = new BeheerWagens(this.db);
            this.Beheerapplicatie = new BeheerApplicatie(this.db);
            this.Beheerplanning = new BeheerPlanning(this.db);
        }

        public void NavigateTo(string Location)
        {
            switch(Location)
            {
                case "Planning":
                    home.FNavigateframe.Navigate(this.Beheerplanning);
                    break;
                case "Chauffeurs":
                    home.FNavigateframe.Navigate(this.Beheerchauffeur);
                    this.Beheerchauffeur.Setup();
                    break;
                case "Facturatie":
                    home.FNavigateframe.Navigate(this.Beheerfacturatie);
                        break;
                case "Klant":
                    home.FNavigateframe.Navigate(this.Beheerklanten);
                    break;
                case "Wagens":
                    home.FNavigateframe.Navigate(this.Beheerwagens);
                    break;
                case "Applicatie":
                    home.FNavigateframe.Navigate(this.Beheerapplicatie);
                    break;
                default:
                    home.FNavigateframe.Source = null;
                    break;

            }
        }

        //private void PageSlide(Page topage)
        //{
        //    var a = new DoubleAnimation
        //    {
        //        From = -700,
        //        To = topage.ActualWidth,
        //        FillBehavior = FillBehavior.Stop,
        //        Duration = new Duration(TimeSpan.FromSeconds(0.2))
        //    };
        //    var storyboard = new Storyboard();

        //    storyboard.Children.Add(a);
        //    Storyboard.SetTarget(a, topage);
        //    Storyboard.SetTargetProperty(a, new PropertyPath(Frame.));
        //    storyboard.Completed += delegate { this.home.Visibility = Visibility.Hidden; home.FNavigateframe.Navigate(topage); };
        //    storyboard.Begin();
        //}
    }
}
