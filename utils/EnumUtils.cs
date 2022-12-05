using System;
using System.ComponentModel;

namespace AppClassLibraryDomain.utils
{
    public static class EnumUtils<T>
    {
        public static T FindEnumByValue(object value)
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                    if (attribute.Description.Equals(value))
                        return (T)field.GetValue(null);
                    else if (field.Name == AppDomain.CurrentDomain.FriendlyName.Split('.')[0])
                        return (T)field.GetValue(null);
            }
            return default(T);
        }
    }
}
