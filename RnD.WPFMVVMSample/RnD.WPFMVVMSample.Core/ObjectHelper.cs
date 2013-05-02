using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace RnD.WPFMVVMSample.Core
{
    public static class ObjectHelper
    {
        public static string GetName<T>(Expression<Func<T>> memberExpression)
        {
            return (memberExpression.Body as MemberExpression).Member.Name;
        }

        public static void CopyPropertiesValueFromBaseType<Entity>(Entity baseSource, Entity destinationChild)
        {
            Type type = typeof(Entity);

            PropertyInfo[] myObjectFields = type.GetProperties(
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo fi in myObjectFields)
            {
                fi.SetValue(destinationChild, fi.GetValue(baseSource, null), null);
            }
        }
    }
}
