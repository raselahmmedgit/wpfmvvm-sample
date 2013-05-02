using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using RnD.WPFMVVMSample.Core;
using RnD.WPFMVVMSample.Data;
using System.IO;

namespace RnD.WPFMVVMSample.Presentation
{
    public static class BootStrapper
    {
        public static void Run()
        {
            AppReady();
            //SetIocContainer();
        }

        private static void AppReady()
        {
            try
            {
                // if AppSettings File Exist
                if (IsAppSettingsExists())
                {
                    //WriteConnectionStringToXmlAppSettings();
                    //WriteConnectionStringToTextAppSettings();
                    InitializeAndSeedDb();
                }
                else // if not AppSettings File Exist
                {
                    if (IsCreateAppSettingsFile()) // if Create AppSettings File
                    {
                        //WriteConnectionStringToXmlAppSettings();
                        WriteConnectionStringToTextAppSettings();
                        InitializeAndSeedDb();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void InitializeAndSeedDb()
        {
            try
            {
                //string connectionString = GetConnectionString(); // by String

                DataSettings dataSettings = GetDataSettings(); //by DataSettings

                if (dataSettings != null)
                {
                    //SqlConnectionFactory sqlConnectionFactory = new SqlConnectionFactory();
                    //sqlConnectionFactory.CreateConnection("");

                    // Initializes and seeds the database.
                    Database.SetInitializer(new DbInitializer());

                    //Database.DefaultConnectionFactory = sqlConnectionFactory;

                    //Need to Default Connection Factory
                    //Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", "", connectionString); //by String
                    Database.DefaultConnectionFactory = new SqlCeConnectionFactory(dataSettings.DataProvider, "", dataSettings.DataConnectionString); //by DataSettings

                    //using (var context = new AppDbContext()) // Create Database to Project bin folder
                    //using (var context = new AppDbContext(connectionString)) // by String
                    using (var context = new AppDbContext(dataSettings.DataConnectionString)) // by DateSettings
                    {
                        context.Database.Initialize(force: true);
                        var categories = context.Categories.ToList();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //private static void SetIocContainer()
        //{
        //    try
        //    {
        //        //Implement Autofac

        //        var configuration = GlobalConfiguration.Configuration;
        //        var builder = new ContainerBuilder();

        //        // Register MVC controllers using assembly scanning.
        //        builder.RegisterControllers(Assembly.GetExecutingAssembly());

        //        // Register MVC controller and API controller dependencies per request.
        //        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
        //        builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerLifetimeScope();

        //        // Register service
        //        builder.RegisterAssemblyTypes(typeof(ProfileService).Assembly)
        //        .Where(t => t.Name.EndsWith("Service"))
        //        .AsImplementedInterfaces().InstancePerDependency();

        //        // Register repository
        //        builder.RegisterAssemblyTypes(typeof(ProfileRepository).Assembly)
        //        .Where(t => t.Name.EndsWith("Repository"))
        //        .AsImplementedInterfaces().InstancePerDependency();

        //        var container = builder.Build();

        //        //for MVC Controller Set the dependency resolver implementation.
        //        var resolverMvc = new AutofacDependencyResolver(container);
        //        System.Web.Mvc.DependencyResolver.SetResolver(resolverMvc);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}

        private static string GetConnectionString()
        {
            string connectionString = string.Empty;

            //string connectionString = ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString;

            //string providerName = ConfigurationManager.ConnectionStrings["AppDbContext"].ProviderName;

            //string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            connectionString = "data source=" + LocalSettings.AppDocumentFolderPath + "\\wpfdemo.sdf";

            return connectionString;
        }

        private static DataSettings GetDataSettings()
        {
            try
            {
                // if AppSettings File Exist
                if (IsAppSettingsExists() && IsSqlCompact40Installed())
                {
                    return ReadConnectionStringFromAppSettings();
                    //return ReadConnectionStringFromTextAppSettings();
                    //return ReadConnectionStringFromXmlAppSettings();
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void WriteConnectionStringToAppSettings()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void WriteConnectionStringToXmlAppSettings()
        {
            try
            {
                XmlHelper.WriteConnectionStringToXml(LocalSettings.AppSettingsXmlFilePath);
                //XmlHelper.WriteConnectionStringToXml(LocalSettings.AppSettingsXmlFilePath, "ds", "ic", "is", "pn");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void WriteConnectionStringToTextAppSettings()
        {
            try
            {
                TextHelper.WriteConnectionStringToText(LocalSettings.AppSettingsFilePath);
                //TextHelper.WriteConnectionStringToText(LocalSettings.AppSettingsFilePath, "ds", "ic", "is", "pn");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DataSettings ReadConnectionStringFromAppSettings()
        {
            try
            {
                DataSettings dataSettings = TextHelper.ReadConnectionStringFromText(LocalSettings.AppSettingsFilePath);
                if (dataSettings.IsValid())
                {
                    return dataSettings;
                }
                else
                {
                    return new DataSettings();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DataSettings ReadConnectionStringFromTextAppSettings()
        {
            try
            {
                DataSettings dataSettings = TextHelper.ReadConnectionStringFromText(LocalSettings.AppSettingsFilePath);
                if (dataSettings.IsValid())
                {
                    return dataSettings;
                }
                else
                {
                    return new DataSettings();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DataSettings ReadConnectionStringFromXmlAppSettings()
        {
            try
            {
                DataSettings dataSettings = XmlHelper.ReadConnectionStringFromXml(LocalSettings.AppSettingsXmlFilePath);
                if (dataSettings.IsValid())
                {
                    return dataSettings;
                }
                else
                {
                    return new DataSettings();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static bool IsSqlCompact40Installed()
        {
            var rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server Compact Edition\\v4.0");
            return rk != null;
        }

        private static bool IsAppSettingsExists()
        {
            bool isAppSettingsExists = false;

            try
            {
                // Determine whether the directory and file exists. 
                if (Directory.Exists(LocalSettings.AppDocumentFolderPath) && File.Exists(LocalSettings.AppSettingsFilePath)) // Checking Application Folder and Application Setting File if not exists
                {
                    isAppSettingsExists = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isAppSettingsExists;
        }

        private static bool IsCreateAppSettingsFile()
        {
            bool isCreateAppSettingsFile = false;

            try
            {
                // Create the directory and file. 
                if (CommonHelper.CreateAppSettingsFile())
                {
                    isCreateAppSettingsFile = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isCreateAppSettingsFile;
        }
    }
}
