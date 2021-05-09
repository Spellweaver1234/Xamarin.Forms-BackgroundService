using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly:Xamarin.Forms.Dependency(typeof(BackgroundService.Droid.DemoService))]
namespace BackgroundService.Droid
{
    [Service(ForegroundServiceType = Android.Content.PM.ForegroundService.TypeDataSync)]
    class DemoService : Service, IDemoService
    {
        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            var startIntent = new Intent(MainActivity.ActivityCurrent, typeof(DemoService));
            startIntent.SetAction(ServiceActions.Start.ToString());
            MainActivity.ActivityCurrent.StartService(startIntent);
        }

        public void Stop()
        {
            var stopIntent = new Intent(MainActivity.ActivityCurrent, typeof(DemoService));
            stopIntent.SetAction(ServiceActions.Stop.ToString());
            MainActivity.ActivityCurrent.StartService(stopIntent);
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            if (intent.Action==ServiceActions.Start.ToString())
            {
                System.Diagnostics.Debug.WriteLine("Start service");
                RegisterNotification();
            }
            else if (intent.Action== ServiceActions.Stop.ToString())
            {
                System.Diagnostics.Debug.WriteLine("Stop service");
                StopForeground(true);
                StopSelfResult(startId);
            }

            return StartCommandResult.NotSticky;
        }

        private void RegisterNotification()
        {
            var channel = new NotificationChannel("ServiceChannel", "Demo", NotificationImportance.Max);
            var manager = (NotificationManager)MainActivity.ActivityCurrent.GetSystemService(NotificationService);
            manager.CreateNotificationChannel(channel);

            var notification = new Notification.Builder(this, "ServiceChannel")
                .SetContentTitle("Service")
                .SetSmallIcon(Resource.Drawable.abc_ic_star_black_16dp)
                .SetOngoing(true)
                .Build();

            StartForeground(100, notification);
        }
    }
}