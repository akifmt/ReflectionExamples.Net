﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExamples
{
    // Reference:
    // https://stackoverflow.com/questions/15641339/create-new-propertyinfo-object-on-the-fly


    public class MyObjectBuilder
    {
        public Type objType { get; set; }

        public MyObjectBuilder()
        {
            this.objType = null;
        }

        public object CreateNewObject(List<ClassField> Fields)
        {
            this.objType = CompileResultType(Fields);
            var myObject = Activator.CreateInstance(this.objType);

            return myObject;
        }

        public IList getObjectList()
        {
            Type listType = typeof(List<>).MakeGenericType(this.objType);

            return (IList)Activator.CreateInstance(listType);
        }

        public static MethodInfo GetCompareToMethod(object genericInstance, string sortExpression)
        {
            Type genericType = genericInstance.GetType();
            object sortExpressionValue = genericType.GetProperty(sortExpression).GetValue(genericInstance, null);
            Type sortExpressionType = sortExpressionValue.GetType();
            MethodInfo compareToMethodOfSortExpressionType = sortExpressionType.GetMethod("CompareTo", new Type[] { sortExpressionType });

            return compareToMethodOfSortExpressionType;
        }

        public static Type CompileResultType(List<ClassField> Fields)
        {
            TypeBuilder tb = GetTypeBuilder();
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            // NOTE: assuming your list contains Field objects with fields FieldName(string) and FieldType(Type)
            foreach (var field in Fields)
                CreateProperty(tb, field.Name, field.Type);

            Type objectType = tb.CreateType();
            return objectType;
        }

        private static TypeBuilder GetTypeBuilder()
        {
            var typeSignature = "MyDynamicType";
            var an = new AssemblyName(typeSignature);
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
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


        public static void SetMemberValue(MemberInfo member, object target, object value)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    ((FieldInfo)member).SetValue(target, value);
                    break;
                case MemberTypes.Property:
                    ((PropertyInfo)member).SetValue(target, value, null);
                    break;
                default:
                    throw new ArgumentException("MemberInfo must be if type FieldInfo or PropertyInfo", "member");
            }
        }

        public static void SetCollectionMemberValue(MemberInfo member, object target, object value)
        {
            object[] valueElements = ((Newtonsoft.Json.Linq.JArray)value).ToObject<object[]>();
            object[] indexElements = Enumerable.Range(0, valueElements.Length).OfType<object>().ToArray();

            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    ((FieldInfo)member).SetValue(target, valueElements);
                    break;
                case MemberTypes.Property:

                    //  System.Reflection.TargetParameterCountException: 'Parameter count mismatch.'
                    ((PropertyInfo)member).SetValue(target, valueElements, indexElements);
                    break;
                default:
                    throw new ArgumentException("MemberInfo must be if type FieldInfo or PropertyInfo", "member");
            }
        }
    }
}
