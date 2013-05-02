using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RnD.WPFMVVMSample.Core
{
    public static class XmlHelper
    {
        #region Write To Xml File

        /// <summary>
        /// Writing Default Connection String To AppSettings Xml File
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns type="bool"></returns>
        public static bool WriteConnectionStringToXml(string fileName)
        {
            bool isSaved = false;

            try
            {
                if (string.IsNullOrEmpty(fileName))
                    throw new ArgumentNullException("fileName");

                //creating new XML document
                XmlDocument xmlDoc = new XmlDocument();

                //creating XmlTestWriter, and passing file name and encoding type as argument
                XmlTextWriter xmlWriter = new XmlTextWriter(fileName, Encoding.UTF8);

                //setting XmlWriter formatting to be indented
                xmlWriter.Formatting = Formatting.Indented;

                //writing version and encoding type of XML in file.
                xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");

                //writing first element
                xmlWriter.WriteStartElement("connectionStrings");

                //closing writer
                xmlWriter.Close();

                //loading XML file
                xmlDoc.Load(fileName);

                //creating child nodes.
                XmlNode connectionStrings = xmlDoc.DocumentElement;

                //creating child node to root
                XmlElement connectionString = xmlDoc.CreateElement("connectionString");
                if (connectionStrings != null) connectionStrings.AppendChild(connectionString);

                //creating attribute connectionString element
                XmlAttribute connectionStringType = xmlDoc.CreateAttribute("type");
                connectionStringType.Value = "default";
                connectionString.Attributes.Append(connectionStringType);

                //adding child node to connectionString.
                XmlElement dataConnectionString = xmlDoc.CreateElement("dataConnectionString");
                XmlElement dataProviderName = xmlDoc.CreateElement("dataProviderName");

                //creating connectionString
                string conString = "Data Source=" + LocalSettings.AppDefaultDbFilePath;

                //assigning innerText of childNode to connectionString.
                connectionString.AppendChild(dataConnectionString);
                dataConnectionString.InnerText = conString;

                connectionString.AppendChild(dataProviderName);
                dataProviderName.InnerText = "System.Data.SqlServerCe.4.0";

                xmlDoc.Save(fileName);

                isSaved = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSaved;
        }

        /// <summary>
        /// Writing Custom Database Connection String To AppSettings Xml File
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dataSource"></param>
        /// <param name="initialCatalog"></param>
        /// <param name="integratedSecurity"></param>
        /// <param name="providerName"></param>
        /// <returns type="bool"></returns>
        public static bool WriteConnectionStringToXml(string fileName, string dataSource, string initialCatalog, string integratedSecurity, string providerName)
        {
            bool isSaved = false;

            try
            {

                if (string.IsNullOrEmpty(fileName) && string.IsNullOrEmpty(dataSource) && string.IsNullOrEmpty(initialCatalog) && string.IsNullOrEmpty(integratedSecurity) && string.IsNullOrEmpty(providerName))
                    throw new ArgumentNullException("fileName");

                //creating new XML document
                XmlDocument xmlDoc = new XmlDocument();

                //creating XmlTestWriter, and passing file name and encoding type as argument
                XmlTextWriter xmlWriter = new XmlTextWriter(fileName, Encoding.UTF8);

                //setting XmlWriter formatting to be indented
                xmlWriter.Formatting = Formatting.Indented;

                //writing version and encoding type of XML in file.
                xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");

                //writing first element
                xmlWriter.WriteStartElement("connectionStrings");

                //closing writer
                xmlWriter.Close();

                //loading XML file
                xmlDoc.Load(fileName);

                //creating child nodes.
                XmlNode connectionstrings = xmlDoc.DocumentElement;

                //creating child node to root
                XmlElement connectionstring = xmlDoc.CreateElement("connectionString");
                if (connectionstrings != null) connectionstrings.AppendChild(connectionstring);

                //creating attribute connectionString element
                XmlAttribute connectionstringtype = xmlDoc.CreateAttribute("type");
                connectionstringtype.Value = "custom";
                connectionstring.Attributes.Append(connectionstringtype);

                //adding child node to connectionString.
                XmlElement dataConnectionString = xmlDoc.CreateElement("dataConnectionString");
                XmlElement dataProviderName = xmlDoc.CreateElement("dataProviderName");

                //creating connectionString
                string conString = "Data Source=" + dataSource + ";" + "Catalog=" + initialCatalog + ";" + "Integrated Security=" + integratedSecurity + ";";

                //assigning innerText of childNode to connectionString.
                connectionstring.AppendChild(dataConnectionString);
                dataConnectionString.InnerText = conString;

                connectionstring.AppendChild(dataProviderName);
                dataProviderName.InnerText = providerName;

                xmlDoc.Save(fileName);

                isSaved = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSaved;
        }

        /// <summary>
        /// Writing Custom Database with User, Password Connection String To AppSettings Xml File
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dataSource"></param>
        /// <param name="userId"></param>
        /// <param name="passWord"></param>
        /// <param name="initialCatalog"></param>
        /// <param name="integratedSecurity"></param>
        /// <param name="providerName"></param>
        /// <returns type="bool"></returns>
        public static bool WriteConnectionStringToXml(string fileName, string dataSource, string userId, string passWord, string initialCatalog, string integratedSecurity, string providerName)
        {
            bool isSaved = false;

            try
            {

                if (string.IsNullOrEmpty(fileName) && string.IsNullOrEmpty(dataSource) && string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(passWord) && string.IsNullOrEmpty(initialCatalog) && string.IsNullOrEmpty(integratedSecurity) && string.IsNullOrEmpty(providerName))
                    throw new ArgumentNullException("fileName");

                //creating new XML document
                XmlDocument xmlDoc = new XmlDocument();

                //creating XmlTestWriter, and passing file name and encoding type as argument
                XmlTextWriter xmlWriter = new XmlTextWriter(fileName, Encoding.UTF8);

                //setting XmlWriter formatting to be indented
                xmlWriter.Formatting = Formatting.Indented;

                //writing version and encoding type of XML in file.
                xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");

                //writing first element
                xmlWriter.WriteStartElement("connectionStrings");

                //closing writer
                xmlWriter.Close();

                //loading XML file
                xmlDoc.Load(fileName);

                //creating child nodes.
                XmlNode connectionstrings = xmlDoc.DocumentElement;

                //creating child node to root
                XmlElement connectionstring = xmlDoc.CreateElement("connectionString");
                if (connectionstrings != null) connectionstrings.AppendChild(connectionstring);

                //creating attribute connectionString element
                XmlAttribute connectionstringtype = xmlDoc.CreateAttribute("type");
                connectionstringtype.Value = "custom";
                connectionstring.Attributes.Append(connectionstringtype);

                //adding child node to connectionString.
                XmlElement dataConnectionString = xmlDoc.CreateElement("dataConnectionString");
                XmlElement dataProviderName = xmlDoc.CreateElement("dataProviderName");

                //creating connectionString
                string conString = "Data Source=" + dataSource + ";" + "User ID=" + userId + ";" + "Password=" + passWord + ";" + "Catalog=" + initialCatalog + ";" + "Integrated Security=" + integratedSecurity + ";";

                //assigning innerText of childNode to connectionString.
                connectionstring.AppendChild(dataConnectionString);
                dataConnectionString.InnerText = conString;

                connectionstring.AppendChild(dataProviderName);
                dataProviderName.InnerText = providerName;

                xmlDoc.Save(fileName);

                isSaved = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSaved;
        }

        /// <summary>
        /// Writing Custom Server Connection String To AppSettings Xml File
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
        public static bool WriteConnectionStringToXml(string fileName, string serverName, string databaseName, string userId, string passWord, string initialCatalog, string integratedSecurity, string providerName)
        {
            bool isSaved = false;

            try
            {

                if (string.IsNullOrEmpty(fileName) && string.IsNullOrEmpty(serverName) && string.IsNullOrEmpty(databaseName) && string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(passWord) && string.IsNullOrEmpty(initialCatalog) && string.IsNullOrEmpty(integratedSecurity) && string.IsNullOrEmpty(providerName))
                    throw new ArgumentNullException("fileName");

                //creating new XML document
                XmlDocument xmlDoc = new XmlDocument();

                //creating XmlTestWriter, and passing file name and encoding type as argument
                XmlTextWriter xmlWriter = new XmlTextWriter(fileName, Encoding.UTF8);

                //setting XmlWriter formatting to be indented
                xmlWriter.Formatting = Formatting.Indented;

                //writing version and encoding type of XML in file.
                xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");

                //writing first element
                xmlWriter.WriteStartElement("connectionStrings");

                //closing writer
                xmlWriter.Close();

                //loading XML file
                xmlDoc.Load(fileName);

                //creating child nodes.
                XmlNode connectionstrings = xmlDoc.DocumentElement;

                //creating child node to root
                XmlElement connectionstring = xmlDoc.CreateElement("connectionString");
                if (connectionstrings != null) connectionstrings.AppendChild(connectionstring);

                //creating attribute connectionString element
                XmlAttribute connectionstringtype = xmlDoc.CreateAttribute("type");
                connectionstringtype.Value = "custom";
                connectionstring.Attributes.Append(connectionstringtype);

                //adding child node to connectionString.
                XmlElement dataConnectionString = xmlDoc.CreateElement("dataConnectionString");
                XmlElement dataProviderName = xmlDoc.CreateElement("dataProviderName");

                //creating connectionString
                string conString = "Server=" + serverName + "Database=" + databaseName + ";" + "User ID=" + userId + ";" + "Password=" + passWord + ";" + "Catalog=" + initialCatalog + ";" + "Integrated Security=" + integratedSecurity + ";";

                //assigning innerText of childNode to connectionString.
                connectionstring.AppendChild(dataConnectionString);
                dataConnectionString.InnerText = conString;

                connectionstring.AppendChild(dataProviderName);
                dataProviderName.InnerText = providerName;

                xmlDoc.Save(fileName);

                isSaved = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSaved;
        }

        #endregion Write To Xml File

        #region Read From Xml File

        /// <summary>
        /// Reading Default Connection String From AppSettings Xml File
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns type="DataSettings"></returns>
        public static DataSettings ReadConnectionStringFromXml(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                    throw new ArgumentNullException("fileName");

                DataSettings dataSettings = new DataSettings();

                //creating new XML document
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(fileName);

                XmlNode dataConnectionStringNode = xmlDoc.SelectSingleNode("/connectionStrings/connectionString/dataConnectionString");
                string dataConnectionString = dataConnectionStringNode.InnerText;

                XmlNode dataProviderNameNode = xmlDoc.SelectSingleNode("/connectionStrings/connectionString/dataProviderName");
                string dataProviderName = dataProviderNameNode.InnerText;

                dataSettings.DataConnectionString = String.IsNullOrEmpty(dataConnectionString) ? null : dataConnectionString;
                dataSettings.DataProvider = String.IsNullOrEmpty(dataProviderName) ? null : dataProviderName;

                return dataSettings;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Read From Xml File

    }
}
