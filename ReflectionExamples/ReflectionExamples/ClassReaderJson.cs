using Newtonsoft.Json;
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
        public List<ClassField> ClassFields { get; set; }
    }

    [Serializable]
    public class ClassField
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public object Value { get; set; }

        public Type Type {
            get {
                return Type.GetType(TypeName);
            }
        }
    }

}
