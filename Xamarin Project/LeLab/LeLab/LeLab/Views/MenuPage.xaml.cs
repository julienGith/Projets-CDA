using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LeLab.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new CountPage());
        }
        private void TabsButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new TabsPage());
        }

        private void ListButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new ListPage());
        }

        private void JeuButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new JeuPenduPage());
        }

        private void JeuNombreButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new NavigationPage(new WelcomeJeuNombrePage()));
        }
    }
}