using System.Timers;
using Xamarin.Forms;

namespace Tamagotchi
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrinkPage : ContentPage
    {
        public Creature Creature { get; set; }
        public Need pageSpecificNeed => Creature.Hydration;
        public double ProgressValue => pageSpecificNeed.Value / 100;
        public string NeedValueText => $"Hydration: {pageSpecificNeed.ValueToOneDecimal()}";

        public string NeedStateText {
            get
            {
                switch(pageSpecificNeed.NeedState)
                {
                    case Need.State.HEALTHY:
                        return "Your tamagotchi is well hydrated!";
                    case Need.State.NOTGREAT:
                        return "Your tamagotchi might like another sip of milk";
                    case Need.State.DANGER:
                        return "Your tamagotchi is thirsty, give it some milk";
                    case Need.State.EMERGENCY:
                        return "Your tamagotchi is very thirsty, keep it hydrated!";
                        default:
                    return "Your tamagotchi is having trouble communicating with you";
                }
            }
        }

        private uint animationLength = 750;

        public DrinkPage(Creature creature)
        {
            //PropertyChanged += UpdateUI;
            BindingContext = this;

            Creature = creature;

            InitializeComponent();

            creature.OnCreatureChanged += UpdateUI;
            StartButtonAnimation();
            UpdateUI();
        }

        private async void StartButtonAnimation()
        {
            await MovingButton.TranslateTo(-150, 0, animationLength);
            await MovingButton.TranslateTo(150, 0, animationLength);
            StartButtonAnimation();
            UpdateUI();
        }

        private void UpdateUI()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Color progressColor = ColorManager.GetColorFromState(pageSpecificNeed.NeedState);
                ProgressBar.ProgressColor = progressColor;
                ProgressBar.Progress = ProgressValue;
                NeedText.Text = NeedValueText;
                DrinkTextLabel.Text = NeedStateText;
                TamagotchiImage.Source = CreatureStateResponse.GetImageSourceFromState(Creature.CurrentState);
            });
        }

        private async void PlayScalingAnimation(View view)
        {
            await view.ScaleTo(1.20, 150);
            view.ScaleTo(1, 150);
        }

        private void FeedBoii(object sender, System.EventArgs e)
        {
            Creature.Hydration.IncreaseValue(5);
            PlayScalingAnimation(TamagotchiImage as View);
            PlayScalingAnimation(MovingButton as View);
        }
    }
}