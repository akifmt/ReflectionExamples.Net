using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace ReflectionExamples.JsonReaderHelpers
{
    public static class ClassJsonHelper
    {
        public static ClassJsonFile ReadClassFromFile(string path)
        {
            ClassJsonFile classJsonFile = new ClassJsonFile();
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                classJsonFile = JsonConvert.DeserializeObject<ClassJsonFile>(json);
            }
            return classJsonFile;
        }

    }
}
