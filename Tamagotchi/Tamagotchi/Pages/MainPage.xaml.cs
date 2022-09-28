using Android.Media;
using System;
using System.Timers;
using Tamagotchi.Pages;
using Xamarin.Essentials;
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

            UpdateUI();
            var timeManager = DependencyService.Get<TimeManager>();
            timeManager.InitializeTimer(timerInterval, OnTimerElapsed);
        }

        private void PrepareCreature()
        {
            dataStorer = new DataStorer();

            Creature = dataStorer.ReadData();

            if (Creature == null)
            {
                Creature = new Creature();
                dataStorer.CreateData(Creature);
            }
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs args)
        {
            //double interval
            Creature.ReceiveAllTimePenalties(timerInterval/1000.0);
            SaveCreatureData();
            UpdateUI(); // Zou zonder moeten kunnen
            //Console.WriteLine(stats);
        }

        private void UpdateUI()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                StatsLabel.Text = stats;
            });
        }

        private void FoodPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FoodPage(Creature));
        }

       /* private void GoToClickedPage(object sender, EventArgs e)
        {
            Page page = new Page();
            if (sender.Equals(FoodButton))
            {
                page = new FoodPage(Creature);
            }
            else if (sender.Equals(SocialButton))
            {
                page = new SocialPage(Creature); 
            }

            Navigation.PushAsync(page);
        }*/

        private void SleepPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FoodPage(Creature));
        }

        private void AloneTimePage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FoodPage(Creature));
        }

        private void SocialPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SocialPage(Creature));
        }

        private void SaveCreatureData()
        {
            Console.WriteLine("SAVED");
            dataStorer.UpdateData(Creature);
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
