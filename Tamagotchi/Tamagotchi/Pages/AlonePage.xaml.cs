using System;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tamagotchi
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlonePage : ContentPage
    {
        public Creature Creature { get; set; }
        public Need pageSpecificNeed => Creature.SocialEnergy;
        public double ProgressValue => pageSpecificNeed.Value / 100;
        public string NeedValueText => $"Social Energy: {pageSpecificNeed.ValueToOneDecimal()}";

        public string NeedStateText
        {
            get
            {
                switch (pageSpecificNeed.NeedState)
                {
                    case Need.State.HEALTHY:
                        return "Your tamagotchi has a charged social battery";
                    case Need.State.NOTGREAT:
                        return "Your tamagotchi would like to play a single-player game";
                    case Need.State.DANGER:
                        return "Your tamagotchi wants to be left alone";
                    case Need.State.EMERGENCY:
                        return "Your tamagotchi is overstimilated and desperately needs alone time";
                    default:
                        return "Your tamagotchi is having trouble communicating with you";
                }
            }
        }

        private uint animationLenght = 500;


        public AlonePage(Creature creature)
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
            await MovingButton.TranslateTo(-150, 0, animationLenght);
            await MovingButton.TranslateTo(150, 0, animationLenght);
            StartButtonAnimation();
        }

        private void UpdateUI()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Color progressColor = ColorManager.GetColorFromState(pageSpecificNeed.NeedState);
                ProgressBar.ProgressColor = progressColor;
                ProgressBar.Progress = ProgressValue;
                NeedText.Text = NeedValueText;
                AloneTextLabel.Text = NeedStateText;
            });
        }

        private void FeedBoii(object sender, System.EventArgs e)
        {
            Creature.SocialEnergy.IncreaseValue(5);
            UpdateUI();
        }
    }
}