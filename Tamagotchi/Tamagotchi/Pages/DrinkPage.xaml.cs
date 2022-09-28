using System;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tamagotchi
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrinkPage : ContentPage
    {
        private Creature creature;
        public Creature MyCreature {
            get { return creature; }
            set { creature = value; }
        }

        public Need pageSpecificNeed { get { return MyCreature.Hydration; } set { } }

        public double ProgressValue 
        { 
            get => pageSpecificNeed.Value/100.0;
            set { }
        }

        public string stats
        {
            get { return MyCreature.NeedsToString; }
            set { }
        }

        private uint animationLength = 750;

        public DrinkPage(Creature creature)
        {
            //PropertyChanged += UpdateUI;
            BindingContext = this;

            MyCreature = creature;

            InitializeComponent();

            TimeManager timeManager = DependencyService.Get<TimeManager>();
            timeManager.AddTimerEvent(OnTimerElapsed);
            StartButtonAnimation();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            TimeManager timeManager = DependencyService.Get<TimeManager>();
            timeManager.RemoveTimerEvent(OnTimerElapsed);
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs args)
        {
            UpdateUI();
        }

        private async void StartButtonAnimation()
        {
            double width = DeviceDisplay.MainDisplayInfo.Width;
            await MovingButton.TranslateTo(-150, 0, animationLength);
            await MovingButton.TranslateTo(150, 0, animationLength);
            StartButtonAnimation();
        }

        private void UpdateUI()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ProgressBar.Progress = ProgressValue;
                StatsLabel.Text = stats;
            });
        }

        private void FeedBoii(object sender, System.EventArgs e)
        {
            MyCreature.Hydration.IncreaseValue(5);
            UpdateUI();
        }
    }
}