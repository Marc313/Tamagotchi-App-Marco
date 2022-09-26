using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tamagotchi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class View1 : ContentView
    {
        public static readonly BindableProperty creatureProperty = BindableProperty.Create(nameof(creature), typeof(Creature), typeof(View1));

        public Creature creature
        {
            get => GetValue(creatureProperty) as Creature;
            set => SetValue((creatureProperty), value);
        }

        public string stats
        {
            get { return creature.NeedsToString(); }
            set { }
        }

        public View1()
        {
            BindingContext = this;

            InitializeComponent();
        }

        public void UpdateUI()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                StatsLabel.Text = stats;
            });
        }

        /*public string ShowNeeds()
        {
            float Hunger = creatureRef.Hunger.value;
            float Thirst = creatureRef.Thirst.value;
            float Attention = creatureRef.Attention.value;
            float Energy = creatureRef.Energy.value;
            float SocialEnergy = creatureRef.SocialEnergy.value;
            float Loneliness = creatureRef.Loneliness.value;

            string stats = $"Hunger: {Hunger}\n" +
                            $"Thirst: {Thirst}\n" +
                            $"Attention: {Attention}\n" +
                            $"Energy: {Energy}\n" +
                            $"Social Energy: {SocialEnergy}\n" +
                            $"Loneliness: {Loneliness}";

            StatsLabel.Text = stats;
            return stats;
        }*/
    }
}