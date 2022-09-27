using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tamagotchi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class View1 : ContentView, INotifyPropertyChanged
    {
        public static readonly BindableProperty MyCreatureProperty = BindableProperty.Create(nameof(MyCreature), typeof(Creature), typeof(View1), propertyChanged: CreaturePropertyChanged);

        private static void CreaturePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as View1).OnPropertyChanged(propertyName: nameof(MyCreature));
            (bindable as View1).OnPropertyChanged(nameof(stats));
            (bindable as View1).UpdateUI();
        }

        public Creature MyCreature
        {
            get => GetValue(MyCreatureProperty) as Creature;
            set => SetValue((MyCreatureProperty), value);
        }

        public string stats
        {
            get { return MyCreature?.NeedsToString; }
            set { }
        }

        public View1()
        {
            //BindingContext = this;

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