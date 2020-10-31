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
            var path = 
                @"C:\Users\bitdd\RiderProjects\TestObfuscator\TestProject1\bin\Debug\TestProject1.exe";
            var fileName = new FileInfo(path).Name;
            ModuleDefMD module = ModuleDefMD.Load(path);
            List<ICustomModule> modules = new List<ICustomModule>()
            {
                //new RenamerModule(module,20),
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