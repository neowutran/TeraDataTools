﻿using System;
using System.IO;
using System.Linq;
using DCTools;
using Newtonsoft.Json;

namespace DataTools.Parsers.DC
{
    class JsonDumper
    {
        public static void Parse()
        {
            var dir = "data/json/";
            var dc = DCT.GetDataCenter();
            foreach (var group in dc.MainObjects.GroupBy(x => x.Name))
            {
                string dir2, format;
                if (group.Count() > 1)
                {
                    dir2 = dir + group.Key+"/";
                    format = "{0}-{1}.json";
                }
                else
                {
                    dir2 = dir;
                    format = "{0}.json";
                }

                Directory.CreateDirectory(dir2);
                int i = 0;
                foreach (var mainObject in group)
                {
                    var values = dc.GetValues(mainObject);
                    var json = JsonConvert.SerializeObject(values, Formatting.Indented);
                    File.WriteAllText(dir2 + string.Format(format, mainObject.Name, i), json);
                    i++;
                }
            }
        }
    }
}
