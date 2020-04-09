using System;
using System.Collections.Generic;

namespace StaticAnalysisProject.Helpers
{
    /// <summary>
    /// Source from https://stackoverflow.com/questions/9755090/split-a-byte-array-at-a-delimiter
    /// </summary>
    public static class ByteHelper
    {
        /*byte[] SeparateAndGetLast(byte[] source, byte[] separator)
        {
            for (var i = 0; i < source.Length; ++i)
            {
                if (Equals(source, separator, i))
                {
                    var index = i + separator.Length;
                    var part = new byte[source.Length - index];
                    Array.Copy(source, index, part, 0, part.Length);
                    return part;
                }
            }
            throw new Exception("not found");
        }*/

        public static byte[][] Split(this byte[] source, byte[] separator)
        {
            var Parts = new List<byte[]>();
            var Index = 0;
            byte[] Part;
            for (var I = 0; I < source.Length; ++I)
            {
                if (Equals(source, separator, I))
                {
                    Part = new byte[I - Index];
                    Array.Copy(source, Index, Part, 0, Part.Length);
                    Parts.Add(Part);
                    Index = I + separator.Length;
                    I += separator.Length - 1;
                }
            }
            Part = new byte[source.Length - Index];
            Array.Copy(source, Index, Part, 0, Part.Length);
            Parts.Add(Part);
            return Parts.ToArray();
        }

        public static bool Equals(this byte[] source, byte[] separator, int index)
        {
            for (int i = 0; i < separator.Length; ++i)
                if (index + i >= source.Length || source[index + i] != separator[i])
                    return false;
            return true;
        }
    }
}
