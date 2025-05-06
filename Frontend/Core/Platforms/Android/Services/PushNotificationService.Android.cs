#if ANDROID
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using AndroidX.Core.App;
using System;

namespace Core.Platforms.Android.Services
{
    public static class PushNotificationService
    {
        public static void ShowNotification(string title, string message, string redirectRoute = "")
        {
            var context = Android.MainApplication.Context;
            var channelId = CreateNotificationChannel(context);


            var intent = new Intent(context, typeof(MainActivity));
            if (!string.IsNullOrEmpty(redirectRoute))
                intent.PutExtra("route", redirectRoute);

            intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            var pendingIntent = PendingIntent.GetActivity(
                        context,
                        0,
                        intent,
                        PendingIntentFlags.Immutable | PendingIntentFlags.UpdateCurrent
                    );


            var builder = new NotificationCompat.Builder(context, channelId)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.notification_template_icon_bg)
                .SetContentIntent(pendingIntent)
                .SetAutoCancel(true)
                .SetPriority((int)NotificationPriority.High);

            var notificationManager = NotificationManagerCompat.From(context);
            notificationManager.Notify(new Random().Next(), builder.Build());
        }

        private static string CreateNotificationChannel(Context context)
        {
            const string channelId = "push_channel_id";

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O) // O = Android 8.0 = API 26
            {
                var channel = new NotificationChannel(channelId, "Push Notifications", NotificationImportance.High)
                {
                    Description = "Channel for push notifications"
                };

                var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
                notificationManager.CreateNotificationChannel(channel);
            }

            return channelId;
        }
    }
}
#endif