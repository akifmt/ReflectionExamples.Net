using System;
using System.Text.RegularExpressions;

namespace ReflectionExamples.JsonReaderHelpers
{
    [Serializable]
    public class ClassField
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public object Value { get; set; }

        public Type Type {
            get {
                string formatted1 = TypeName;
                if (TypeName.Contains("<"))
                {
                    string collectionTypeName = formatted1.Split('<')[0] + "`1";
                    string innerType = Regex.Match(formatted1, @"\<([^)]*)\>").Groups[1].Value;
                    string collectionTypeNamewithInnerType = collectionTypeName + "[" + innerType + "]";
                    return Type.GetType(collectionTypeNamewithInnerType); ;
                }
                else if (TypeName.Contains("["))
                {
                    string collectionTypeName = formatted1.Split('[')[0] + "`1";
                    string innerType = Regex.Match(formatted1, @"\[([^)]*)\]").Groups[1].Value;
                    string collectionTypeNamewithInnerType = collectionTypeName + "[" + innerType + "]";
                    return Type.GetType(collectionTypeNamewithInnerType); ;
                }
                else
                    return Type.GetType(TypeName);

            }
        }
    }
}
