using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Stanley_Utility
{
    public class FilterablePropertyBase : PersistentXML, ICustomTypeDescriptor
    {
        [Browsable(false)]
        [XmlIgnore]
        public Action UpdateMethod
        {
            get
            {
                return this._updateMethod;
            }
            set
            {
                if (this._updateMethod == null)
                {
                    this._updateMethod = value;
                }
            }
        }

        [Browsable(false)]
        public void UpdatedData()
        {
            if (this.UpdateMethod != null)
            {
                this.UpdateMethod();
            }
        }

        protected PropertyDescriptorCollection GetFilteredProperties(Attribute[] attributes)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
            PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
            foreach (object obj in properties)
            {
                PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
                bool flag = false;
                bool flag2 = false;
                foreach (object obj2 in propertyDescriptor.Attributes)
                {
                    Attribute attribute = (Attribute)obj2;
                    if (attribute is DynamicPropertyFilterAttribute)
                    {
                        flag2 = true;
                        DynamicPropertyFilterAttribute dynamicPropertyFilterAttribute = (DynamicPropertyFilterAttribute)attribute;
                        PropertyDescriptor propertyDescriptor2 = properties[dynamicPropertyFilterAttribute.PropertyName];
                        if (dynamicPropertyFilterAttribute.ShowOn.IndexOf(propertyDescriptor2.GetValue(this).ToString()) > -1)
                        {
                            flag = true;
                        }
                    }
                }
                if (!flag2 || flag)
                {
                    propertyDescriptorCollection.Add(propertyDescriptor);
                }
            }
            return propertyDescriptorCollection;
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return this.GetFilteredProperties(attributes);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return this.GetFilteredProperties(new Attribute[0]);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        protected Action _updateMethod = null;
    }
}
