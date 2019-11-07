﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExamples
{
    public static class ClassReaderJson
    {
        public static ClassesJson ReadClassFromFile()
        {
            ClassesJson classes = new ClassesJson();
            using (StreamReader r = new StreamReader(@"C:\Users\Akif.DESKTOP-KNQM9LL\Desktop\projGit\ReflectionExamples.Net\ReflectionExamples\ReflectionExamples\classesconfig.json"))
            {
                string json = r.ReadToEnd();
                classes = JsonConvert.DeserializeObject<ClassesJson>(json);
            }
            return classes;
        }
    }



    [Serializable]
    public class ClassesJson
    {
        public List<ClassJson> Classes { get; set; }
    }


    [Serializable]
    public class ClassJson
    {
        public string ClassName { get; set; }
        public List<ClassProperty> Properties { get; set; }
    }

    [Serializable]
    public class ClassProperty
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

}