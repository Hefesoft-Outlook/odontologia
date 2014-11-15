using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


    public static class Stream_To_Xml
    {
        public static string streamToString(this System.IO.Stream memoryStream)
        {
            string myStr = "";
            using (MemoryStream stream = new MemoryStream())
            {
                memoryStream.Position = 0;
                var sr = new StreamReader(memoryStream);
                myStr = sr.ReadToEnd();
            }

            return myStr;
        }
    }

