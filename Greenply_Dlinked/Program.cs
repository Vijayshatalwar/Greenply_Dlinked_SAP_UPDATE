using Greenply.common1.Helper;
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
        #region 172.16.3.121
        //172.16.3.121
        LogHelper.LogSuccess("Starting Server : 172.16.3.121");

        Int64 PlantCode = service.GetPlantCode();
        string LastRecordId = service.GetLatestRecordFetched(PlantCode);
        if (!string.IsNullOrWhiteSpace(LastRecordId))
        {
            DataModel = service.PullRedordsById(LastRecordId);
        }
        else
        {
            DataModel = service.GetLatestRecordFetchedLasteHour();
            //datamodel
        }
        if (DataModel != null && DataModel.Count != 0)
        {
            foreach (LabelPrintingEntity Data in DataModel)
            {
                try
                {
                    Server = "172.16.3.121";
                    service.InsertPrintingDetails(Data, Server);
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
            LogHelper.LogSuccess("Total Data Fetched for 172.16.3.121 on : " + DateTime.Now + ", Count : " + DataModel.Count);

        }
        else
        {
            LogHelper.LogSuccess("No data Found To Fetch : " + DateTime.Now);
            LogHelper.LogSuccess("Total Data Fetched for 172.16.3.121 on : " + DateTime.Now + ", Count : " + 0);
        }
        if (DataModel != null)
        {
            SuccessLogEntity newSuccess = new SuccessLogEntity();
            newSuccess.Location_Code = Convert.ToInt32(PlantCode);
            newSuccess.SuccessCount = Convert.ToInt32(DataModel.Count);
            newSuccess.StartOn = StartOn;
            newSuccess.EndOn = DateTime.Now;
            service.InsertSuccessLog(newSuccess);
        }
        #endregion

        #region 172.16.2.126
        //172.16.2.126
        LogHelper.LogSuccess("Starting Server : 172.16.2.126");

        Int64 PlantCodeK_Decor = service.GetPlantCodeK_Decor();
        string LastRecordIdK_Decor = service.GetLatestRecordFetcheWithServer(PlantCodeK_Decor, "172.16.2.126");
        if (!string.IsNullOrWhiteSpace(LastRecordIdK_Decor))
        {
            DataModelK_Decor = service.PullRedordsByIdK_Decor(LastRecordIdK_Decor);
        }
        else
        {
            DataModelK_Decor = service.GetLatestRecordFetchedLasteHourK_Decor();
            //DataModelK_Decor
        }
        if (DataModelK_Decor != null && DataModelK_Decor.Count != 0)
        {
            foreach (LabelPrintingEntity Data in DataModelK_Decor)
            {
                try
                {
                    Server = "172.16.2.126";
                    service.InsertPrintingDetails(Data, Server);
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
            LogHelper.LogSuccess("Total Data Fetched for 172.16.2.126 on : " + DateTime.Now + ", Count : " + DataModelK_Decor.Count);
        }
        else
        {
            LogHelper.LogSuccess("No data Found To Fetch : " + DateTime.Now);
            LogHelper.LogSuccess("Total Data Fetched for 172.16.2.126 on : " + DateTime.Now + ", Count : " + 0);
        }
        if (DataModelK_Decor != null)
        {
            SuccessLogEntity newSuccessK_Decor = new SuccessLogEntity();
            newSuccessK_Decor.Location_Code = Convert.ToInt32(PlantCodeK_Decor);
            newSuccessK_Decor.SuccessCount = Convert.ToInt32(DataModelK_Decor.Count);
            newSuccessK_Decor.StartOn = StartOn;
            newSuccessK_Decor.EndOn = DateTime.Now;
            service.InsertSuccessLog(newSuccessK_Decor);
        }
        #endregion

        #region 172.16.4.121
        //172.16.4.121
        LogHelper.LogSuccess("Starting Server : 172.16.4.121");

        Int64 PlantCode_TIZIT = service.GetPlantCode_TIZIT();
        string LastRecordId_TIZIT = service.GetLatestRecordFetched(PlantCode_TIZIT);
        if (!string.IsNullOrWhiteSpace(LastRecordId_TIZIT))
        {
            DataModel_TIZIT = service.PullRedordsById_TIZIT(LastRecordId_TIZIT);
        }
        else
        {
            DataModel_TIZIT = service.GetLatestRecordFetchedLasteHour_TIZIT();
            //DataModel_TIZIT
        }
        if (DataModel_TIZIT != null && DataModel_TIZIT.Count != 0)
        {
            foreach (LabelPrintingEntity Data in DataModel_TIZIT)
            {
                try
                {
                    Server = "172.16.4.121";
                    service.InsertPrintingDetails(Data, Server);
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
            LogHelper.LogSuccess("Total Data Fetched for 172.16.4.121 on : " + DateTime.Now + ", Count : " + DataModel_TIZIT.Count);
        }
        else
        {
            LogHelper.LogSuccess("No data Found To Fetch : " + DateTime.Now);
            LogHelper.LogSuccess("Total Data Fetched for 172.16.4.121 on : " + DateTime.Now + ", Count : " + 0);
        }
        if (DataModel_TIZIT != null)
        {
            SuccessLogEntity newSuccess_TIZIT = new SuccessLogEntity();
            newSuccess_TIZIT.Location_Code = Convert.ToInt32(PlantCode_TIZIT);
            newSuccess_TIZIT.SuccessCount = Convert.ToInt32(DataModel_TIZIT.Count);
            newSuccess_TIZIT.StartOn = StartOn;
            newSuccess_TIZIT.EndOn = DateTime.Now;
            service.InsertSuccessLog(newSuccess_TIZIT);

        }
        #endregion

        #region 172.16.11.121
        //172.16.11.121
        LogHelper.LogSuccess("Starting Server : 172.16.11.121");

        Int64 PlantCode_Sandila = service.GetPlantCode_Sandila();
        string LastRecordId_Sandila = service.GetLatestRecordFetched(PlantCode_Sandila);
        if (!string.IsNullOrWhiteSpace(LastRecordId_Sandila))
        {
            DataModel_Sandila = service.PullRedordsById_Sandila(LastRecordId_Sandila);
        }
        else
        {
            DataModel_Sandila = service.GetLatestRecordFetchedLasteHour_Sandila();
            //DataModel_Sandila
        }
        if (DataModel_Sandila != null && DataModel_Sandila.Count != 0)
        {
            foreach (LabelPrintingEntity Data in DataModel_Sandila)
            {
                try
                {
                    Server = "172.16.11.121";
                    service.InsertPrintingDetails(Data, Server);
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
            LogHelper.LogSuccess("Total Data Fetched for 172.16.11.121 on : " + DateTime.Now + ", Count : " + DataModel_Sandila.Count);
        }
        else
        {
            LogHelper.LogSuccess("No data Found To Fetch : " + DateTime.Now);
            LogHelper.LogSuccess("Total Data Fetched for 172.16.11.121 on : " + DateTime.Now + ", Count : " + 0);
        }
        if (DataModel_Sandila != null)
        {
            SuccessLogEntity newSuccess_Sandila = new SuccessLogEntity();
            newSuccess_Sandila.Location_Code = Convert.ToInt32(PlantCode_Sandila);
            newSuccess_Sandila.SuccessCount = Convert.ToInt32(DataModel_Sandila.Count);
            newSuccess_Sandila.StartOn = StartOn;
            newSuccess_Sandila.EndOn = DateTime.Now;
            service.InsertSuccessLog(newSuccess_Sandila);
        }
        #endregion


        //----------------------------------------------------HUB
        #region 172.16.7.122
        //172.16.7.122
        LogHelper.LogSuccess("Starting Server : 172.16.7.122");

        Int64 PlantCode_KHUB = service.GetPlantCode_KHUB();
        string LastRecordId_KHUB = service.GetLatestRecordFetched(PlantCode_KHUB);
        if (!string.IsNullOrWhiteSpace(LastRecordId_KHUB))
        {
            DataModel_KHUB = service.PullRedordsById_KHUB(LastRecordId_KHUB);
        }
        else
        {
            DataModel_KHUB = service.GetLatestRecordFetchedLasteHour_KHUB();
            //DataModel_KHUB
        }
        if (DataModel_KHUB != null && DataModel_KHUB.Count != 0)
        {
            foreach (LabelPrintingEntity Data in DataModel_KHUB)
            {
                try
                {
                    Server = "172.16.7.122";
                    service.InsertPrintingDetails(Data, Server);
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
            LogHelper.LogSuccess("Total Data Fetched for 172.16.7.122 on : " + DateTime.Now + ", Count : " + DataModel_KHUB.Count);
        }
        else
        {
            LogHelper.LogSuccess("No data Found To Fetch : " + DateTime.Now);
            LogHelper.LogSuccess("Total Data Fetched for 172.16.7.122 on : " + DateTime.Now + ", Count : " + 0);
        }
        if (DataModel_KHUB != null)
        {
            SuccessLogEntity newSuccess_KHUB = new SuccessLogEntity();
            newSuccess_KHUB.Location_Code = Convert.ToInt32(PlantCode_KHUB);
            newSuccess_KHUB.SuccessCount = Convert.ToInt32(DataModel_KHUB.Count);
            newSuccess_KHUB.StartOn = StartOn;
            newSuccess_KHUB.EndOn = DateTime.Now;
            service.InsertSuccessLog(newSuccess_KHUB);
        }
        #endregion

        #region 172.16.6.122
        //172.16.6.122
        LogHelper.LogSuccess("Starting Server : 172.16.6.122");

        Int64 PlantCode_YHUB = service.GetPlantCode_YHUB();
        string LastRecordId_YHUB = service.GetLatestRecordFetched(PlantCode_YHUB);
        if (!string.IsNullOrWhiteSpace(LastRecordId_YHUB))
        {
            DataModel_YHUB = service.PullRedordsById_YHUB(LastRecordId_YHUB);
        }
        else
        {
            DataModel_YHUB = service.GetLatestRecordFetchedLasteHour_YHUB();
            //DataModel_YHUB
        }
        if (DataModel_YHUB != null && DataModel_YHUB.Count != 0)
        {
            foreach (LabelPrintingEntity Data in DataModel_YHUB)
            {
                try
                {
                    Server = "172.16.6.122";
                    service.InsertPrintingDetails(Data, Server);
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
            LogHelper.LogSuccess("Total Data Fetched for 172.16.6.122 on : " + DateTime.Now + ", Count : " + DataModel_YHUB.Count);
        }
        else
        {
            LogHelper.LogSuccess("No data Found To Fetch : " + DateTime.Now);
            LogHelper.LogSuccess("Total Data Fetched for 172.16.6.122 on : " + DateTime.Now + ", Count : " + 0);
        }
        if (DataModel_YHUB != null)
        {
            SuccessLogEntity newSuccess_YHUB = new SuccessLogEntity();
            newSuccess_YHUB.Location_Code = Convert.ToInt32(PlantCode_YHUB);
            newSuccess_YHUB.SuccessCount = Convert.ToInt32(DataModel_YHUB.Count);
            newSuccess_YHUB.StartOn = StartOn;
            newSuccess_YHUB.EndOn = DateTime.Now;
            service.InsertSuccessLog(newSuccess_YHUB);
        }
        #endregion

        #region 172.16.8.122
        //172.16.8.122
        LogHelper.LogSuccess("Starting Server : 172.16.8.122");

        Int64 PlantCode_BHUB = service.GetPlantCode_BHUB();
        string LastRecordId_BHUB = service.GetLatestRecordFetched(PlantCode_BHUB);
        if (!string.IsNullOrWhiteSpace(LastRecordId_BHUB))
        {
            DataModel_BHUB = service.PullRedordsById_BHUB(LastRecordId_BHUB);
        }
        else
        {
            DataModel_BHUB = service.GetLatestRecordFetchedLasteHour_BHUB();
            //DataModel_BHUB
        }
        if (DataModel_BHUB != null && DataModel_BHUB.Count != 0)
        {
            foreach (LabelPrintingEntity Data in DataModel_BHUB)
            {
                try
                {
                    Server = "172.16.8.122";
                    service.InsertPrintingDetails(Data, Server);
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
            LogHelper.LogSuccess("Total Data Fetched for 172.16.2.131 on : " + DateTime.Now + ", Count : " + DataModelK_Door.Count);
        }
        else
        {
            LogHelper.LogSuccess("No data Found To Fetch : " + DateTime.Now);
            LogHelper.LogSuccess("Total Data Fetched for 172.16.8.122 on : " + DateTime.Now + ", Count : " + 0);
        }

        if (DataModel_BHUB != null)
        {
            SuccessLogEntity newSuccess_BHUB = new SuccessLogEntity();
            newSuccess_BHUB.Location_Code = Convert.ToInt32(PlantCode_BHUB);
            newSuccess_BHUB.SuccessCount = Convert.ToInt32(DataModel_BHUB.Count);
            newSuccess_BHUB.StartOn = StartOn;
            newSuccess_BHUB.EndOn = DateTime.Now;
            service.InsertSuccessLog(newSuccess_BHUB);
        }
        #endregion

        //Door PLY Not Working

        #region 172.16.2.131
        //172.16.2.131
        LogHelper.LogSuccess("Starting Server : 172.16.2.131");

        Int64 PlantCodeK_Door = service.GetPlantCodeK_Door();
        string LastRecordIdK_Door = service.GetLatestRecordFetcheWithServer(PlantCodeK_Door, "172.16.2.131");
        if (!string.IsNullOrWhiteSpace(LastRecordIdK_Door))
        {
            DataModelK_Door = service.PullRedordsByIdK_Door(LastRecordIdK_Door);
        }
        else
        {
            DataModelK_Door = service.GetLatestRecordFetchedLasteHourK_Door();
            //DataModelK_Door
        }
        if (DataModelK_Door != null && DataModelK_Door.Count != 0)
        {
            foreach (LabelPrintingEntity Data in DataModelK_Door)
            {
                try
                {
                    Server = "172.16.2.131";
                    service.InsertPrintingDetails(Data, Server);
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
            LogHelper.LogSuccess("Total Data Fetched for 172.16.2.131 on : " + DateTime.Now + ", Count : " + DataModelK_Door.Count);
        }
        else
        {
            LogHelper.LogSuccess("No data Found To Fetch : " + DateTime.Now);
            LogHelper.LogSuccess("Total Data Fetched for 172.16.2.131 on : " + DateTime.Now + ", Count : " + 0);

        }
        if (DataModelK_Door != null)
        {
            SuccessLogEntity newSuccessK_Door = new SuccessLogEntity();
            newSuccessK_Door.Location_Code = Convert.ToInt32(PlantCodeK_Door);
            newSuccessK_Door.SuccessCount = Convert.ToInt32(DataModelK_Door.Count);
            newSuccessK_Door.StartOn = StartOn;
            newSuccessK_Door.EndOn = DateTime.Now;
            service.InsertSuccessLog(newSuccessK_Door);
        }
        #endregion

        #region 172.16.2.121
        //172.16.2.121
        LogHelper.LogSuccess("Starting Server : 172.16.2.121");

        Int64 PlantCodeK_Ply = service.GetPlantCodeK_PLY();
        string LastRecordIdK_Ply = service.GetLatestRecordFetcheWithServer(PlantCodeK_Ply, "172.16.2.121");
        if (!string.IsNullOrWhiteSpace(LastRecordIdK_Ply))
        {
            DataModelK_Ply = service.PullRedordsByIdK_PLY(LastRecordIdK_Ply);
        }
        else
        {
            DataModelK_Ply = service.GetLatestRecordFetchedLasteHourK_PLY();
            //DataModelK_Ply
        }
        if (DataModelK_Ply != null && DataModelK_Ply.Count != 0)
        {
            foreach (LabelPrintingEntity Data in DataModelK_Ply)
            {
                try
                {
                    Server = "172.16.2.121";
                    service.InsertPrintingDetails(Data, Server);
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
            LogHelper.LogSuccess("Total Data Fetched for 172.16.2.121 on : " + DateTime.Now + ", Count : " + DataModelK_Ply.Count);
        }
        else
        {
            LogHelper.LogSuccess("No data Found To Fetch : " + DateTime.Now);
            LogHelper.LogSuccess("Total Data Fetched for 172.16.2.121 on : " + DateTime.Now + ", Count : " + 0);
        }
        if (DataModelK_Ply != null)
        {
            SuccessLogEntity newSuccessK_Ply = new SuccessLogEntity();
            newSuccessK_Ply.Location_Code = Convert.ToInt32(PlantCodeK_Ply);
            newSuccessK_Ply.SuccessCount = Convert.ToInt32(DataModelK_Ply.Count);
            newSuccessK_Ply.StartOn = StartOn;
            newSuccessK_Ply.EndOn = DateTime.Now;
            service.InsertSuccessLog(newSuccessK_Ply);
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
