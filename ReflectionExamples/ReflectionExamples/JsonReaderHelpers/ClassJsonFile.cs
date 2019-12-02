using System;
using System.Collections.Generic;

namespace ReflectionExamples.JsonReaderHelpers
{
    [Serializable]
    public class ClassJsonFile
    {
        public string AssemblyName { get; set; }
        public string DynamicModuleName { get; set; }
        public List<ClassJson> Classes { get; set; }
    }


}
