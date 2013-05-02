using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RnD.WPFMVVMSample.Core
{
    public static class TextHelper
    {
        #region Write To Text File

        /// <summary>
        /// Writing Default Connection String To AppSettings Text File
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns type="bool"></returns>
        public static bool WriteConnectionStringToText(string fileName)
        {
            bool isSaved = false;

            try
            {
                if (string.IsNullOrEmpty(fileName))
                    throw new ArgumentNullException("fileName");

                //creating ConnectionString
                string conString = "Data Source=" + LocalSettings.AppDefaultDbFilePath;

                // creating ProviderName
                //string proName = "providerName=" + "System.Data.SqlServerCe.4.0";
                string proName = "System.Data.SqlServerCe.4.0";

                if (File.Exists(fileName))
                {
                    string text = string.Format("DataProvider: {0}{2}DataConnectionString: {1}{2}",
                                                     proName,
                                                     conString,
                                                     Environment.NewLine);

                    File.WriteAllText(fileName, text);

                    isSaved = true;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSaved;
        }

        /// <summary>
        /// Writing Custom Database Connection String To AppSettings Text File
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dataSource"></param>
        /// <param name="initialCatalog"></param>
        /// <param name="integratedSecurity"></param>
        /// <param name="providerName"></param>
        /// <returns type="bool"></returns>
        public static bool WriteConnectionStringToText(string fileName, string dataSource, string initialCatalog, string integratedSecurity, string providerName)
        {
            bool isSaved = false;

            try
            {

                if (string.IsNullOrEmpty(fileName) && string.IsNullOrEmpty(dataSource) && string.IsNullOrEmpty(initialCatalog) && string.IsNullOrEmpty(integratedSecurity) && string.IsNullOrEmpty(providerName))
                    throw new ArgumentNullException("fileName");

                //creating ConnectionString
                string conString = "Data Source=" + dataSource + ";" + "Catalog=" + initialCatalog + ";" + "Integrated Security=" + integratedSecurity + ";";

                // creating ProviderName
                //string proName = "providerName=" + providerName;
                string proName = providerName;

                if (File.Exists(fileName))
                {
                    string text = string.Format("DataProvider: {0}{2}DataConnectionString: {1}{2}",
                                                     proName,
                                                     conString,
                                                     Environment.NewLine);

                    if (fileName != null) File.WriteAllText(fileName, text);

                    isSaved = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSaved;
        }

        /// <summary>
        /// Writing Custom Database with User, Password Connection String To AppSettings Text File
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dataSource"></param>
        /// <param name="userId"></param>
        /// <param name="passWord"></param>
        /// <param name="initialCatalog"></param>
        /// <param name="integratedSecurity"></param>
        /// <param name="providerName"></param>
        /// <returns type="bool"></returns>
        public static bool WriteConnectionStringToText(string fileName, string dataSource, string userId, string passWord, string initialCatalog, string integratedSecurity, string providerName)
        {
            bool isSaved = false;

            try
            {

                if (string.IsNullOrEmpty(fileName) && string.IsNullOrEmpty(dataSource) && string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(passWord) && string.IsNullOrEmpty(initialCatalog) && string.IsNullOrEmpty(integratedSecurity) && string.IsNullOrEmpty(providerName))
                    throw new ArgumentNullException("fileName");

                //creating ConnectionString
                string conString = "Data Source=" + dataSource + ";" + "User ID=" + userId + ";" + "Password=" + passWord + ";" + "Catalog=" + initialCatalog + ";" + "Integrated Security=" + integratedSecurity + ";";

                // creating ProviderName
                //string proName = "providerName=" + providerName;
                string proName = providerName;

                if (File.Exists(fileName))
                {
                    string text = string.Format("DataProvider: {0}{2}DataConnectionString: {1}{2}",
                                                     proName,
                                                     conString,
                                                     Environment.NewLine);

                    File.WriteAllText(fileName, text);

                    isSaved = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSaved;
        }

        /// <summary>
        /// Writing Custom Server Connection String To AppSettings Text File
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="serverName"></param>
        /// <param name="databaseName"></param>
        /// <param name="userId"></param>
        /// <param name="passWord"></param>
        /// <param name="initialCatalog"></param>
        /// <param name="integratedSecurity"></param>
        /// <param name="providerName"></param>
        /// <returns type="bool"></returns>
        public static bool WriteConnectionStringToText(string fileName, string serverName, string databaseName, string userId, string passWord, string initialCatalog, string integratedSecurity, string providerName)
        {
            bool isSaved = false;

            try
            {

                if (string.IsNullOrEmpty(fileName) && string.IsNullOrEmpty(serverName) && string.IsNullOrEmpty(databaseName) && string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(passWord) && string.IsNullOrEmpty(initialCatalog) && string.IsNullOrEmpty(integratedSecurity) && string.IsNullOrEmpty(providerName))
                    throw new ArgumentNullException("fileName");

                //creating ConnectionString
                string conString = "Server=" + serverName + "Database=" + databaseName + ";" + "User ID=" + userId + ";" + "Password=" + passWord + ";" + "Catalog=" + initialCatalog + ";" + "Integrated Security=" + integratedSecurity + ";";

                // creating ProviderName
                //string proName = "providerName=" + providerName;
                string proName = providerName;

                if (File.Exists(fileName))
                {
                    string text = string.Format("DataProvider: {0}{2}DataConnectionString: {1}{2}",
                                                     proName,
                                                     conString,
                                                     Environment.NewLine);

                    File.WriteAllText(fileName, text);

                    isSaved = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSaved;
        }

        #endregion Write To Text File

        #region Read From Text File

        /// <summary>
        /// Reading Default Connection String From AppSettings Text File
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns type="DataSettings"></returns>
        public static DataSettings ReadConnectionStringFromText(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                    throw new ArgumentNullException("fileName");

                if (File.Exists(fileName))
                {
                    string text = File.ReadAllText(fileName);
                    return CommonHelper.ParseDataSettingsFromText(text);
                }
                else
                    return new DataSettings();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Read From Text File
    }
}
