using System;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tamagotchi
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPage : ContentPage
    {
        private Creature creature;
        public Creature MyCreature {
            get { return creature; }
            set { creature = value; }
        }

        public string stats
        {
            get { return MyCreature.NeedsToString; }
            set { }
        }

        public FoodPage(Creature creature)
        {
            //PropertyChanged += UpdateUI;
            BindingContext = this;

            MyCreature = creature;

            InitializeComponent();

            StartButtonAnimation();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void StartButtonAnimation()
        {
            double width = DeviceDisplay.MainDisplayInfo.Width;
            await MovingButton.TranslateTo(-150, 0, 500);
            await MovingButton.TranslateTo(150, 0, 500);
            StartButtonAnimation();
        }

        private void UpdateUI()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //CreatureBinding.MyCreature = this.MyCreature;
                StatsLabel.Text = stats;
            });
        }

        private void FeedBoii(object sender, System.EventArgs e)
        {
            MyCreature.Hunger.IncreaseValue(5);
            UpdateUI();

            //await StatsLabel.RotateTo(90, 500, Easing.SinIn);
            //StatsLabel.TranslateTo(-100, 0, 300, Easing.BounceIn);    // Vanuit het scherm: eerst vna in naar uit, dan van uit naar in.
        }
    }
}