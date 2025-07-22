using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebhookReceiver.Models
{
    public class MyObj
    {
      
            public string CustomerName { get; set; }
            public string SupplierName { get; set; }
            public string ContractName { get; set; }
            public string ServiceName { get; set; }
            public string ServiceLineName { get; set; }
            public string ProcessTypeName { get; set; }
            public string Reference { get; set; }
            public string Title { get; set; }
            public int Status { get; set; }
            public string DueDate { get; set; }
            public string ResolvedOn { get; set; }
            public int RAGStatus { get; set; }
            public int ProcessType { get; set; }
            public int StartedByMethod { get; set; }
            public string GUID { get; set; }

    }
}