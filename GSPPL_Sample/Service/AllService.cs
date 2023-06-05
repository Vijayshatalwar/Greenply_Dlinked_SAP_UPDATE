using GSPPL.common1.Helper;
using GSPPL.common1.Repository;
using GSPPL.GSPPL_Sample.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace GSPPL_Sample.Service
{
    public class AllService
    {

        #region Feilds
        public DBContext<LabelPrintingEntity> _LabelPrintingRepository;
        #endregion

        #region Constructor
        public AllService()
        {
            _LabelPrintingRepository = new DBContext<LabelPrintingEntity>();
            //_mailUserEntityRepository = new DBContext<MailUserEntity>();
        }
        #endregion
        public string GetLatestRecordFetched(Int64 PlantCode)
        {
            try
            {
                SqlCommand command = new SqlCommand("GetLatestRecordId");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@LocationCode", SqlDbType.BigInt).Value = PlantCode;

                return Convert.ToString(_LabelPrintingRepository.PExecuteProcedure(command));
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                LogHelper.LogError("Failed to execute GetLatestRecordId");
            }
            return null;
        }
        public Int64 GetPlantCode()
        {
            try
            {
                SqlCommand command = new SqlCommand("USP_GETPLANTCODE");
                command.CommandType = System.Data.CommandType.StoredProcedure;

                return Convert.ToInt64(_LabelPrintingRepository.ExecuteProcedure(command));
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                LogHelper.LogError("Failed to execute GetLatestRecordId");
            }
            return 0;
        }
        public List<LabelPrintingEntity> PullRedordsById(string LastExecutedId)
        {
            try
            {
                SqlCommand command = new SqlCommand("GetLatestRecordById");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@L_Id", SqlDbType.VarChar).Value = LastExecutedId;
                return _LabelPrintingRepository.GetRecords(command).ToList();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                LogHelper.LogError("Failed to execute PullRedordsById");
            }
            return null;
        }

        public List<LabelPrintingEntity> GetLatestRecordFetchedLasteHour()
        {
            try
            {
                SqlCommand command = new SqlCommand("GetLatestRecordFromLastHour");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                return _LabelPrintingRepository.GetRecords(command).ToList();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                LogHelper.LogError("Failed to execute GetLatestRecordFromLastHour");
            }
            return null;
        }
        public void InsertPrintingDetails(LabelPrintingEntity entity)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERTRecord");
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@L_ID", SqlDbType.VarChar).Value = entity.LocationCode + "_" + entity.ID;//TLocation_ID
                command.Parameters.Add("@TLocation_ID", SqlDbType.BigInt).Value = entity.ID;//TLocation_ID
                command.Parameters.Add("@LocationCode", SqlDbType.VarChar).Value = entity.LocationCode;
                if (entity.PONumber != null)
                    command.Parameters.Add("@PONumber", SqlDbType.VarChar).Value = entity.PONumber;
                else
                    command.Parameters.Add("@PONumber", SqlDbType.VarChar).Value = DBNull.Value;

                command.Parameters.Add("@MatCode", SqlDbType.VarChar).Value = entity.MatCode;
                command.Parameters.Add("@QRCode", SqlDbType.VarChar).Value = entity.QRCode;
                if (entity.StackQRCode != null)
                    command.Parameters.Add("@StackQRCode", SqlDbType.VarChar).Value = entity.StackQRCode;
                else
                    command.Parameters.Add("@StackQRCode", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.MatStatus != null)
                    command.Parameters.Add("@MatStatus", SqlDbType.VarChar).Value = entity.MatStatus;
                else
                    command.Parameters.Add("@MatStatus", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.OldMatStatus != null)
                    command.Parameters.Add("@OldMatStatus", SqlDbType.VarChar).Value = entity.OldMatStatus;
                else
                    command.Parameters.Add("@OldMatStatus", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.VendorInvoice != null)
                    command.Parameters.Add("@VendorInvoice", SqlDbType.VarChar).Value = entity.VendorInvoice;
                else
                    command.Parameters.Add("@VendorInvoice", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.VendorInvoiceDate != null)
                    command.Parameters.Add("@VendorInvoiceDate", SqlDbType.VarChar).Value = entity.VendorInvoiceDate;
                else
                    command.Parameters.Add("@VendorInvoiceDate", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.VendorCode != null)
                    command.Parameters.Add("@VendorCode", SqlDbType.VarChar).Value = entity.VendorCode;
                else
                    command.Parameters.Add("@VendorCode", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.IsQRCodePrinted != null)
                    command.Parameters.Add("@IsQRCodePrinted", SqlDbType.VarChar).Value = entity.IsQRCodePrinted;
                else
                    command.Parameters.Add("@IsQRCodePrinted", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.IsQRCodeUsed != null)
                    command.Parameters.Add("@IsQRCodeUsed", SqlDbType.VarChar).Value = entity.IsQRCodeUsed;
                else
                    command.Parameters.Add("@IsQRCodeUsed", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.IsStackPrinted != null)
                    command.Parameters.Add("@IsStackPrinted", SqlDbType.VarChar).Value = entity.IsStackPrinted;
                else
                    command.Parameters.Add("@IsStackPrinted", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.IsStackUsed != null)
                    command.Parameters.Add("@IsStackUsed", SqlDbType.VarChar).Value = entity.IsStackUsed;
                else
                    command.Parameters.Add("@IsStackUsed", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.OldStackQRCode != null)
                    command.Parameters.Add("@OldStackQRCode", SqlDbType.VarChar).Value = entity.OldStackQRCode;
                else
                    command.Parameters.Add("@OldStackQRCode", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.OldMatCode != null)
                    command.Parameters.Add("@OldMatCode", SqlDbType.VarChar).Value = entity.OldMatCode;
                else
                    command.Parameters.Add("@OldMatCode", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.Status != null)
                    command.Parameters.Add("@Status", SqlDbType.VarChar).Value = entity.Status;
                else
                    command.Parameters.Add("@Status", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.ReturnStatus != null)
                    command.Parameters.Add("@ReturnStatus", SqlDbType.VarChar).Value = entity.ReturnStatus;
                else
                    command.Parameters.Add("@ReturnStatus", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.CreatedBy != null)
                    command.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = entity.CreatedBy;
                else
                    command.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.CreatedOn != null)
                    command.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = entity.CreatedOn;
                else
                    command.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = DBNull.Value;


                if (entity.PrintedBy != null)
                    command.Parameters.Add("@PrintedBy", SqlDbType.VarChar).Value = entity.PrintedBy;
                else
                    command.Parameters.Add("@PrintedBy", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.PrintedOn != null)
                    command.Parameters.Add("@PrintedOn", SqlDbType.DateTime).Value = entity.PrintedOn;
                else
                    command.Parameters.Add("@PrintedOn", SqlDbType.DateTime).Value = DBNull.Value;

                if (entity.QRCodeScanBy != null)
                    command.Parameters.Add("@QRCodeScanBy", SqlDbType.VarChar).Value = entity.QRCodeScanBy;
                else
                    command.Parameters.Add("@QRCodeScanBy", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.QRCodeScanOn != null)
                    command.Parameters.Add("@QRCodeScanOn", SqlDbType.DateTime).Value = entity.QRCodeScanOn;
                else
                    command.Parameters.Add("@QRCodeScanOn", SqlDbType.DateTime).Value = DBNull.Value;

                if (entity.MIGONo != null)
                    command.Parameters.Add("@MIGONo", SqlDbType.VarChar).Value = entity.MIGONo;
                else
                    command.Parameters.Add("@MIGONo", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.InspectionLotNo != null)
                    command.Parameters.Add("@InspectionLotNo", SqlDbType.VarChar).Value = entity.InspectionLotNo;
                else
                    command.Parameters.Add("@InspectionLotNo", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.UpdateOn != null)
                    command.Parameters.Add("@UpdateOn", SqlDbType.DateTime).Value = entity.UpdateOn;
                else
                    command.Parameters.Add("@UpdateOn", SqlDbType.DateTime).Value = DBNull.Value;

                if (entity.RejectionCode != null)
                    command.Parameters.Add("@RejectionCode", SqlDbType.VarChar).Value = entity.RejectionCode;
                else
                    command.Parameters.Add("@RejectionCode", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.QCStatus != null)
                    command.Parameters.Add("@QCStatus", SqlDbType.VarChar).Value = entity.QCStatus;
                else
                    command.Parameters.Add("@QCStatus", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.QCBy != null)
                    command.Parameters.Add("@QCBy", SqlDbType.VarChar).Value = entity.QCBy;
                else
                    command.Parameters.Add("@QCBy", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.QCOn != null)
                    command.Parameters.Add("@QCOn", SqlDbType.DateTime).Value = entity.QCOn;
                else
                    command.Parameters.Add("@QCOn", SqlDbType.DateTime).Value = DBNull.Value;

                if (entity.QCPostedStatus != null)
                    command.Parameters.Add("@QCPostedStatus", SqlDbType.VarChar).Value = entity.QCPostedStatus;
                else
                    command.Parameters.Add("@QCPostedStatus", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.QCPostedBy != null)
                    command.Parameters.Add("@QCPostedBy", SqlDbType.VarChar).Value = entity.QCPostedBy;
                else
                    command.Parameters.Add("@QCPostedBy", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.QCPostedOn != null)
                    command.Parameters.Add("@QCPostedOn", SqlDbType.DateTime).Value = entity.QCPostedOn;
                else
                    command.Parameters.Add("@QCPostedOn", SqlDbType.DateTime).Value = DBNull.Value;

                if (entity.MtmOldQRCode != null)
                    command.Parameters.Add("@MtmOldQRCode", SqlDbType.VarChar).Value = entity.MtmOldQRCode;
                else
                    command.Parameters.Add("@MtmOldQRCode", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.MtmTransferBy != null)
                    command.Parameters.Add("@MtmTransferBy", SqlDbType.VarChar).Value = entity.MtmTransferBy;
                else
                    command.Parameters.Add("@MtmTransferBy", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.MtmTransferOn != null)
                    command.Parameters.Add("@MtmTransferOn", SqlDbType.DateTime).Value = entity.MtmTransferOn;
                else
                    command.Parameters.Add("@MtmTransferOn", SqlDbType.DateTime).Value = DBNull.Value;

                if (entity.BatchNo != null)
                    command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = entity.BatchNo;
                else
                    command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.IsSAPPosted != null)
                    command.Parameters.Add("@IsSAPPosted", SqlDbType.VarChar).Value = entity.IsSAPPosted;
                else
                    command.Parameters.Add("@IsSAPPosted", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.SAPPostMsg != null)
                    command.Parameters.Add("@SAPPostMsg", SqlDbType.VarChar).Value = entity.SAPPostMsg;
                else
                    command.Parameters.Add("@SAPPostMsg", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.PostedBy != null)
                    command.Parameters.Add("@PostedBy", SqlDbType.VarChar).Value = entity.PostedBy;
                else
                    command.Parameters.Add("@PostedBy", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.PostedOn != null)
                    command.Parameters.Add("@PostedOn", SqlDbType.DateTime).Value = entity.PostedOn;
                else
                    command.Parameters.Add("@PostedOn", SqlDbType.DateTime).Value = DBNull.Value;

                if (entity.SentBy != null)
                    command.Parameters.Add("@SentBy", SqlDbType.VarChar).Value = entity.SentBy;
                else
                    command.Parameters.Add("@SentBy", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.SentOn != null)
                    command.Parameters.Add("@SentOn", SqlDbType.DateTime).Value = entity.SentOn;
                else
                    command.Parameters.Add("@SentOn", SqlDbType.DateTime).Value = DBNull.Value;

                _LabelPrintingRepository.PExecuteNonQueryProc(command);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                LogHelper.LogError("Failed to execute PullLabelPrintingData");
            }
        }

        public void InsertSuccessLog(SuccessLogEntity entity)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERTSuccesLog");
                command.CommandType = System.Data.CommandType.StoredProcedure;

                if (entity.Location_Code > 0)
                    command.Parameters.Add("@Location_Code", SqlDbType.BigInt).Value = entity.Location_Code;
                else
                    command.Parameters.Add("@Location_Code", SqlDbType.BigInt).Value = DBNull.Value;
                
                if (entity.SuccessCount > 0)
                    command.Parameters.Add("@SuccessCount", SqlDbType.VarChar).Value = entity.SuccessCount;
                else
                    command.Parameters.Add("@SuccessCount", SqlDbType.VarChar).Value = DBNull.Value;    

                if (entity.StartOn != null)
                    command.Parameters.Add("@StartOn", SqlDbType.DateTime).Value = entity.StartOn;
                else
                    command.Parameters.Add("@StartOn", SqlDbType.DateTime).Value = DBNull.Value;

                if (entity.EndOn != null)
                    command.Parameters.Add("@EndOn", SqlDbType.DateTime).Value = entity.EndOn;
                else
                    command.Parameters.Add("@EndOn", SqlDbType.DateTime).Value = DBNull.Value;

                _LabelPrintingRepository.PExecuteNonQueryProc(command);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                LogHelper.LogError("Failed to execute InsertSuccessLog");
            }
        }
        public void INSERTErrorLog(ErrorLogEntity entity)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERTErrorLog");
                command.CommandType = System.Data.CommandType.StoredProcedure;

                if (entity.Location_Code > 0)
                    command.Parameters.Add("@Location_Code", SqlDbType.BigInt).Value = entity.Location_Code;
                else
                    command.Parameters.Add("@Location_Code", SqlDbType.BigInt).Value = DBNull.Value;

                if (entity.TLocation_ID > 0)
                    command.Parameters.Add("@SuccessCount", SqlDbType.BigInt).Value = entity.TLocation_ID;
                else
                    command.Parameters.Add("@SuccessCount", SqlDbType.BigInt).Value = DBNull.Value;

                if (string.IsNullOrWhiteSpace(entity.ErrorDetails))
                    command.Parameters.Add("@ErrorDetails", SqlDbType.VarChar).Value = entity.ErrorDetails;
                else
                    command.Parameters.Add("@ErrorDetails", SqlDbType.VarChar).Value = DBNull.Value;

                if (entity.StartOn != null)
                    command.Parameters.Add("@StartOn", SqlDbType.DateTime).Value = entity.StartOn;
                else
                    command.Parameters.Add("@StartOn", SqlDbType.DateTime).Value = DBNull.Value;

                if (entity.EndOn != null)
                    command.Parameters.Add("@EndOn", SqlDbType.DateTime).Value = entity.EndOn;
                else
                    command.Parameters.Add("@EndOn", SqlDbType.DateTime).Value = DBNull.Value;

                _LabelPrintingRepository.PExecuteNonQueryProc(command);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                LogHelper.LogError("Failed to execute INSERTErrorLog");
            }
        }
    }
}
