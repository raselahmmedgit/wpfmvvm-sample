using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RnD.WPFMVVMSample.Core
{
    public static class CommonHelper
    {
        /// <summary>
        /// Parsing Connection String To DataSettings From Text File
        /// </summary>
        /// <param name="text"></param>
        /// <returns type="DataSettings"></returns>
        public static DataSettings ParseDataSettingsFromText(string text)
        {
            var dataSettings = new DataSettings();
            if (String.IsNullOrEmpty(text))
                return dataSettings;

            //Old way of file reading. This leads to unexpected behavior when a user's FTP program transfers these files as ASCII (\r\n becomes \n).
            //var settings = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var settings = new List<string>();
            using (var reader = new StringReader(text))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                    settings.Add(str);
            }

            foreach (var setting in settings)
            {
                var separatorIndex = setting.IndexOf(":");
                if (separatorIndex == -1)
                {
                    continue;
                }
                string key = setting.Substring(0, separatorIndex).Trim();
                string value = setting.Substring(separatorIndex + 1).Trim();

                switch (key)
                {
                    case "DataProvider":
                        dataSettings.DataProvider = value;
                        break;
                    case "DataConnectionString":
                        dataSettings.DataConnectionString = value;
                        break;
                    default:
                        dataSettings.RawDataSettings.Add(key, value);
                        break;
                }
            }

            return dataSettings;
        }

        /// <summary>
        /// Parsing Connection String To DataSettings From XML File
        /// </summary>
        /// <param name="text"></param>
        /// <returns type="DataSettings"></returns>
        public static DataSettings ParseDataSettingsFromXml(string text)
        {
            var dataSettings = new DataSettings();
            if (String.IsNullOrEmpty(text))
                return dataSettings;

            //Old way of file reading. This leads to unexpected behavior when a user's FTP program transfers these files as ASCII (\r\n becomes \n).
            //var settings = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var settings = new List<string>();
            using (var reader = new StringReader(text))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                    settings.Add(str);
            }

            foreach (var setting in settings)
            {
                var separatorIndex = setting.IndexOf(":");
                if (separatorIndex == -1)
                {
                    continue;
                }
                string key = setting.Substring(0, separatorIndex).Trim();
                string value = setting.Substring(separatorIndex + 1).Trim();

                switch (key)
                {
                    case "DataProvider":
                        dataSettings.DataProvider = value;
                        break;
                    case "DataConnectionString":
                        dataSettings.DataConnectionString = value;
                        break;
                    default:
                        dataSettings.RawDataSettings.Add(key, value);
                        break;
                }
            }

            return dataSettings;
        }


        /// <summary>
        /// Creating AppSettings File
        /// </summary>
        /// <returns type="bool"></returns>
        public static bool CreateAppSettingsFile()
        {
            bool isCreated = false;

            try
            {
                // Create application folder in My Document Folder
                // Determine whether the directory exists. 
                if (!Directory.Exists(LocalSettings.AppDocumentFolderPath) || !File.Exists(LocalSettings.AppSettingsFilePath)) // Checking Application Folder and Application Setting File if not exists
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(LocalSettings.AppDocumentFolderPath);

                    // Create application setting in My Document/[Application Folder]
                    // Determine whether the file exists in directory.
                    if (Directory.Exists(LocalSettings.AppDocumentFolderPath)) // Checking Application Folder if exists
                    {
                        using (File.Create(LocalSettings.AppSettingsFilePath))
                        //using (File.Create(LocalSettings.AppSettingsXmlFilePath))
                        {
                            //we use 'using' to close the file after it's created
                            isCreated = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isCreated;
        }
    }
}
