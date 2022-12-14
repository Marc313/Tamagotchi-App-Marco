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
            timer.Interval = interval;
            timer.AutoReset = true;
            timer.Elapsed += action;
            timer.Start();
            Console.WriteLine("Start");
        }

        public void AddTimerEvent(ElapsedEventHandler action)
        {
            if (timer == null) { return; }

            timer.Elapsed += action;
        }

        public void RemoveTimerEvent(ElapsedEventHandler action)
        {
            if (timer == null) { return; }

            timer.Elapsed -= action;
        }

        private void OnAppStart()
        {
            Console.WriteLine("START");
            OnAppResume();
        }

        private void OnAppSleep()
        {
            // Utc is internationale tijd, overal gelijk
            DateTime sleepTime = DateTime.UtcNow;
            Preferences.Set("SleepTime", sleepTime);
            Console.WriteLine($"Sleep time set to: {sleepTime}");
        }

        private void OnAppResume()
        {
            Console.WriteLine("RESUME");

            DateTime dateTime = Preferences.Get("SleepTime", DateTime.UtcNow);
            TimeSpan timeSpan = DateTime.UtcNow - dateTime;

            double secondsPassed = timeSpan.TotalSeconds;
            Preferences.Set("secondsPassed", secondsPassed);
        }
    }
}
