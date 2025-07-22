using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebhookReceiver.Models
{
    public class webhookCommunicationModel
    {
        public string Sender { get; set; }
        public string CC { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public int AttachmentCount { get; set; }
        public string GUID { get; set; }
        public string PacketGUID { get; set; }
        public int CommunicationType { get; set; }
    }
}