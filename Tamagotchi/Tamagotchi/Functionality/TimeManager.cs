using System;
using Xamarin.Essentials;

namespace Tamagotchi
{
    internal class TimeManager
    {
        public TimeManager()
        {
            App.OnStartEvent += OnAppStart;
            App.OnSleepEvent += OnAppSleep;
            App.OnResumeEvent += OnAppResume;
        }

        private void OnAppStart()
        {

        }

        private void OnAppSleep()
        {
            // Utc is internationale tijd, overal gelijk
            DateTime sleepTime = DateTime.UtcNow;
            Preferences.Set("SleepTime", sleepTime);
        }

        private void OnAppResume()
        {
            DateTime dateTime = Preferences.Get("SleepTime", DateTime.UtcNow);
            TimeSpan timeSpan = DateTime.UtcNow - dateTime;

            double secondsPassed = timeSpan.TotalSeconds;
            Preferences.Set("secondsPassed", secondsPassed);
        }
    }
}
