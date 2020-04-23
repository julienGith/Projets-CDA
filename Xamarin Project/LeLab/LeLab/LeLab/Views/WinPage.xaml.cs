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
    public partial class WinPage : ContentPage
    {
        //contructeur pour le previewer
        public WinPage() : this(5)
            {

            }
        public WinPage(int nombreMagique)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            mainLayout.Scale = 0.7;
            mainLayout.ScaleTo(1.0, 1500,Easing.BounceIn);
            nombreMagiqueLbl.Text = $"Le nombre magique était {nombreMagique.ToString()}";
            ToWelcomePage();
        }
        private async Task ToWelcomePage()
        {
            await Task.Delay(3000);
            await Navigation.PopToRootAsync();
        }
    }
}