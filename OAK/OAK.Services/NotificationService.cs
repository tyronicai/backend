using CorePush.Firebase;
using CorePush.Serialization;
using Microsoft.Extensions.Options;
using OAK.Model.ConfigurationModels;
using OAK.Model.Core;
using OAK.ServiceContracts;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
namespace OAK.Services
{
    public class NotificationService : INotificationService
    {
       
        public NotificationSettings NotificationSettings { get; }
        private static readonly HttpClient http = new();
        String contents;

        public NotificationService(IOptions<NotificationSettings> notificationSettings)
        {
            NotificationSettings = notificationSettings?.Value ?? throw new ArgumentException("notificationSettings");
        }


        public static async Task SendNotification(PushNotificationItem pushNotificationItem)
        {
            var contents = File.ReadAllTextAsync("./Documents/Keyfiles/umzug-e3a96-0d8bf4781898.json");
            var serializer = new DefaultCorePushJsonSerializer();
            var settings = serializer.Deserialize<FirebaseSettings>(await contents);

            var fcm = new FirebaseSender(settings, http);
            var payload = new
            {
                message = new
                {
                    token = pushNotificationItem.Token,
                    notification = new
                    {
                        title = pushNotificationItem.Title,
                        body = pushNotificationItem.Body
                    }
                }
            };

            await fcm.SendAsync(payload);

        }

    }
}