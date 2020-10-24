using System;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace TestObfuscator.Modules
{
    //new module
    public class EncodeVariablesModule : ICustomModule
    {
        public ModuleDefMD Module { get; set; }

        public EncodeVariablesModule(ModuleDefMD module)
        {
            Module = module;
        }

        public void Execute()
        {
            foreach (var type in Module.GetTypes())
            {
                foreach (var method in type.Methods)
                {
                    var cilBody = method.Body;
                    foreach (var variable in cilBody.Variables)
                    {
                        for (int i = 0; i < cilBody.Instructions.Count; i++)
                        {
                            var currentOpcode = cilBody.Instructions[i].OpCode;
                            if (currentOpcode == OpCodes.Stloc_0
                                || currentOpcode == OpCodes.Stloc_1
                                || currentOpcode == OpCodes.Stloc_2 ||
                                currentOpcode == OpCodes.Stloc_3)
                            {
                                for (int j = i; j > 0; j--)
                                    if (cilBody.Instructions[j].OpCode == OpCodes.Ldstr)
                                        Console.WriteLine(cilBody.Instructions[j].Operand);

                            }
                        }
                    }
                }
            }
        }
    }
}