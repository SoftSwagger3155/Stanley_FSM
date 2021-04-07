using System;
using System.Collections.Generic;
using System.Reflection;

namespace Stanley_Utility
{
    public class AttributeHelper
    {
        public static string GetDescription(Type type, string propertyName)
        {
            Dictionary<string, object> propertyAttributes = AttributeHelper.GetPropertyAttributes(type.GetProperty(propertyName));
            if (propertyAttributes != null)
            {
                if (propertyAttributes.ContainsKey("Description"))
                {
                    return propertyAttributes["Description"].ToString();
                }
            }
            return propertyName;
        }

        public static Dictionary<string, object> GetPropertyAttributes(PropertyInfo property)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (CustomAttributeData customAttributeData in property.GetCustomAttributesData())
            {
                if (customAttributeData.ConstructorArguments.Count == 1)
                {
                    string text = customAttributeData.Constructor.DeclaringType.Name;
                    if (text.EndsWith("Attribute"))
                    {
                        text = text.Substring(0, text.Length - 9);
                    }
                    dictionary[text] = customAttributeData.ConstructorArguments[0].Value;
                }
            }
            return dictionary;
        }
    }
}
