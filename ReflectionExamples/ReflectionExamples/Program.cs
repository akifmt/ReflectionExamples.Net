using ReflectionExamples.JsonReaderHelpers;
using ReflectionExamples.MockData;
using ReflectionExamples.ReflectionHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ReflectionExamples
{
    class Program
    {
        public const string CLASS_CONFIG_FILE = @"my_classes.json";

        private const int PADRIGHT_VALUE = ConsoleWriteHelper.PADRIGHT_VALUE;

        static void Main(string[] args)
        {
            char selected = '\0';
            while (Char.ToLower(selected) != 'e')
            {
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine(" ".PadRight(PADRIGHT_VALUE, '_'));
                Console.WriteLine("|".PadRight(PADRIGHT_VALUE, ' ') + "|");
                Console.WriteLine("| 1- For SampleClass".PadRight(PADRIGHT_VALUE, ' ') + "|");
                Console.WriteLine("| 2- For SampleAllTypesData".PadRight(PADRIGHT_VALUE, ' ') + "|");
                Console.WriteLine("| 3- For ReadClassFromFile(classesconfig.json)".PadRight(PADRIGHT_VALUE, ' ') + "|");
                Console.WriteLine("| e- EXIT".PadRight(PADRIGHT_VALUE, ' ') + "|");
                Console.WriteLine("|".PadRight(PADRIGHT_VALUE, '_') + "|");
                Console.Write("select: ");

                selected = Console.ReadKey().KeyChar;
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine(" ".PadRight(PADRIGHT_VALUE, '_'));
                Console.WriteLine("|".PadRight(PADRIGHT_VALUE, ' ') + "|");
                switch (Char.ToLower(selected))
                {
                    case '1':
                        ConsoleWriteHelper.WritePropertiesonBaseClass(MockDataStatics.SampleClass);
                        break;
                    case '2':
                        ConsoleWriteHelper.WritePropertiesonBaseClass(MockDataStatics.SampleAllTypesData);
                        break;
                    case '3':
                        ClassJsonFile classJsonFile = ClassJsonHelper.ReadClassFromFile(CLASS_CONFIG_FILE);
                        IList objectList = ReflectionHelper.PrepareJsonClasses(classJsonFile);
                        foreach (var obj in objectList)
                            ConsoleWriteHelper.WritePropertiesonBaseClass(obj);
                        break;
                    case 'e':
                        return;
                    default:
                        Console.WriteLine("| Wrong key....".PadRight(PADRIGHT_VALUE, ' ') + "|");
                        break;
                }
                Console.WriteLine("|".PadRight(PADRIGHT_VALUE, '_') + "|");
                Console.Write("To continue press any key...");
                Console.ReadKey();
            }
        }

    }


}
