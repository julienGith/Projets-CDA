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
    public partial class WelcomeJeuNombrePage : ContentPage
    {
        public WelcomeJeuNombrePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            ScaleInfini(JouerNombreButton, 1000);
        }
        
        private async Task ScaleInfini(View view, uint length)
        {
            bool always = true;
            while (always)
            {
                await view.ScaleTo(1.1, length);
                await view.ScaleTo(1.0, length);
                
            }
        }

        private void JouerButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new JeuNombrePage());
        }
    }
}