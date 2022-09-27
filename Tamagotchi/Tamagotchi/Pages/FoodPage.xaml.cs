using System;
using System.Timers;
using Xamarin.Forms;

namespace Tamagotchi
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPage : ContentPage
    {
        private Creature creature;
        public Creature MyCreature {
            get { return creature; }
            set { creature = value; OnPropertyChanged(nameof(MyCreature)); }
        }
        private Timer timer;

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

            // In-game timer
            timer = new Timer();
            // Omdat apps minder zwaar zijn dan games kun je doubles gebruiken ipv floats om accuracy te verhogen.
            timer.Interval = 1000.0;
            timer.AutoReset = true;
            timer.Elapsed += OnTimerElapsed;
            timer.Start();
            Console.WriteLine("Start");


            /*var timeManager = DependencyService.Get<TimeManager>();
            timeManager.InitializeTimer(1000.0, OnTimerElapsed);*/
            InitializeComponent();

            StartButtonAnimation();

        }

        private async void StartButtonAnimation()
        {
            await MovingButton.TranslateTo(-100, 0, 500);
            await MovingButton.TranslateTo(100, 0, 500);
            StartButtonAnimation();
        }

        /*override OnDisappearing()
        {

        }*/

        private void OnTimerElapsed(object sender, ElapsedEventArgs args)
        {
            MyCreature.Hunger.ReceiveTimePenalty(timer.Interval/1000.0);
            UpdateUI(); // Zou zonder moeten kunnen
            //Console.WriteLine(stats);
        }

        private void FeedBoii(object sender, System.EventArgs e)
        {
            MyCreature.Hunger.IncreaseValue(5);
            UpdateUI();

            //await StatsLabel.RotateTo(90, 500, Easing.SinIn);
            //StatsLabel.TranslateTo(-100, 0, 300, Easing.BounceIn);    // Vanuit het scherm: eerst vna in naar uit, dan van uit naar in.
        }

        private void UpdateUI()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                CreatureBinding.MyCreature = this.MyCreature;
                StatsLabel.Text = stats;
            });
        }
    }
}