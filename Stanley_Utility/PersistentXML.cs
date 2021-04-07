using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml.Serialization;

namespace Stanley_Utility
{
    public class PersistentXML
    {
        public PersistentXML()
        {
            this.fileName = "PersistentXML.config";
        }

        [ReadOnly(true)]
        [Browsable(false)]
        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                if (value != this.fileName)
                {
                    this.fileName = value;
                }
            }
        }

        public void Save()
        {
            StreamWriter streamWriter = null;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(base.GetType());
                streamWriter = new StreamWriter(this.fileName, false);
                xmlSerializer.Serialize(streamWriter, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException);
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                }
            }
        }

        public bool Load()
        {
            FileStream fileStream = null;
            bool result = false;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(base.GetType());
                FileInfo fileInfo = new FileInfo(this.fileName);
                if (fileInfo.Exists)
                {
                    fileStream = fileInfo.OpenRead();
                    PersistentXML persistentXML = (PersistentXML)xmlSerializer.Deserialize(fileStream);
                    this.UpdateMemberVariables(persistentXML);
                    result = true;
                }
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
            return result;
        }

        protected virtual void UpdateMemberVariables(PersistentXML persistentXML)
        {
            foreach (PropertyInfo propertyInfo in persistentXML.GetType().GetProperties())
            {
                foreach (PropertyInfo propertyInfo2 in base.GetType().GetProperties())
                {
                    if (propertyInfo2.CanWrite && propertyInfo.Name == propertyInfo2.Name && propertyInfo2.Name != "FileName")
                    {
                        propertyInfo2.SetValue(this, propertyInfo.GetValue(persistentXML, null), null);
                        break;
                    }
                }
            }
        }

        public void CopyTo(PersistentXML copy)
        {
            foreach (PropertyInfo propertyInfo in copy.GetType().GetProperties())
            {
                foreach (PropertyInfo propertyInfo2 in base.GetType().GetProperties())
                {
                    if (propertyInfo.CanWrite && propertyInfo2.Name == propertyInfo.Name && propertyInfo.Name != "FileName")
                    {
                        propertyInfo.SetValue(copy, propertyInfo2.GetValue(this, null), null);
                        break;
                    }
                }
            }
        }

        protected string fileName;
    }
}
