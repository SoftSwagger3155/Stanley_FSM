using System;

namespace Stanley_Utility
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class DynamicPropertyFilterAttribute : Attribute
    {
        public string PropertyName
        {
            get
            {
                return this._propertyName;
            }
        }

        public string ShowOn
        {
            get
            {
                return this._showOn;
            }
        }

        public DynamicPropertyFilterAttribute(string propertyName, string propertyValuesToDisplay)
        {
            this._propertyName = propertyName;
            this._showOn = propertyValuesToDisplay;
        }

        private string _propertyName;

        private string _showOn;
    }
}
