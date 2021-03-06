using System;
using System.Linq;
using dnlib.DotNet;

namespace InterstarCore.Modules
{
    public class RenamerModule : ICustomModule
    {
        public ModuleDefMD ModuleDef { get; set; }
        public int RandomStringLenght { get; set; }
        private static Random _random = new Random();
        private int _countOfChanges;

        public RenamerModule(ModuleDefMD module, int randomStringLenght)
        {
            ModuleDef = module;
            RandomStringLenght = randomStringLenght;
        }

        public void Execute()
        {
            
            foreach (var type in ModuleDef.GetTypes())
            {
                type.Name = RandomString();
                foreach (var methodDef in type.Methods.Where(x => !x.IsConstructor && !x.IsVirtual))
                {
                    methodDef.Name = RandomString();
                    if (methodDef.HasBody && methodDef.Body.HasVariables)
                        foreach (var bodyVariable in methodDef.Body.Variables)
                            bodyVariable.Name = RandomString();
                }

                foreach (var eventDef in type.Events)
                    eventDef.Name = RandomString();
                foreach (var fieldDef in type.Fields)
                    fieldDef.Name = RandomString();
                type.Namespace = RandomString();
            }

        }

        public string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, RandomStringLenght)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}