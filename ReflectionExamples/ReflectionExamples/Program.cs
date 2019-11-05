using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReflectionExamples
{
    class Program
    {
        public const string PROJECT_NAME = "ReflectionExamples";


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

                switch (propTypeBase.Namespace)
                {
                    case "System":
                        // Built-in types
                        ProcessingBuiltinTypes(propertyInfoBase, obj);
                        break;

                    case PROJECT_NAME:
                        // User defined classes
                        ProcessingClassTypes(propertyInfoBase, obj);

                        break;

                    case "System.Collections.Generic":
                        // Collection types
                        ProcessingGenericCollection(propertyInfoBase, obj);
                        break;

                    default:
                        break;
                }

            }
        }

        static void ProcessingBuiltinTypes(PropertyInfo propertyInfo, object data, string prefix = "|-")
        {
            Console.WriteLine(string.Format("{0}{1}({2}) \t\tValue:{3}", prefix, propertyInfo.Name, propertyInfo.PropertyType.Name, propertyInfo.GetValue(data)));
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
            if (index == null)
                Console.WriteLine(string.Format("{0}({1}) \t\tValue:{2}", prefix, type.Name, value));
            else
                Console.WriteLine(string.Format("{0}{1}({2}) \t\tValue:{3}", prefix, index, type.Name, value));
        }

        static void ProcessingClassTypes(PropertyInfo propertyInfo, object data, string prefix = "|-")
        {
            Type classType = propertyInfo.PropertyType;
            var classDatas = propertyInfo.GetValue(data);
            PropertyInfo[] classPropertyInfos = classType.GetProperties();

            Console.WriteLine(string.Format("{0}{1}({2}(Class))", prefix, propertyInfo.Name, propertyInfo.PropertyType.Name));

            foreach (var classProp in classPropertyInfos)
            {
                Console.WriteLine(string.Format("{0}|-{1}({2}) \t\t\tValue:{3}", prefix, classProp.Name, classProp.PropertyType.Name, classProp.GetValue(classDatas)));
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
                Console.WriteLine(string.Format("{0}|-{1}({2}) \t\t\tValue:{3}", prefix, classProp.Name, classProp.PropertyType.Name, classProp.GetValue(data)));
            }
        }

        static void ProcessingGenericCollection(PropertyInfo propertyInfo, object data, string prefix = "|-")
        {
            var dataICollection = propertyInfo.GetValue(data) as System.Collections.ICollection;
            Type typeInner = propertyInfo.PropertyType.GetGenericArguments()[0];

            Console.WriteLine(string.Format("{0}{1}({2}<{3}>) \t\tCount:{4}", prefix, propertyInfo.Name, propertyInfo.PropertyType.Name, typeInner.Name, dataICollection.Count));

            switch (typeInner.Namespace)
            {
                case "System":
                    // Built-in types collection

                    int i = 0;
                    foreach (var dataChild in dataICollection)
                    {
                        ProcessingBuiltinTypes(typeInner, dataChild, i, prefix + prefix);
                        i++;
                    }
                    break;

                case PROJECT_NAME:
                    // User defined classes collection
                    int j = 0;
                    foreach (var dataChild in dataICollection)
                    {
                        ProcessingClassTypes(typeInner, dataChild, j);
                        j++;
                    }
                    break;

                case "System.Collections.Generic":
                    // Collection types collection
                    ProcessingGenericCollection(propertyInfo, data);
                    break;
                default:
                    break;
            }

        }

    }
}
