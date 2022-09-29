using System;
using System.Timers;
using Tamagotchi.Pages;
using Xamarin.Forms;

namespace Tamagotchi
{
    public partial class MainPage : ContentPage
    {
        public Creature Creature { get; set; }
        public string stats => Creature.NeedsToString;

        private DataStorer dataStorer;
        private StyleManager styleManager;
        private double timerInterval = 5000.0;

        public MainPage()
        {
            App.OnSleepEvent += SaveCreatureData;
            PrepareCreature();

            InitializeComponent();

            /*Style style = Application.Current.Resources["HealthyStyle"] as Style;
            FoodButton.Style = style;*/

            CreateTimer();
            UpdateUI();
        }

        private void PrepareCreature()
        {
            dataStorer = DependencyService.Get<DataStorer>();

            Creature = dataStorer.ReadData();

            if (Creature == null)
            {
                Creature = new Creature();
                dataStorer.CreateData(Creature);
            }
        }

        private void CreateTimer()
        {
            var timeManager = DependencyService.Get<TimeManager>();
            timeManager.InitializeTimer(timerInterval, OnTimerElapsed);
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs args)
        {
            double intervalToSeconds = timerInterval / 1000.0;
            Creature.ReceiveAllTimePenalties(intervalToSeconds);

            SaveCreatureData();
            UpdateUI(); // Zou zonder moeten kunnen
        }

        private void UpdateUI()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                StatsLabel.Text = stats;
                UpdateButtonColors();
            });
        }

        /*private void UpdateButtonStyles()
        {
            if (styleManager == null)
            {
                styleManager = DependencyService.Get<StyleManager>();
            }

            FoodButton.Style = styleManager.GetStyleFromState(Creature.Food.NeedState);
            DrinkButton.Style = styleManager.GetStyleFromState(Creature.Hydration.NeedState);
            AttentionButton.Style = styleManager.GetStyleFromState(Creature.Attention.NeedState);
            EneryButton.Style = styleManager.GetStyleFromState(Creature.Energy.NeedState);
            AloneButton.Style = styleManager.GetStyleFromState(Creature.SocialEnergy.NeedState);
            SocialButton.Style = styleManager.GetStyleFromState(Creature.Company.NeedState);
        }*/

        private void UpdateButtonColors()
        {
            FoodButton.BackgroundColor = ColorManager.GetColorFromState(Creature.Food.NeedState);
            DrinkButton.BackgroundColor = ColorManager.GetColorFromState(Creature.Hydration.NeedState);
            AttentionButton.BackgroundColor = ColorManager.GetColorFromState(Creature.Attention.NeedState);
            EneryButton.BackgroundColor = ColorManager.GetColorFromState(Creature.Energy.NeedState);
            AloneButton.BackgroundColor = ColorManager.GetColorFromState(Creature.SocialEnergy.NeedState);
            SocialButton.BackgroundColor = ColorManager.GetColorFromState(Creature.Company.NeedState);
        }

        private void SaveCreatureData()
        {
            Console.WriteLine("SAVED");
            dataStorer.UpdateData(Creature);
        }


        // Button EventHandlers \\

        private void FoodPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FoodPage(Creature));
        }

        private void DrinkPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DrinkPage(Creature));
        }

        private void AttentionPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AttentionPage(Creature));
        }

        private void SleepPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EnergyPage(Creature));
        }

        private void AloneTimePage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AlonePage(Creature));
        }

        private void SocialPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SocialPage(Creature));
        }

        /*private void PlaySound()
        {
            // NOTE: Only works for android??
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.SetDataSource("MARCO.mp3");
            mediaPlayer.Prepare();
            mediaPlayer.Start();
        }*/
    }
}
