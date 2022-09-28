using System;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tamagotchi
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttentionPage : ContentPage
    {
        public Creature Creature { get; set; }
        public Need pageSpecificNeed => Creature.Hydration;
        public double ProgressValue => pageSpecificNeed.Value / 100;

        public string stats
        {
            get { return Creature.NeedsToString; }
            set { }
        }

        private uint animationLenght = 500;

        public AttentionPage(Creature creature)
        {
            //PropertyChanged += UpdateUI;
            BindingContext = this;

            Creature = creature;

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
            await MovingButton.TranslateTo(-150, 0, 500);
            await MovingButton.TranslateTo(150, 0, 500);
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
            Creature.Attention.IncreaseValue(5);
            UpdateUI();
        }
    }
}