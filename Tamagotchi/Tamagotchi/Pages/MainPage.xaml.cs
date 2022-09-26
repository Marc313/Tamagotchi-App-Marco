using Android.Media;
using System;
using Tamagotchi.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tamagotchi
{
    public partial class MainPage : ContentPage
    {
        private Creature creature;
        private DataStorer dataStorer;

        public MainPage()
        {
            // Alternative Constructor
            /*creature = new Creature
            {
                Attention.value = 50f;
            }*/

            App.OnSleepEvent += SaveCreatureData;
            PrepareCreature();

            InitializeComponent();
        }        

        private void PrepareCreature()
        {
            dataStorer = new DataStorer();

            creature = dataStorer.ReadData();

            if (creature == null)
            {
                creature = new Creature();
                dataStorer.CreateData(creature);
            }
            else
            {
                if (Preferences.ContainsKey("secondsPassed"))
                {
                    //double timePassed = Preferences.Get("secondsPassed", 0);
                    double timePassed = 10.0;
                    creature.ReceiveAllTimePenalties(timePassed);
                }
            }
        }

        private void FoodPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FoodPage(creature));
        }

        private void GoToClickedPage(object sender, EventArgs e)
        {
            Page page = new Page();
            if (sender.Equals(FoodButton))
            {
                page = new FoodPage(creature);
            }
            else if (sender.Equals(SocialButton))
            {
                page = new SocialPage(creature); 
            }

            Navigation.PushAsync(page);
        }

        private void SleepPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FoodPage(creature));
        }

        private void AloneTimePage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FoodPage(creature));
        }

        private void SocialPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SocialPage(creature));
        }

        private void SaveCreatureData()
        {
            Console.WriteLine("SAVED");
            dataStorer.UpdateData(creature);
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
