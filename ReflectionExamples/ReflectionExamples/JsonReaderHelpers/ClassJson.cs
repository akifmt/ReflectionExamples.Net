using System;
using System.Collections.Generic;

namespace ReflectionExamples.JsonReaderHelpers
{
    [Serializable]
    public class ClassJson
    {
        public string ClassName { get; set; }
        public List<ClassField> ClassFields { get; set; }
    }
}
