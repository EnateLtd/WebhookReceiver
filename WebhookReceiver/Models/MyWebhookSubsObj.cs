using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebhookReceiver.Models
{
    public class MyWebhookSubsObj
    {
        public string Webhook { get; set; }
        public string FilterObjectGUID { get; set; }
        public string FilterObjectType { get; set; }
        public string SubscriberURL { get; set; }
        public string CustomHeader { get; set; }
        public string CustomHeaderValue { get; set; }
    }
}