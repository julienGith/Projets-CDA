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
    public partial class JeuPenduPage : ContentPage
    {
        public JeuPenduPage()
        {
            InitializeComponent();
            GenereClavier();
            CacheLeMot();
        }
        string mot = "XAMARIN";

        private void CacheLeMot()
        {
            
            for (int i = 0; i < mot.Length; i++)
            {
                entryMot.Text += "?";
            }
        }
        private void GenereClavier()
        {
            string alpha = /*"abcdefghijklmnopqrstuvwxyz"*/ "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int margeHaut = 15; int margeGauche = 15;
            Thickness myThickness = new Thickness();
            myThickness.Left = margeGauche;
            myThickness.Top = margeHaut;
            List<Button> bouttons = new List<Button>();
            foreach (char item in alpha)
            {
                Button button = new Button();
                button.Clicked += LettreButton_Click;
                button.Text = item.ToString();
                button.Scale = 0.75;
                button.FontAttributes = FontAttributes.Bold;
                clavierGrid.Children.Add(button);
                bouttons.Add(button);
            }
        }

        private void LettreButton_Click(object sender, EventArgs e)
        {
            char[] tabMotCach = mot.ToCharArray();
            List<int> stockPos = new List<int>();
            string chaine = ((Button)sender).Text.ToString();
            char lettre = chaine[0];
            string temp = entryMot.Text;
            for (int i = 0; i < tabMotCach.Length; i++)
            {
                if (lettre == tabMotCach[i])
                {
                    stockPos.Add(i);
                }
            }
            foreach (int item in stockPos)
            {
                StringBuilder str = new StringBuilder(temp);
                str[item] = lettre;
                temp = str.ToString();
                entryMot.Text = temp;
            }
            ((Button)sender).IsEnabled = false;

            if (entryMot.Text==mot)
            {
                PartieGagne();
            }
        }
        private async Task PartieGagne()
        {
            await DisplayAlert("Mot Trouvé", $"Bravo vous avez gagné {mot}", "ok");
            await this.Navigation.PushAsync(new JeuPenduPage());
        }
    }
}