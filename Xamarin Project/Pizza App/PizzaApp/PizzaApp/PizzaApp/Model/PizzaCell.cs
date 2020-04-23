using PizzaApp.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PizzaApp.Model
{
    public class PizzaCell : INotifyPropertyChanged
    {
        public PizzaCell()
        {
            FavClickCommand = new Command((obj) =>
              {
                  Pizza commandParam = obj as Pizza;
                  isfav = !isfav;
                  OnPropertyChanged("imageFav");
                  favChangeAction.Invoke(this);
              });
        }
        public Pizza pizza { get; set; }
        public bool isfav { get; set; }
        public string imageFav { get { return isfav ? "star2.png" : "star1.png"; }}
        public Action<PizzaCell> favChangeAction { get; set; }
        public ICommand FavClickCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
