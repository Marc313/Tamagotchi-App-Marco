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
        private double timerInterval = 5000.0;

        public MainPage()
        {
            App.OnSleepEvent += SaveCreatureData;
            PrepareCreature();

            InitializeComponent();

            App.OnSleepEvent += ScheduleNotifcation;

            CreateTimer();
            UpdateUI();

            Creature.OnCreatureChanged += UpdateUI;
            Creature.OnCreatureChanged += SaveCreatureData;
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
        }

        private void UpdateUI()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                UpdateButtonColors();
                StateText.Text = CreatureStateResponse.GetTextFromState(Creature.CurrentState);
                TamagotchiImage.Source = CreatureStateResponse.GetImageSourceFromState(Creature.CurrentState);
            });
        }

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

        private void ResetCreature()
        {
            Creature = new Creature();
            dataStorer.DeleteData(Creature);
            dataStorer.CreateData(Creature);

            Creature.OnCreatureChanged += UpdateUI;
            Creature.OnCreatureChanged += SaveCreatureData;

            UpdateUI();
        }

        private void ScheduleNotifcation()
        {
            double secondsToNotification = Creature.SecondsToFirstEmergency();
            NotificationManager.ScheduleNotificationAfterSeconds("Open the app and take care of Kuchipachi", secondsToNotification);
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

        private void ResetButton(object sender, EventArgs e)
        {
            ResetCreature();
        }
    }
}
