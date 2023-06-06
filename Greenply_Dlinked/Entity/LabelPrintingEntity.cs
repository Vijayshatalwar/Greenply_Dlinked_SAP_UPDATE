using Greenply.common1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenply.Greenply_Sample.Entity
{
    public class LabelPrintingEntity : BaseEntity
    {
        public int ID { get; set; }
        public string L_ID { get; set; }
        public string LocationCode { get; set; }
        public string PONumber { get; set; }
        public string MatCode { get; set; }
        public string QRCode { get; set; }
        public string StackQRCode { get; set; }
        public string MatStatus { get; set; }
        public string OldMatStatus { get; set; }
        public string VendorInvoice { get; set; }
        public string VendorInvoiceDate { get; set; }
        public string VendorCode { get; set; }
        public string IsQRCodePrinted { get; set; }
        public string IsQRCodeUsed { get; set; }
        public string IsStackPrinted { get; set; }
        public string IsStackUsed { get; set; }
        public string OldStackQRCode { get; set; }
        public string OldMatCode { get; set; }
        public string Status { get; set; }
        public string ReturnStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string PrintedBy { get; set; }
        public DateTime? PrintedOn { get; set; }
        public string QRCodeScanBy { get; set; }
        public DateTime? QRCodeScanOn { get; set; }
        public string MIGONo { get; set; }
        public string InspectionLotNo { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string RejectionCode { get; set; }
        public string QCStatus { get; set; }
        public string QCBy { get; set; }
        public DateTime? QCOn { get; set; }
        public string QCPostedStatus { get; set; }
        public string QCPostedBy { get; set; }
        public DateTime? QCPostedOn { get; set; }
        public string MtmOldQRCode { get; set; }
        public string MtmTransferBy { get; set; }
        public DateTime? MtmTransferOn { get; set; }
        public string BatchNo { get; set; }
        public string IsSAPPosted { get; set; }
        public string SAPPostMsg { get; set; }
        public string PostedBy { get; set; }
        public DateTime? PostedOn { get; set; }
        public string SentBy { get; set; }
        public DateTime? SentOn { get; set; }
        public string Server_Details { get; set; }
    }
    public class SuccessLogEntity
    {
        public int Location_Code { get; set; }
        public int SuccessCount { get; set; }
        public DateTime? StartOn { get; set; }
        public DateTime? EndOn { get; set; }
    }
    public class ErrorLogEntity
    {
        public int Location_Code { get; set; }
        public int TLocation_ID { get; set; }
        public string ErrorDetails { get; set; }
        public DateTime? StartOn { get; set; }
        public DateTime? EndOn { get; set; }
    }

}
