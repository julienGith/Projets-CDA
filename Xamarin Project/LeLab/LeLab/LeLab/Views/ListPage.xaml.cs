using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LeLab.Views
{
    public class Article
    {
        public string Nom { get; set; }
        public string Prix { get; set; }
        public string Description { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        List<Article> articles;
        public ListPage()
        {
            InitializeComponent();
            articles = new List<Article>();
            articles.Add(new Article { Nom = "Lait", Prix = "4€", Description ="Pack de lait" });
            articles.Add(new Article { Nom = "Chocolat", Prix = "2.5€", Description = "100% Chocolat" });
            articles.Add(new Article { Nom = "Pain", Prix = "2€" , Description = "Pain traditionnel" });
            articles.Add(new Article { Nom = "Beurre", Prix = "1.2€" , Description = "Beurre 90%" });
            articlesListView.ItemsSource = articles;
            articlesListView.ItemSelected += (sender, e) =>
            {
                if (articlesListView.SelectedItem != null)
                {
                    Article article = articlesListView.SelectedItem as Article;
                    DisplayAlert(article.Nom, article.Description, "OK");
                    articlesListView.SelectedItem = null;
                }

            };
        }
    }
}