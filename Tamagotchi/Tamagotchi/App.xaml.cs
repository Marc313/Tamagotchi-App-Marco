using System;
using Xamarin.Forms;

namespace Tamagotchi
{
    public partial class App : Application
    {
        public static event Action OnStartEvent;
        public static event Action OnSleepEvent;
        public static event Action OnResumeEvent;

        public App()
        {
            DependencyService.RegisterSingleton<TimeManager>(new TimeManager());
            DependencyService.RegisterSingleton<DataStorer>(new DataStorer());

            MainPage = new NavigationPage(new MainPage())
            {
                //BackgroundColor = Color.Violet,
                BarBackgroundColor = Color.Violet,
            };

            InitializeComponent();
        }

        protected override void OnStart()
        {
            OnStartEvent?.Invoke();
        }

        // Wanneer je uit de app gaat. Kan zijn dat de applicatie gestopt is, dus sluit hier dingen af!
        protected override void OnSleep()
        {
            Console.WriteLine("SLEEP");
            OnSleepEvent?.Invoke();
        }

        // Vergelijkbaar met OnEnable, steeds als de app weer wordt opgebruikt
        protected override void OnResume()
        {
            OnResumeEvent?.Invoke();
        }
    }
}
