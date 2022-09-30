using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tamagotchi.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SocialPage : ContentPage
    {
        public Creature Creature { get; set; }
        public Need pageSpecificNeed => Creature.Company;
        public double ProgressValue => pageSpecificNeed.Value / 100;
        public string NeedValueText => $"Company: {pageSpecificNeed.ValueToOneDecimal()}";

        public string NeedStateText
        {
            get
            {
                switch (pageSpecificNeed.NeedState)
                {
                    case Need.State.HEALTHY:
                        return "Your tamagotchi had nice company!";
                    case Need.State.NOTGREAT:
                        return "Your tamagotchi would like to speak to his friend";
                    case Need.State.DANGER:
                        return "Your tamagotchi hasn't seen his friends for a while";
                    case Need.State.EMERGENCY:
                        return "Your tamagotchi is lonely and depressed";
                    default:
                        return "Your tamagotchi is having trouble communicating with you";
                }
            }
        }

        public string stats
        {
            get { return Creature.NeedsToString; }
            set { }
        }

        private uint animationLength = 750;

        public SocialPage(Creature creature)
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
            await MovingButton.TranslateTo(-150, 0, animationLength);
            await MovingButton.TranslateTo(150, 0, animationLength);
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
                CompanyTextLabel.Text = NeedStateText;
            });
        }

        private async void PlayScalingAnimation(View view)
        {
            await view.ScaleTo(1.20, 150);
            view.ScaleTo(1, 150);
        }

        private void FeedBoii(object sender, System.EventArgs e)
        {
            Creature.Company.IncreaseValue(5);
            PlayScalingAnimation(TamagotchiImage as View);
            PlayScalingAnimation(MovingButton as View);
        }
    }
}