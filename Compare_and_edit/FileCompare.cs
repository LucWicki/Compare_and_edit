using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compare_and_edit
{
    //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/how-to-compare-the-contents-of-two-folders-linq
    internal class FileCompare : System.Collections.Generic.IEqualityComparer<System.IO.FileInfo>
    {
        public bool Equals(FileInfo x, FileInfo y)
        {
            return (x.Name == y.Name &&
                     x.Length == y.Length);
        }

        public int GetHashCode(FileInfo fi)
        {
            string s = $"{fi.Name}{fi.Length}";
            return s.GetHashCode();
        }
    }
}
