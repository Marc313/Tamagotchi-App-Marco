using System;
using System.Timers;
using Xamarin.Forms;

namespace Tamagotchi
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPage : ContentPage
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        private Creature creatureRef;
        private Timer timer;

        public string stats
        {
            get { return creatureRef.NeedsToString(); }
            set { }
        }

        public FoodPage(Creature creature)
        {
            BindingContext = this;
            //PropertyChanged += UpdateUI;

            creatureRef = creature;

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
        }

        /*override OnDisappearing()
        {

        }*/

        private void OnTimerElapsed(object sender, ElapsedEventArgs args)
        {
            creatureRef.Hunger.ReceiveTimePenalty(timer.Interval/1000.0);
            UpdateUI(); // Zou zonder moeten kunnen
            Console.WriteLine(stats);
        }

        private void FeedBoii(object sender, System.EventArgs e)
        {
            creatureRef.Hunger.IncreaseValue(5);
            //UpdateUI();

            //await StatsLabel.RotateTo(90, 500, Easing.SinIn);
            //StatsLabel.TranslateTo(-100, 0, 300, Easing.BounceIn);    // Vanuit het scherm: eerst vna in naar uit, dan van uit naar in.

        }

        private void UpdateUI()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                StatsLabel.Text = stats;
            });
        }
    }
}