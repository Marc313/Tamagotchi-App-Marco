using System;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tamagotchi
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPage : ContentPage
    {
        public Creature Creature { get; set; }
        public Need pageSpecificNeed => Creature.Food;
        public double ProgressValue => pageSpecificNeed.Value / 100;
        public string NeedValueText => $"Food: {pageSpecificNeed.ValueToOneDecimal()}";

        public string NeedStateText
        {
            get
            {
                switch (pageSpecificNeed.NeedState)
                {
                    case Need.State.HEALTHY:
                        return "Your tamagotchi is full!";
                    case Need.State.NOTGREAT:
                        return "Your tamagotchi is craving a snack";
                    case Need.State.DANGER:
                        return "Your tamagotchi is hungry and wants a burger";
                    case Need.State.EMERGENCY:
                        return "Your tamagotchi is dying of hunger!";
                    default:
                        return "Your tamagotchi is having trouble communicating with you";
                }
            }
        }

        private uint animationLenght = 500;

        public FoodPage(Creature creature)
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
                FoodTextLabel.Text = NeedStateText;
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
            Creature.Food.IncreaseValue(5);
            PlayScalingAnimation(TamagotchiImage as View);
            PlayScalingAnimation(MovingButton as View);
        }
    }
}