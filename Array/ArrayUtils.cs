using System;
using System.Runtime.InteropServices;

namespace CSharpCodeSnippets.Array
{
    public class ArrayUtils
    {
        public static System.Array ConvertTo2DArray<T>(T[][] input)
        {
            if (input == null)
            {
                throw new NullReferenceException("The input array is null.");
            }

            if (input.Length <= 0 || input[0].Length <= 0)
            {
                return new T[0, 0];
            }

            System.Array ret = new T[input.Length, input[0].Length];

            if (typeof(T) == typeof(string))
            {
                for (int i = 0; i < input.Length; ++i)
                {
                    for (int j = 0; j < input[0].Length; ++j)
                    {
                        ret.SetValue(input[i][j], i, j);
                    }
                }
            }
            else
            {
                for (int i = 0; i < input.Length; ++i)
                {
                    Buffer.BlockCopy(input[i], 0, ret, Marshal.SizeOf(typeof(T)) * i * input[0].Length,
                        Marshal.SizeOf(typeof(T)) * input[0].Length);
                }
            }
            return ret;
        }
    }
}
