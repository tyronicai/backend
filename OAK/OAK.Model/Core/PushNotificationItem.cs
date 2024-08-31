namespace OAK.Model.Core
{
    public class PushNotificationItem
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Target { get; set; }
    }
}