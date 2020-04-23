using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PizzaApp.Model;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace PizzaApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        List<Pizza> pizzas = new List<Pizza>();
        List<Pizza> copieListePizzas = new List<Pizza>();
        List<string> pizzasFav = new List<string>();
        string urlGdrive = "https://drive.google.com/uc?export=download&id=1e786cajpgQXcL1RzQ00nXLdO45jBO2by";
        string imageTri = "";
        string KEY_TRI = "tri";
        string KEY_FAV = "fav";
        string jsonFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pizzasJson");
        public MainPage()
        {
            InitializeComponent();
            FonctionListRefresh();
            ChoixAffichage(ContextesAffichage.Loading);
            //ChargementPizzasBasique();
            ChargementDernierTri();
            LoadFavList();
            ChargementPizzasJson();
        }

        private void FonctionListRefresh()
        {
            pizzasListView.RefreshCommand = new Command((obj) =>
            {
                //ChargementPizzasJson();
                DownloadJson();
                pizzasListView.IsRefreshing = false;
            });
        }
        private void ChargementDernierTri()
        {
            if (Application.Current.Properties.ContainsKey(KEY_TRI))
            {
                TriChoisi = (Tri)Application.Current.Properties[KEY_TRI];
                SelectionTri(TriChoisi);
            }
            else
            {
                SelectionTri(Tri.Tri_Aucun);
            }
        }
        private enum ContextesAffichage
        {
            Loading,
            Loaded
        }
        private void ChoixAffichage(ContextesAffichage contexte)
        {
            switch (contexte)
            {
                case ContextesAffichage.Loading:
                    pizzasListView.IsVisible = false;
                    waitStackLayout.IsVisible = true;
                    break;
                case ContextesAffichage.Loaded:
                    pizzasListView.IsVisible = true;
                    waitStackLayout.IsVisible = false;
                    break;

                default:
                    break;
            }

        }
        private enum Tri
        {
            Tri_Aucun,
            Tri_Nom,
            Tri_Prix,
            Tri_Favori
        }
        private Tri TriChoisi;
        private void SelectionTri(Tri tri)
        {
            switch (tri)
            {
                case Tri.Tri_Aucun:
                    TriChoisi = Tri.Tri_Aucun;
                    bouttonTri.Source = "sort_none.png";
                    break;
                case Tri.Tri_Nom:
                    TriChoisi = Tri.Tri_Nom;
                    imageTri = "sort_nom.png";
                    bouttonTri.Source = imageTri;
                    break;
                case Tri.Tri_Prix:
                    TriChoisi = Tri.Tri_Prix;
                    imageTri = "sort_prix.png";
                    bouttonTri.Source = imageTri;
                    break;
                case Tri.Tri_Favori:
                    TriChoisi = Tri.Tri_Favori;
                    imageTri = "sort_fav.png";
                    bouttonTri.Source = imageTri;
                    break;
                default:
                    break;
            }
        }
        private void ChargementPizzasJson()
        {
            if (File.Exists(jsonFileName))
            {
                LectureJson();
            }
            else
            {
                DownloadJson();

            }

        }
        private void DownloadJson()
        {
            using (var webclient = new WebClient())
            {
                //webclient.DownloadStringAsync(new Uri(urlGdrive));
                webclient.DownloadFileAsync(new Uri(urlGdrive), jsonFileName);
                webclient.DownloadFileCompleted += (object sender, AsyncCompletedEventArgs e) =>
                {
                    Exception ex = e.Error;
                    if (e.Error == null)
                    {
                        LectureJson();
                    }
                    else
                    {
                        //Methodes pour repasser dans la thread ui afin d'afficher le message
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            DisplayAlert("Erreur", "Une erreur réseau est survenue", "ok");
                            ChoixAffichage(ContextesAffichage.Loaded);
                        });
                    }
                };

            }

        }
        private void LectureJson()
        {
            string pizzasJson = File.ReadAllText(jsonFileName);
            if (!String.IsNullOrEmpty(pizzasJson))
            {
                pizzas = JsonConvert.DeserializeObject<List<Pizza>>(pizzasJson);
                Device.BeginInvokeOnMainThread(() =>
                {
                    ChoixTri(TriChoisi);
                });
            }
            else
            {
                DownloadJson();
            }

        }
        private void ChargementPizzasBasique()
        {
            pizzas = new List<Pizza>();
            pizzas.Add(new Pizza
            {
                nom = "végétarienne",
                prix = 7,
                ingredients = new string[] { "tomate", "poivrons", "ognions" },
                imageUrl = "https://www.recettes-pizza.fr/files/2011/09/pizza-vegetarienne.jpg"
            });
            pizzas.Add(new Pizza
            {
                nom = "MonTagnarde",
                prix = 11,
                ingredients = new string[] { "reblochon", "pomme de terre", "ognions", "crème", "saucisses", "camenbert", "olives", "oeufs", "tomates" },
                imageUrl = "https://lh4.googleusercontent.com/proxy/7zUTPhVh0G5MIBhjtjGbMIF37OhlVUVDhgU0VJMYw_3fQBPPjmcrJw_Qu7KnZ4GZNXzUMQNJjU60wKF7-atgHtPPpcAxDToMKz-M66wfw8hmkafkz-JnvQAPtkRrZfGF"
            });
            pizzas.Add(new Pizza
            {
                nom = "Carnivore",
                prix = 7,
                ingredients = new string[] { "tomate", "viande hachée", "mozarella" },
                imageUrl = "https://www.smokymountainpizza.com/wp-content/uploads/2014/07/header-carnivore-combo-2-300x273.jpg"
            });
            AffichagePizzas(pizzas);
        }
        private void AffichagePizzas(List<Pizza> listePizzas)
        {
            pizzasListView.ItemsSource = null;
            pizzasListView.ItemsSource = GetPizzaCells(listePizzas, pizzasFav);
            pizzasListView.ItemSelected += (sender, e) =>
            {
                if (pizzasListView.SelectedItem != null)
                {
                    PizzaCell pizzaCell = pizzasListView.SelectedItem as PizzaCell;
                    DisplayAlert(pizzaCell.pizza.Titre, pizzaCell.pizza.IngredientsStr, "OK");
                    pizzasListView.SelectedItem = null;
                }

            };
            ChoixAffichage(ContextesAffichage.Loaded);
            pizzasListView.IsRefreshing = false;
        }
        private void bouttonTri_Clicked(object sender, EventArgs e)
        {

            if (TriChoisi == Tri.Tri_Aucun)
            {
                SelectionTri(Tri.Tri_Nom);
                ChoixTri(TriChoisi);

            }
            else if (TriChoisi == Tri.Tri_Nom)
            {
                SelectionTri(Tri.Tri_Prix);
                ChoixTri(TriChoisi);

            }
            else if (TriChoisi == Tri.Tri_Prix)
            {
                SelectionTri(Tri.Tri_Favori);
                ChoixTri(TriChoisi);
                return;
            }
            else if (TriChoisi == Tri.Tri_Favori)
            {
                SelectionTri(Tri.Tri_Aucun);
                ChoixTri(TriChoisi);
            }
            Application.Current.Properties[KEY_TRI] = (int)TriChoisi;
            Application.Current.SavePropertiesAsync();
        }
        private void ChoixTri(Tri tri)
        {
            switch (tri)
            {
                case Tri.Tri_Aucun:
                    AffichagePizzas(pizzas);
                    break;
                case Tri.Tri_Nom:
                    copieListePizzas = pizzas.OrderBy(p => p.Titre).ToList();
                    AffichagePizzas(copieListePizzas);

                    break;
                case Tri.Tri_Prix:
                    copieListePizzas = pizzas.OrderBy(n => n.prix).ToList();
                    AffichagePizzas(copieListePizzas);

                    break;
                case Tri.Tri_Favori:
                    List<PizzaCell> pizzaCells = new List<PizzaCell>();
                    foreach (PizzaCell pizzaCell in GetPizzaCells(pizzas,pizzasFav))
                    {
                        if (!pizzaCell.isfav)
                        {
                            copieListePizzas.Remove(pizzaCell.pizza);
                        }
                    }
                    
                    AffichagePizzas(copieListePizzas);

                    break;
                default:
                    break;
            }
        }
        private List<PizzaCell> GetPizzaCells(List<Pizza>listPizzas, List<String> listFav)
        {
            List<PizzaCell> pizzaCells = new List<PizzaCell>();
            
            //if (pizzaCells==null)
            //{
            //    return pizzaCells;
            //}

            foreach (Pizza pizza in listPizzas)
            {
                bool isFav = listFav.Contains(pizza.nom.ToLower());
                pizzaCells.Add(new PizzaCell { pizza = pizza, isfav = isFav, favChangeAction = OnFavPizzaChanged });

            }
            return pizzaCells;
        }
        private void OnFavPizzaChanged(PizzaCell pizzaCell)
        {
            bool isInFavList = pizzasFav.Contains(pizzaCell.pizza.nom.ToLower());
            if (pizzaCell.isfav && !isInFavList)
            {
                pizzasFav.Add(pizzaCell.pizza.nom.ToLower());
            }
            else if (!pizzaCell.isfav && isInFavList)
            {
                pizzasFav.Remove(pizzaCell.pizza.nom);
            }
            SaveFavList();
        }

        private void SaveFavList()
        {
            Application.Current.Properties[KEY_FAV] = JsonConvert.SerializeObject(pizzasFav);
            Application.Current.SavePropertiesAsync();
        }
        private void LoadFavList()
        {
            if (Application.Current.Properties.ContainsKey(KEY_FAV))
            {
               pizzasFav = JsonConvert.DeserializeObject<List<string>>(Application.Current.Properties[KEY_FAV].ToString());
            }
        }
    }

}

