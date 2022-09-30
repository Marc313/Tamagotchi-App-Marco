using Plugin.LocalNotification;
using System;
using System.Collections.Generic;

namespace Tamagotchi
{
    public static class NotificationManager
    {
        public static List<NotificationRequest> scheduledNotifications = new List<NotificationRequest>();

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

            scheduledNotifications.Add(notification);

            LocalNotificationCenter.Current.Show(notification);
        }

        public static void CancelAllNotifications()
        {
            foreach(NotificationRequest notification in scheduledNotifications)
            {
                notification.Cancel();
            }

            scheduledNotifications.Clear();
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
