using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebhookReceiver.Models
{
    public class addPacketCommunicatioModel
    {
        public string Body { get; set; }
        public int CommunicationType { get; set; }
        public bool IsResolutionCommunication { get; set; }
    }
}