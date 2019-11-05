using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReflectionExamples
{
    class Program
    {
        public const int PADRIGHT_VALUE = 55;
        public static readonly List<TypeInfo> definedTypes = Assembly.GetCallingAssembly().DefinedTypes.ToList();

        static void Main(string[] args)
        {
            //WritePropertiesonBaseClass(Statics.OrnekVeri);

            WritePropertiesonBaseClass(Statics.Data);

            Console.ReadKey();
        }

        static void WritePropertiesonBaseClass(object obj)
        {
            

            Type typeBase = obj.GetType();
            PropertyInfo[] propertyInfosBase = typeBase.GetProperties();

            Console.WriteLine(string.Format("Base Object: {0}", typeBase.Name));

            foreach (var propertyInfoBase in propertyInfosBase)
            {
                Type propTypeBase = propertyInfoBase.PropertyType;

                if (propTypeBase.Namespace == "System")
                {
                    // Built-in types
                    ProcessingBuiltinTypes(propertyInfoBase, obj);
                }
                else if (definedTypes.Contains(propTypeBase))
                {
                    // User defined classes
                    ProcessingClassTypes(propertyInfoBase, obj);
                }
                else if (propTypeBase.Namespace == "System.Collections.Generic")
                {
                    // Collection types
                    ProcessingGenericCollection(propertyInfoBase, obj);
                }
                else
                {
                    Console.WriteLine("UNDEFINED TYPE: " + propertyInfoBase.Name);
                }
            }
        }

        static void ProcessingBuiltinTypes(PropertyInfo propertyInfo, object data, string prefix = "|-")
        {
            string formatted = string.Format("{0}{1}({2})", prefix, propertyInfo.Name, propertyInfo.PropertyType.Name).PadRight(PADRIGHT_VALUE, '-');
            Console.WriteLine(formatted + "Value:" + propertyInfo.GetValue(data));
        }

        /// <summary>
        /// for inner Collections
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <param name="prefix"></param>
        static void ProcessingBuiltinTypes(Type type, object value, int? index = null, string prefix = "|-")
        {
            string formatted = "";
            if (index == null)
                formatted = string.Format("{0}({1})", prefix, type.Name).PadRight(PADRIGHT_VALUE, '-');
            else
                formatted = string.Format("{0}{1}({2})", prefix, index, type.Name).PadRight(PADRIGHT_VALUE, '-');
                
            Console.WriteLine(formatted + "Value:" + value);
        }

        static void ProcessingClassTypes(PropertyInfo propertyInfo, object data, string prefix = "|-")
        {
            Type classType = propertyInfo.PropertyType;
            var classDatas = propertyInfo.GetValue(data);
            PropertyInfo[] classPropertyInfos = classType.GetProperties();

            Console.WriteLine(string.Format("{0}{1}({2}(Class))", prefix, propertyInfo.Name, propertyInfo.PropertyType.Name));

            foreach (var classProp in classPropertyInfos)
            {
                string formatted = string.Format("{0}|-{1}({2}) ", prefix, classProp.Name, classProp.PropertyType.Name).PadRight(PADRIGHT_VALUE, '-');
                Console.WriteLine(formatted + "Value:" + classProp.GetValue(classDatas));
            }
        }

        /// <summary>
        /// for inner Collections
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <param name="prefix"></param>
        static void ProcessingClassTypes(Type type, object data, int? index = null, string prefix = "|-")
        {
            PropertyInfo[] classPropertyInfos = type.GetProperties();

            Console.WriteLine(string.Format("{0}({1})({2}(Class))", prefix, index, type.Name));

            foreach (var classProp in classPropertyInfos)
            {
                string formatted = string.Format("{0}|-{1}({2})", prefix, classProp.Name, classProp.PropertyType.Name).PadRight(PADRIGHT_VALUE, '-');
                Console.WriteLine(formatted + "Value:" + classProp.GetValue(data));
            }
        }

        static void ProcessingGenericCollection(PropertyInfo propertyInfo, object data, string prefix = "|-")
        {
            var dataICollection = propertyInfo.GetValue(data) as System.Collections.ICollection;
            Type typeInner = propertyInfo.PropertyType.GetGenericArguments()[0];

            Console.WriteLine(string.Format("{0}{1}({2}<{3}>) Count:{4}", prefix, propertyInfo.Name, propertyInfo.PropertyType.Name, typeInner.Name, dataICollection.Count));

            if (typeInner.Namespace == "System")
            {
                // Built-in types
                int i = 0;
                foreach (var dataChild in dataICollection)
                {
                    ProcessingBuiltinTypes(typeInner, dataChild, i, prefix + prefix);
                    i++;
                }
            }
            else if (definedTypes.Contains(typeInner))
            {
                // User defined classes collection
                int j = 0;
                foreach (var dataChild in dataICollection)
                {
                    ProcessingClassTypes(typeInner, dataChild, j);
                    j++;
                }
            }
            else if (typeInner.Namespace == "System.Collections.Generic")
            {
                // Collection types collection
                ProcessingGenericCollection(propertyInfo, data);
            }
            else
            {
                Console.WriteLine("UNDEFINED TYPE: " + propertyInfo.Name);
            }

        }

    }
}
