using System;
using System.Diagnostics;
using System.IO;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;

namespace To.AtNinjas.AppInit
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: config de las rutas de los ejecutables para jobs
            //TODO: config del connection string.-

            GlobalConfiguration.Configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            //.UseSqlServerStorage(@"Server = tcp:bpsqlserver01.database.windows.net,1433; Initial Catalog = Novus; Persist Security Info = False; User ID = juarui; Password =Santiago230611@; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;",
            .UseSqlServerStorage(@"Server = MCASTILLA\SQLEXPRESS; Initial Catalog = KarenDaemon; Persist Security Info = False; User ID = sa; Password =bp_8998;",
            new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                UsePageLocksOnDequeue = true,
                DisableGlobalLocks = true
            });
            //.UseBatches()
            //.UsePerformanceCounters();

            BackgroundJob.Enqueue(() => Process.Start("notepad", "readme.txt"));

            string value1 = ConfigurationManager.AppSetting["GrandParent_Key:Parent_Key:Child_Key"];
            string custodyLoadExePath = ConfigurationManager.AppSetting["CustodyLoad:ExePath"];
            string trackingBypassExePath = ConfigurationManager.AppSetting["TrackingByPass:ExePath"];
            string trackingExePath = ConfigurationManager.AppSetting["Tracking:ExePath"];
            string generateTrackingReportExePath = ConfigurationManager.AppSetting["GenerateTrackingReport:ExePath"];
            string generateAssistControlExePath = ConfigurationManager.AppSetting["GenerateAssistControl:ExePath"];
            string requestMonitoringExePath = ConfigurationManager.AppSetting["RequestMonitoring:ExePath"];
            string generateTrackingDatabaseBackup = ConfigurationManager.AppSetting["GenerateTrackingDatabaseBackup:ExePath"];
            string generateCustodyDatabaseBackup = ConfigurationManager.AppSetting["GenerateCustodyDatabaseBackup:ExePath"];
            string custodyReportExePath = ConfigurationManager.AppSetting["CutodyReport:ExePath"];
            string value3 = ConfigurationManager.AppSetting["Child_Key"];
            //TODO: TIMEZONE INFO ENTRE AZURE Y LOCAL DIFIERE, VER IDIOMA timeinfo explicito peru utc-5
            //TODO: ASEGURAR QUE AL EJECUTAR PROCSES START SOLO SALGA EN 1 ...

            #region CustodyLoad
            //RecurringJob.AddOrUpdate("1",() =>
            //Process.Start(custodyLoadExePath),
            //Cron.Daily(2, 5), TimeZoneInfo.Local, "local_queue");

            //RecurringJob.AddOrUpdate("CustodyLoad_1", () =>
            //Process.Start(custodyLoadExePath),
            //Cron.Daily(10, 0), TimeZoneInfo.Local, "local_queue");

            //RecurringJob.AddOrUpdate("CustodyLoad_2", () =>
            //Process.Start(custodyLoadExePath),
            //Cron.Daily(16, 0), TimeZoneInfo.Local, "local_queue");
            #endregion

            #region TrackingBypass
            //RecurringJob.AddOrUpdate("TrackingByPass_1", () =>
            //Process.Start(trackingBypassExePath),
            //Cron.Hourly(), TimeZoneInfo.Local, "local_queue");
            #endregion

            #region Tracking
            //RecurringJob.AddOrUpdate("Tracking", () =>
            //Process.Start(trackingExePath),
            //"0 50 * ? * *", TimeZoneInfo.Local, "local_queue");
            #endregion

            #region GenerateTrackingReport
            //RecurringJob.AddOrUpdate("GenerateTrackingReport", () =>
            //Process.Start(generateTrackingReportExePath),
            //Cron.Daily(6, 0), TimeZoneInfo.Local, "local_queue");
            ////"0 55 8 * * ?", TimeZoneInfo.Local, "local_queue");
            #endregion

            #region GenerateAssistControl
            //RecurringJob.AddOrUpdate("GenerateAssistControl", () =>
            //Process.Start(generateAssistControlExePath),
            //Cron.Monthly(1, 6), TimeZoneInfo.Local, "local_queue");
            #endregion

            #region RequestMonitoring
            //RecurringJob.AddOrUpdate("RequestMonitoring", () =>
            //Process.Start(requestMonitoringExePath),
            //"* 9-18 * * 1-5", TimeZoneInfo.Local, "local_queue");
            #endregion

            #region GenerateTrackingDatabaseBackup
            RecurringJob.AddOrUpdate("GenerateTrackingDatabaseBackup", () =>
            Process.Start(generateTrackingDatabaseBackup),
            "0 23 * * 1-5", TimeZoneInfo.Local, "local_queue");
            #endregion

            #region GenerateCustodyDatabaseBackup
            //RecurringJob.AddOrUpdate("GenerateCustodyDatabaseBackup", () =>
            //Process.Start(generateCustodyDatabaseBackup),
            //"30 23 * * 1-5", TimeZoneInfo.Local, "local_queue");
            //RecurringJob.AddOrUpdate("Tracking_1", () =>
            //Process.Start(trackingExePath),
            //"0 */1 * ? * *", TimeZoneInfo.Local, "local_queue");
            #endregion

            #region CustodyReport
            //RecurringJob.AddOrUpdate("CustodyReport_1", () =>
            //Process.Start(custodyReportExePath),
            //"0 0 9 ? * MON ", TimeZoneInfo.Local, "local_queue");
            #endregion

            Console.WriteLine("Inicialización de jobs correcta");
            Console.ReadKey();

            //BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget"));

            //RecurringJob.AddOrUpdate(() =>
            //LogProceso(String.Format("Iniciando método Testing", DateTime.Now.ToString("h:mm:ss.fff"))),
            //Cron.Minutely, TimeZoneInfo.Local);



        }
        static class ConfigurationManager
        {
            public static IConfiguration AppSetting { get; }
            static ConfigurationManager()
            {
                AppSetting = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
            }
        }
    }
}
