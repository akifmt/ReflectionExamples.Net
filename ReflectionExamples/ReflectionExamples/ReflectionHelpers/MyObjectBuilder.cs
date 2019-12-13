using Microsoft.CSharp;
using ReflectionExamples.JsonReaderHelpers;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace ReflectionExamples.ReflectionHelpers
{
    // Reference:
    // https://stackoverflow.com/questions/15641339/create-new-propertyinfo-object-on-the-fly


    public class MyObjectBuilder
    {
        public Type objType { get; set; }
        public string AssemblyName { get; set; }
        public string DynamicModuleName { get; set; }

        public MyObjectBuilder()
        {
            this.objType = null;
        }

        public object CreateNewObject(List<ClassField> fields, List<ClassMethod> methods, string assemblyName, string dynamicModuleName)
        {
            AssemblyName = assemblyName;
            DynamicModuleName = dynamicModuleName;

            this.objType = CompileResultType(fields, methods, assemblyName, dynamicModuleName);
            var myObject = Activator.CreateInstance(this.objType);

            return myObject;
        }

        public static MethodInfo GetCompareToMethod(object genericInstance, string sortExpression)
        {
            Type genericType = genericInstance.GetType();
            object sortExpressionValue = genericType.GetProperty(sortExpression).GetValue(genericInstance, null);
            Type sortExpressionType = sortExpressionValue.GetType();
            MethodInfo compareToMethodOfSortExpressionType = sortExpressionType.GetMethod("CompareTo", new Type[] { sortExpressionType });

            return compareToMethodOfSortExpressionType;
        }

        public static Type CompileResultType(List<ClassField> fields, List<ClassMethod> methods, string assemblyName, string dynamicModuleName)
        {
            TypeBuilder tb = GetTypeBuilder(assemblyName, dynamicModuleName);
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            // NOTE: assuming your list contains Field objects with fields FieldName(string) and FieldType(Type)
            foreach (var field in fields)
                CreateProperty(tb, field.Name, field.Type);

            foreach (var method in methods)
                CreateMethod(tb, method.Name, method.Definition);

            Type objectType = tb.CreateType();
            return objectType;
        }

        private static TypeBuilder GetTypeBuilder(string assemblyName, string dynamicModuleName)
        {
            var typeSignature = assemblyName;
            var an = new AssemblyName(typeSignature);
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(dynamicModuleName);
            TypeBuilder tb = moduleBuilder.DefineType(typeSignature
                                , TypeAttributes.Public |
                                TypeAttributes.Class |
                                TypeAttributes.AutoClass |
                                TypeAttributes.AnsiClass |
                                TypeAttributes.BeforeFieldInit |
                                TypeAttributes.AutoLayout
                                , null);
            return tb;
        }

        private static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType)
        {
            FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr =
                tb.DefineMethod("set_" + propertyName,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null, new[] { propertyType });

            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);
        }

        private static void CreateMethod(TypeBuilder tb, string methodName, string methodDefinition)
        {


            // BU METHOD HAZIRLANACAK!!!


            MethodBuilder methodBuilder = tb.DefineMethod(methodName, MethodAttributes.Public, null, null);


            // https://www.codeproject.com/Tips/715891/Compiling-Csharp-Code-at-Runtime
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerResults results = provider.CompileAssemblyFromSource(new CompilerParameters(), methodDefinition);


        }


        public static void SetMemberValue(MemberInfo member, object target, object value)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    ((FieldInfo)member).SetValue(target, Convert.ChangeType(value, ((FieldInfo)member).FieldType));
                    break;
                case MemberTypes.Property:
                    ((PropertyInfo)member).SetValue(target, Convert.ChangeType(value, ((PropertyInfo)member).PropertyType), null);
                    break;
                default:
                    throw new ArgumentException("MemberInfo must be if type FieldInfo or PropertyInfo", "member");
            }
        }

        public static void SetCollectionMemberValue(MemberInfo member, object target, object values)
        {

            object[] valueElements = ((Newtonsoft.Json.Linq.JArray)values).ToObject<object[]>();
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    // NotImplementedException
                    //((FieldInfo)member).SetValue(target, valueElements);
                    break;
                case MemberTypes.Property:
                    PropertyInfo prop = (PropertyInfo)member;
                    Object collectionInstance = Activator.CreateInstance(prop.PropertyType);
                    prop.SetValue(target, collectionInstance);

                    Type innerType = prop.PropertyType.GetGenericArguments()[0];
                    MethodInfo addMethod = prop.PropertyType.GetMethod("Add");
                    foreach (var value in valueElements)
                    {
                        addMethod.Invoke(prop.GetValue(target), new object[] { Convert.ChangeType(value, innerType) });
                    }

                    break;
                default:
                    throw new ArgumentException("MemberInfo must be if type FieldInfo or PropertyInfo", "member");
            }
        }
    }
}
