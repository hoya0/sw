using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using ICSharpCode.SharpZipLib.Zip;

namespace Swoopo.Utils
{
    public static class ZipUtils
    {
        public static void CreateZip(string path, Stream os)
        {
            FastZip fz = new FastZip();
            fz.CreateZip(os, path, true, null, null);
        }
        public static void ExtractZip(Stream input, string target)
        {
            using (ZipInputStream st = new ZipInputStream(input))
            {
                ZipEntry ze;
                byte[] buffer = new byte[0x1000];
                while ((ze = st.GetNextEntry()) != null)
                {
                    string filename = ze.Name;
                    string dir = Path.Combine(target, filename);
                    string path = Path.GetDirectoryName(Path.GetFullPath(dir));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (ze.IsFile)
                    {
                        using (FileStream ts = File.Create(dir))
                        {
                            int num = 0;
                            while ((num = st.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                if (num > 0)
                                {
                                    ts.Write(buffer, 0, num);
                                }
                            }
                            ts.Flush();
                            ts.Close();
                        }
                    }
                    else if (ze.IsDirectory)
                    {   
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }
                    }
                }
            }
        }

    }
}
