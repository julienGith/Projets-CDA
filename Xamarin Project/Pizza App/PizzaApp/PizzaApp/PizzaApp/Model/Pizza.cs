using PizzaApp.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaApp.Model
{
    public class Pizza
    {
        public string nom { get; set; }
        public int prix { get; set; }
        public string[] ingredients { get; set; }
        public string PrixEuros { get { return this.prix + " €"; } }
        public string imageUrl { get; set; }
        public string Titre { get { return nom.PremiereLettreMajuscule(); }}
        public string IngredientsStr { get {
                StringBuilder builder = new StringBuilder();
                foreach (string value in this.ingredients)
                {
                    builder.Append(value);
                    builder.Append(", ");
                }
                return builder.ToString();
            }
        }

        //public bool isfav { get; set; }
        //public string imageFav { get { return isfav ? "star2.png" : "star1.png"; }}
    }
}
