using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadableProduct.Utillity
{
    public static class FileUtility
    {
        public static string GetLength(long length)
        {
            var result = (length / 1024f) / 1024f;
            return $"{Math.Round(result, 2)} مگابایت";
        }
    }
}
