using Plugin.LocalNotification;
using System;

namespace Tamagotchi
{
    public static class NotificationManager
    {
        public static void SendNotification(string description, string title = default)
        {
            NotificationRequest notification = CreateNotification(description, title);
            LocalNotificationCenter.Current.Show(notification);
        }

        public static void ScheduleNotificationAfterSeconds(string description, double secondsToNotification, string title = default)
        {
            NotificationRequest notification = CreateNotification(description, title);

            notification.Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(secondsToNotification)
            };

            LocalNotificationCenter.Current.Show(notification);
        }

        private static NotificationRequest CreateNotification(string description, string title)
        {
            // Give title a default value of 'Your tamagotchi needs you'
            if (title == default) title = "Your tamagotchi needs you!";

            return new NotificationRequest
            {
                Title = title,
                Description = description,                
            };
        }

    }
}
