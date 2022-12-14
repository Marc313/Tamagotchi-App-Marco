using System;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tamagotchi
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnergyPage : ContentPage
    {
        public Creature Creature { get; set; }
        public Need pageSpecificNeed => Creature.Energy;
        public double ProgressValue => pageSpecificNeed.Value / 100;
        public string NeedValueText => $"Energy: {pageSpecificNeed.ValueToOneDecimal()}";

        public string NeedStateText
        {
            get
            {
                switch (pageSpecificNeed.NeedState)
                {
                    case Need.State.HEALTHY:
                        return "Your tamagotchi is well rested";
                    case Need.State.NOTGREAT:
                        return "Your tamagotchi is losing its energy";
                    case Need.State.DANGER:
                        return "Your tamagotchi is tired, send it to bed";
                    case Need.State.EMERGENCY:
                        return "Your tamagotchi is exhausted and should sleep immediately!";
                    default:
                        return "Your tamagotchi is having trouble communicating with you";
                }
            }
        }

        private uint animationLenght = 500;

        public EnergyPage(Creature creature)
        {
            BindingContext = this;

            Creature = creature;

            InitializeComponent();

            creature.OnCreatureChanged += UpdateUI;

            StartButtonAnimation();
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
                EnergyTextLabel.Text = NeedStateText;
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
            Creature.Energy.IncreaseValue(5);
            PlayScalingAnimation(TamagotchiImage as View);
            PlayScalingAnimation(MovingButton as View);
        }
    }
}