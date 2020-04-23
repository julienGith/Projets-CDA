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
    public partial class StarsView : ContentView
    {
        public StarsView()
        {
            InitializeComponent();
            RotationInfini(star1, 4000);
            RotationInfini(star2, 4000);
            RotationInfini(star3, 4000);
        }
        private async Task RotationInfini(View view, uint length)
        {
            bool always = true;
            while (always)
            {
                await view.RotateTo(360, length);
                view.Rotation = 0;
            }
        }
    }
}