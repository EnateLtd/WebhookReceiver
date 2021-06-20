using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebhookReceiver.Models
{
    public class getcommunication
    {
        public string To { get; set; }
        public string CCs { get; set; }
        public string BCCs { get; set; }
        public string HTMLBody { get; set; }
        public List<Attachment> Attachments { get; set; }
        public DateTime? ResolutionTime { get; set; }
      //  public DateTime ResolutionTime { get; set; }
        public string GUID { get; set; }
        public string From { get; set; }
        public DateTime ? Logged { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Read { get; set; }
        public int AttachmentCount { get; set; }
        public bool IsSystemGenerated { get; set; }
        public FromUser FromUser { get; set; }
        public string Importance { get; set; }
        public string ImportanceDescription { get; set; }
        public string CommunicationScope { get; set; }
        public string CommunicationScopeDescription { get; set; }
        public string Type { get; set; }
        public string TypeDescription { get; set; }
        public bool IsResolutionCommunication { get; set; }
        public bool Sent { get; set; }
    }

    public class Attachment
    {
        public string GUID { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public string AttachmentType { get; set; }
        public string Note { get; set; }
    }

    public class Employee
    {
        public string EntityGUID { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public bool HasProfilePicture { get; set; }
        public string UserType { get; set; }
        public bool IsMe { get; set; }
        public bool Retired { get; set; }
        public string GUID { get; set; }
    }

    public class Tag
    {
        public string GUID { get; set; }
        public string Name { get; set; }
    }

    public class FromUser
    {
        public Employee Employee { get; set; }
        public List<Tag> Tags { get; set; }
        public string EmailAddress { get; set; }
    }

    
}