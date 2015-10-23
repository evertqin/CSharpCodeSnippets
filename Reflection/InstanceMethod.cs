using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCodeSnippets.Reflection
{
    class InstanceMethod
    {
        public static object GetInstanceField(Type type, object instance, string fieldName)
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                        BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindingFlags);
            return field.GetValue(instance);
        }

        public static object InvokeInstanceFunction<T>(Type type, object instance, string funcName,
            params object[] methodParams)
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                              BindingFlags.Static;

            MethodInfo method = type.GetMethod(funcName, bindingFlags);
            if (method.ContainsGenericParameters)
            {
                method = method.MakeGenericMethod(typeof(T));
            }
            return method.Invoke(instance, methodParams);
        }
    }
}
