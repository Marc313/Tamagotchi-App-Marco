using System.Timers;
using Xamarin.Forms;

namespace Tamagotchi
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttentionPage : ContentPage
    {
        public Creature Creature { get; set; }
        public Need pageSpecificNeed => Creature.Attention;
        public double ProgressValue => pageSpecificNeed.Value / 100;
        public string NeedValueText => $"Attention: {pageSpecificNeed.ValueToOneDecimal()}";

        public string NeedStateText
        {
            get
            {
                switch (pageSpecificNeed.NeedState)
                {
                    case Need.State.HEALTHY:
                        return "Your tamagotchi enjoyed playing with you";
                    case Need.State.NOTGREAT:
                        return "Your tamagotchi would like to play with you";
                    case Need.State.DANGER:
                        return "Your tamagotchi is bored, play some soccer with it";
                    case Need.State.EMERGENCY:
                        return "Your tamagotchi is so bored it is plotting to take over the world";
                    default:
                        return "Your tamagotchi is having trouble communicating with you";
                }
            }
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
                AttentionTextLabel.Text = NeedStateText;
            });
        }

        private void FeedBoii(object sender, System.EventArgs e)
        {
            Creature.Attention.IncreaseValue(5);
            UpdateUI();
        }
    }
}