using System;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tamagotchi
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttentionPage : ContentPage
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

        public AttentionPage(Creature creature)
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
            MyCreature.Attention.IncreaseValue(5);
            UpdateUI();
        }
    }
}