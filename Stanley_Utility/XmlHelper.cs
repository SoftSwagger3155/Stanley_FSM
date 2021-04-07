using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Stanley_Utility
{
    public class XmlHelper
    {
        public static string LastError = "";
        public static string LastErrorDetail = "";

        public static bool SaveXml(Type objectType, object objectToStore, string fileName)
        {
            StreamWriter streamWriter = null;
            bool result = false;
            try
            {
                XmlHelper.LastError = "";
                XmlHelper.LastErrorDetail = "";
                XmlSerializer xmlSerializer = new XmlSerializer(objectType);
                streamWriter = new StreamWriter(fileName, false);
                xmlSerializer.Serialize(streamWriter, objectToStore);
                if (streamWriter != null)
                {
                    streamWriter.Close();
                }
                result = true;
            }
            catch (Exception innerException)
            {
                Trace.WriteLine("Failed to serialize. Reason: " + innerException.Message);
                XmlHelper.LastError = innerException.Message;
                XmlHelper.LastErrorDetail = innerException.ToString();
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                    XmlHelper.LastErrorDetail = XmlHelper.LastErrorDetail + Environment.NewLine + "[Inner]" + innerException.ToString();
                }
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                }
            }
            return result;
        }

        public static object LoadXml(Type objectType, string fileName)
        {
            FileStream fileStream = null;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(objectType);
                FileInfo fileInfo = new FileInfo(fileName);
                if (fileInfo.Exists)
                {
                    fileStream = fileInfo.OpenRead();
                    return xmlSerializer.Deserialize(fileStream);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed to deserialize. Reason: " + ex.Message);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
            return null;
        }
    }
}
