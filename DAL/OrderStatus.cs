using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderStatus:Base
    {
        public int OrderStatusId { get; set; }

        public int? OrderId { get; set; }

        public int? CustomerId { get; set; }

        public int? ScriptAssignedUserId { get; set; }

        public string ScriptFileName { get; set; }

        public int? ScriptStatus { get; set; }

        public DateTime? ScriptAssignedDate { get; set; }

        public DateTime? ScriptEndDate { get; set; }

        public int? ScriptClientStatus { get; set; }

        public int? VOAssignedUserId { get; set; }

        public string VOFileName { get; set; }

        public int? VOStatus { get; set; }

        public DateTime? VOAssignedDate { get; set; }

        public DateTime? VOEndDate { get; set; }

        public int? VOClientStatus { get; set; }

        public int? VideoAssignedUserId { get; set; }

        public string VideoFileName { get; set; }

        public int? VideoStatus { get; set; }

        public DateTime? VideoAssignedDate { get; set; }

        public DateTime? VideoEndDate { get; set; }

        public int? VideoClientStatus { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDateDate { get; set; }
        public bool IsOrderCancel { get; set; }

        public DateTime? OrderCancelDate { get; set; }

        public double? ScriptUserCharges { get; set; }

        public double? VoiceOverCharges { get; set; }

        public double? VideoUserCharges { get; set; }



        public bool? ScriptIsInvoiceGenerated { get; set; }

        public DateTime? ScriptInvoiceGeneratedDate { get; set; }

        public bool? VoiceIsInvoiceGenerated { get; set; }

        public DateTime? VoiceInvoiceGeneratedDate { get; set; }

        public bool? VideoIsInvoiceGenerated { get; set; }

        public DateTime? VideoInvoiceGeneratedDate { get; set; }
        public string ScriptInvoiceNo { get; set; }

        public string VoiceInvoiceNo { get; set; }

        public string VideoInvoiceNo { get; set; }

        public bool? IsScriptPaymentCleared { get; set; }

        public bool? IsVoicePaymentCleared { get; set; }

        public bool? IsVideoPaymentCleared { get; set; }

        public DateTime? ScriptAmountClearedDate { get; set; }

        public DateTime? VoiceAmountClearedDate { get; set; }

        public DateTime? VideoAmountClearedDate { get; set; }
        public string ProjectFileLink { get; set; }



        public string ScriptFileName_1 { get; set; }
        public int? ScriptStatus_1 { get; set; }
        public string VOFileName_1 { get; set; }
        public int? VOStatus_1 { get; set; }
        public string VideoFileName_1 { get; set; }
        public int? VideoStatus_1 { get; set; }
        public string ScriptFileName_2 { get; set; }
        public int? ScriptStatus_2 { get; set; }
        public string VOFileName_2 { get; set; }
        public int? VOStatus_2 { get; set; }
        public string VideoFileName_2 { get; set; }
        public int? VideoStatus_2 { get; set; }
        public string ScriptFileName_3 { get; set; }
        public int? ScriptStatus_3 { get; set; }
        public string VOFileName_3 { get; set; }
        public int? VOStatus_3 { get; set; }
        public string VideoFileName_3 { get; set; }
        public int? VideoStatus_3 { get; set; }
        public int? ClientScriptStatus_1 { get; set; }

        public int? ClientVOStatus_1 { get; set; }

        public int? ClientVideoStatus_1 { get; set; }

        public int? ClientScriptStatus_2 { get; set; }

        public int? ClientVOStatus_2 { get; set; }

        public int? ClientVideoStatus_2 { get; set; }

        public int? ClientScriptStatus_3 { get; set; }

        public int? ClientVOStatus_3 { get; set; }

        public int? ClientVideoStatus_3 { get; set; }

    }

}
