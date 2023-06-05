using GSPPL.common1.Helper;
using GSPPL.GSPPL_Sample.Entity;
using GSPPL_Sample.Service;
using System.Collections.Generic;
using System.Threading;

AllService service = new AllService();
DateTime StartOn;
DateTime EndOn;
try
{
    LogHelper.InitLog();
    LogHelper.LogMessage("Main Method Start" + DateTime.Now);
    while (true)
    {
        List<LabelPrintingEntity> DataModel = new List<LabelPrintingEntity>();
        List<LabelPrintingEntity> AllDataModel = new List<LabelPrintingEntity>();

        try
        {
            StartOn = DateTime.Now;

            Int64 PlantCode = service.GetPlantCode();
            string LastRecordId = service.GetLatestRecordFetched(PlantCode);
            if (!string.IsNullOrWhiteSpace(LastRecordId))
            {
                DataModel = service.PullRedordsById(LastRecordId);
            }
            else
            {
                DataModel = service.GetLatestRecordFetchedLasteHour();
            }
            if (DataModel != null && DataModel.Count != 0)
            {
                foreach (LabelPrintingEntity Data in DataModel)
                {
                    service.InsertPrintingDetails(Data);
                }
            }
            else
            {

            }
            LogHelper.LogSuccess("Total Data Fetched on : " + DateTime.Now + ", Count : " + DataModel.Count);

            EndOn = DateTime.Now;
            Thread.Sleep(5000);
        }
        catch (Exception e)
        {
            LogHelper.LogError("Loop Error: " + DateTime.Now + "-" + e);
        }
    }
    LogHelper.LogMessage("Main Method End" + DateTime.Now);
}
catch (Exception e)
{
    LogHelper.LogError("Error: " + DateTime.Now + "-" + e);
}
