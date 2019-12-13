using ReflectionExamples.JsonReaderHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ReflectionExamples.ReflectionHelpers
{
    public static class ReflectionHelper
    {
        public static IList PrepareJsonClasses(ClassJsonFile classData)
        {
            IList objList = null;
            foreach (var aclass in classData.Classes)
            {
                string className = aclass.ClassName;
                List<ClassField> classProperties = aclass.ClassFields;

                // Methods
                var classMethods = aclass.ClassMethods;

                //MyObjectBuilder Class
                MyObjectBuilder o = new MyObjectBuilder();

                //Creating a new object dynamically
                object newObj = o.CreateNewObject(classProperties, classMethods, classData.AssemblyName, classData.DynamicModuleName);

                Type listType = typeof(List<>).MakeGenericType(o.objType);
                objList = (IList)Activator.CreateInstance(listType);

                Type t = newObj.GetType();
                object instance = Activator.CreateInstance(t);

                PropertyInfo[] props = instance.GetType().GetProperties();

                int instancePropsCount = props.Count();

                for (int i = 0; i < instancePropsCount; ++i)
                {
                    string fieldName = props[i].Name;
                    MemberInfo[] mInfo = null;
                    PropertyInfo pInfo = newObj.GetType().GetProperty(fieldName);

                    var prop = classProperties.Find(x => x.Name == fieldName);
                    object value = null;
                    if (prop != null)
                        value = prop.Value;

                    if (pInfo != null)
                    {
                        //var value = pInfo.GetValue(newObj, null);
                        mInfo = t.GetMember(fieldName);

                        if (value != null && mInfo != null && !string.IsNullOrEmpty(mInfo[0].ToString()))
                        {
                            if (typeof(ICollection).IsAssignableFrom(((PropertyInfo)mInfo[0]).PropertyType))
                            {
                                // collection type assign values
                                MyObjectBuilder.SetCollectionMemberValue(mInfo[0], instance, value);
                            }
                            else
                            {
                                MyObjectBuilder.SetMemberValue(mInfo[0], instance, value);
                            }
                        }

                    }
                    else
                    {
                        mInfo = t.GetMember(fieldName);

                        if (mInfo != null && !string.IsNullOrEmpty(mInfo[0].ToString()))
                            MyObjectBuilder.SetMemberValue(mInfo[0], instance, null);
                    }


                }

                objList.Add(instance);

            }


            return objList;
        }

        
    }
}
