using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebhookReceiver.Models
{
    public class WebhookSubscribeModel
    {
        public string GUID { get; set; }
        public string Webhook { get; set; }
        public string FilterObjectGUID { get; set; }
        public string FilterObjectType { get; set; }
        public string SubscriberURL { get; set; }
        public string Signature { get; set; }
        public string CustomHeader { get; set; }
        public string CustomHeaderValue { get; set; }
    }

    public class Root
    {
        public WebhookSubscribeModel Result { get; set; }
    }
}