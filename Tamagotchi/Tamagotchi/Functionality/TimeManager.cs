using System;
using System.Timers;
using Xamarin.Essentials;

namespace Tamagotchi
{
    internal class TimeManager
    {
        private Timer timer;

        public TimeManager()
        {
            App.OnStartEvent += OnAppStart;
            App.OnSleepEvent += OnAppSleep;
            App.OnResumeEvent += OnAppResume;
        }

        public void InitializeTimer(double interval, ElapsedEventHandler action)
        {
            // In-game timer
            timer = new Timer();
            // Omdat apps minder zwaar zijn dan games kun je doubles gebruiken ipv floats om accuracy te verhogen.
            timer.Interval = 1000.0;
            timer.AutoReset = true;
            timer.Elapsed += action;
            timer.Start();
            Console.WriteLine("Start");
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
