using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.ViewModel
{
    public class vmFreelancerInvoice
    {
        public int OrderId { get; set; }
        public double? Amount { get; set; }

        public string YourName { get; set; }

        public string YourAddress { get; set; }
        public string YourEmail { get; set; }
        public string YourPanCard { get; set; }

        public string YourContactDetails { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public string InoviceNo { get; set; }

        public int? ProjectOrderNo { get; set; }

        public string BillTo { get; set; }

        public string ProjectDuration { get; set; }
        public bool IsInvoiceGenerated { get; set; }
      
    }
}