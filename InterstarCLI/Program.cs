using System;
using System.Collections.Generic;
using System.IO;
using dnlib.DotNet;
using InterstarObfuscator.Modules;

namespace InterstarObfuscator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Drop your file (.exe only)");
            var path = Console.ReadLine().Replace("\"",String.Empty);
            Console.WriteLine("Enter ");
            var fileName = new FileInfo(path).Name;
            ModuleDefMD module = ModuleDefMD.Load(path);
            List<ICustomModule> modules = new List<ICustomModule>()
            {
                new EncodeVariablesModule(module),
                new RenamerModule(module,24),
            };
            ExecuteAllModules(modules);
            module.Write(fileName);
        }

        public static void ExecuteAllModules(List<ICustomModule> modules)
        {
            foreach (var module in modules)
                module.Execute();
        }
    }
}