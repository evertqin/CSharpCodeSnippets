using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCodeSnippets.Enum
{
    public class EnumUtils
    {
        public static string GetEnumDescription(System.Enum value)
        {
            // source http://blog.spontaneouspublicity.com/associating-strings-with-enums-in-c

            var fi = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
