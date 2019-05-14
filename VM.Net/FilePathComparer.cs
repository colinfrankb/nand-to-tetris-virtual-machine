using System;
using System.Collections.Generic;
using System.Text;

namespace VM.Net
{
    public class FilePathComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x.Contains("Sys.vm"))
            {
                return -1;
            }

            if (y.Contains("Sys.vm"))
            {
                return 1;
            }

            return 0;
        }
    }
}
