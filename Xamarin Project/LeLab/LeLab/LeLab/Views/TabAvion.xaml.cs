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
    public partial class TabAvion : ContentPage
    {
        public TabAvion()
        {
            InitializeComponent();
            avionImage.Opacity = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            avionImage.TranslationX = -250;
            avionImage.TranslationY = -150;
            avionImage.Rotation = 45;
            avionImage.Scale = 0.7;
            avionImage.Opacity = 0;

            //uint length = 800;
            //avionImage.FadeTo(1, 100);
            //avionImage.TranslateTo(0, 0, length, Easing.SinOut);
            //avionImage.RotateTo(0, length, Easing.SinOut).ContinueWith((arg)=>
            //{
            //    avionImage.ScaleTo(1, length, Easing.SinOut);
            //});

            Anim();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            avionImage.Opacity = 0;
        }

        private async void Anim()
        {
            uint length = 800;
            await avionImage.FadeTo(1, 100);
            await avionImage.TranslateTo(0, 0, length, Easing.SinOut);
            await avionImage.RotateTo(0, length, Easing.SinOut);
            await avionImage.ScaleTo(1, length, Easing.SinOut);
            
        }
    }
}