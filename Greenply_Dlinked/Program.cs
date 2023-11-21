using Greenply.common1.Helper;
using Greenply.Dlinked_SAP_UPDATE.Service;
using Greenply.Greenply_Sample.Entity;
using Greenply_Sample.Service;
using System.Collections.Generic;
using System.Threading;

AllService service = new AllService();

try
{
    DateTime StartOn = DateTime.Now;
    DateTime EndOn = DateTime.Now;
    LogHelper.InitLog();
    LogHelper.LogMessage("Main Method Start" + DateTime.Now);

    List<LabelPrintingEntity> DataModel = new List<LabelPrintingEntity>();
    List<LabelPrintingEntity> DataModelK_Decor = new List<LabelPrintingEntity>();
    List<LabelPrintingEntity> DataModelK_Door = new List<LabelPrintingEntity>();
    List<LabelPrintingEntity> DataModelK_Ply = new List<LabelPrintingEntity>();
    List<LabelPrintingEntity> DataModel_TIZIT = new List<LabelPrintingEntity>();
    List<LabelPrintingEntity> DataModel_Sandila = new List<LabelPrintingEntity>();

    List<LabelPrintingEntity> DataModel_KHUB = new List<LabelPrintingEntity>();
    List<LabelPrintingEntity> DataModel_BHUB = new List<LabelPrintingEntity>();
    List<LabelPrintingEntity> DataModel_YHUB = new List<LabelPrintingEntity>();

    try
    {
        StartOn = DateTime.Now;
        string Server = "";

        #region 10.1.1.214
        //10.1.1.214
        LogHelper.LogSuccess("Starting Failed Records Updates: 10.1.1.214");

        try
        {

            DataModel = service.GetSAPFailedRecord();

            if (DataModel != null && DataModel.Count != 0)
            {
                foreach (LabelPrintingEntity Data in DataModel)
                {
                    try
                    {
                        Server = "10.1.1.214";
                        service.FetchSAP_Status(Data.Id, Data.LocationCode, Data.MatCode, Data.QRCode, Data.LocationCode + "_" + Data.ID);
                    }
                    catch (Exception e)
                    {
                        ErrorLogEntity newError = new ErrorLogEntity();
                        newError.Location_Code = Convert.ToInt32(Data.LocationCode);
                        newError.TLocation_ID = Convert.ToInt32(Data.ID);
                        newError.ErrorDetails = e.Message;
                        newError.StartOn = StartOn;
                        newError.EndOn = DateTime.Now;
                        service.INSERTErrorLog(newError);
                        LogHelper.LogError("Loop Error: " + DateTime.Now + "-" + e);
                    }
                }
                LogHelper.LogSuccess("Total Data Fetched for 10.1.1.214 on : " + DateTime.Now + ", Count : " + DataModel.Count);

            }
            else
            {
                LogHelper.LogSuccess("No data Found To Fetch : " + DateTime.Now);
                LogHelper.LogSuccess("Total Data Fetched for 10.1.1.214 on : " + DateTime.Now + ", Count : " + 0);
            }
            if (DataModel != null)
            {
                SuccessLogEntity newSuccess = new SuccessLogEntity();
                newSuccess.Location_Code = 000;
                newSuccess.SuccessCount = Convert.ToInt32(DataModel.Count);
                newSuccess.StartOn = StartOn;
                newSuccess.EndOn = DateTime.Now;
                service.InsertSuccessLog(newSuccess);
            }
        }
        catch (Exception e)
        {
            ErrorLogEntity newError = new ErrorLogEntity();
            newError.Location_Code = Convert.ToInt32(000);
            newError.TLocation_ID = Convert.ToInt32(000);
            newError.ErrorDetails = e.Message;
            newError.StartOn = StartOn;
            newError.EndOn = DateTime.Now;
            service.INSERTErrorLog(newError);
            LogHelper.LogError("Loop Error: " + DateTime.Now + "-" + e);
        }
        #endregion
    }
    catch (Exception e)
    {
        ErrorLogEntity newError = new ErrorLogEntity();
        LogHelper.LogError("Loop Error: " + DateTime.Now + "-" + e);
    }
    LogHelper.LogMessage("Main Method End" + DateTime.Now);
}
catch (Exception e)
{
    LogHelper.LogError("Error: " + DateTime.Now + "-" + e);
}
