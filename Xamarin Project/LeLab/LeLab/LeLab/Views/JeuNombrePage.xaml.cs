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
    public partial class JeuNombrePage : ContentPage
    {
        int nombreMagique = 0;
        public JeuNombrePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            GenereNombre();
            GenereClavier();
        }
        private void GenereClavier()
        {
            string num = "123456789";
            List<Button> bouttons = new List<Button>();
            foreach (char item in num)
            {
                Button button = new Button();
                button.Clicked += ChiffreButton_Click;
                button.Text = item.ToString();
                button.Scale = 0.75;
                button.FontAttributes = FontAttributes.Bold;
                
                clavierNumGrid.Children.Add(button);
                
                bouttons.Add(button);
            }
            Button button10 = new Button();
            button10.Clicked += ChiffreButton_Click;
            button10.Text = "10";
            button10.Scale = 0.75;
            button10.FontAttributes = FontAttributes.Bold;
            clavierNumGrid.Children.Add(button10);
        }

        private void ChiffreButton_Click(object sender, EventArgs e)
        {
            int essai = int.Parse(((Button)sender).Text);
            entryNombre.Text = essai.ToString();
            if (essai==nombreMagique)
            {
                WinAction(nombreMagique);
                return;
            }
            if (essai>nombreMagique)
            {
                DisplayAlert("Oups", $"C'est moins de {essai} !", "ok");
                return;
            }
            if (essai<nombreMagique)
            {
                DisplayAlert("Oups", $"C'est plus de {essai}!", "ok");
                return;
            }

        }

        private void GenereNombre()
        {
            Random rnd = new Random();
            nombreMagique = rnd.Next(1, 10);
        }
        private async Task WinAction(int nombreMagique)
        {
            await Navigation.PushAsync(new WinPage(nombreMagique));
        }

    }
}