using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tamagotchi.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SocialPage : ContentPage
    {
        public Creature MyCreature { get; set; }

        public SocialPage(Creature creature)
        {
            this.MyCreature = creature;

            BindingContext = this;

            InitializeComponent();
        }
    }
}