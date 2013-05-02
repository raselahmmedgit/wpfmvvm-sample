using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace RnD.WPFMVVMSample.Core
{
    public static class LocalSettings
    {
        public static string DocumentPath { get { return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); } }

        public static string AppPath { get; set; }
        public static string AppDocumentFolderPath { get { return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\wpfdemo"; } }

        public static string AppDataPath { get { return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\wpfdemo"; } }
        public static string AppUserPath { get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\wpfdemo"; } }

        public static string AppSettingsFilePath { get { return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\wpfdemo\\AppSettings.txt"; } }
        public static string AppCommonSettingsFileName { get { return AppDataPath + "\\AppSettings.txt"; } }
        public static string AppUserSettingsFileName { get { return AppUserPath + "\\AppSettings.txt"; } }

        public static string AppDefaultDbFilePath { get { return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\wpfdemo\\wpfdemo.sdf"; } }
        public static string AppCommonDefaultDbFilePath { get { return AppDataPath + "\\wpfdemo.sdf"; } }
        public static string AppUserDefaultDbFilePath { get { return AppUserPath + "\\wpfdemo.sdf"; } }

        public static string AppSettingsXmlFilePath { get { return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\wpfdemo\\AppSettings.xml"; } }
        public static string AppCommonSettingsXmlFileName { get { return AppDataPath + "\\AppSettings.xml"; } }
        public static string AppUserSettingsXmlFileName { get { return AppUserPath + "\\AppSettings.xml"; } }

        public static string AppSettingsFileName { get { return File.Exists(AppUserSettingsFileName) ? AppUserSettingsFileName : AppCommonSettingsFileName; } }

        public static string AppDefaultCurrencyFormat { get; set; }
        public static string AppCurrencySymbol { get { return CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol; } }

        public static int AppDbVersion { get { return 05; } }
        public static string AppVersion { get { return "0.05"; } }

    }
}
