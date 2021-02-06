using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using InterstarCore.Helpers;

namespace InterstarCore.Modules
{
    public class EncodeVariablesModule : ICustomModule
    {
        public ModuleDefMD ModuleDef { get; set; }

        public EncodeVariablesModule(ModuleDefMD module) => ModuleDef = module;
        public void Execute()
        {
            MethodDef decryptMethod = InjectMethod(ModuleDef, "Decrypt_Base64");

            foreach (TypeDef type in ModuleDef.Types)
            {
                if (type.IsGlobalModuleType || type.Name == "Resources" || type.Name == "Settings")
                    continue;

                foreach (MethodDef method in type.Methods)
                {
                    if (!method.HasBody)
                        continue;
                    if (method == decryptMethod)
                        continue;

                    method.Body.KeepOldMaxStack = true;

                    for (int i = 0; i < method.Body.Instructions.Count; i++)
                    {
                        if (method.Body.Instructions[i].OpCode == OpCodes.Ldstr)	// String
                        {
                            string oldString = method.Body.Instructions[i].Operand.ToString();	//Original String

                            method.Body.Instructions[i].Operand = Base64Encode(oldString);
                            method.Body.Instructions.Insert(i + 1, new Instruction(OpCodes.Call, decryptMethod));
                        }
                    }

                    method.Body.SimplifyBranches();
                    method.Body.OptimizeBranches();
                }
            }

        }
        private static MethodDef InjectMethod(ModuleDef module, string methodName)
        {
            ModuleDefMD typeModule = ModuleDefMD.Load(typeof(DecryptionHelper).Module);
            TypeDef typeDef = typeModule.ResolveTypeDef(MDToken.ToRID(typeof(DecryptionHelper).MetadataToken));
            IEnumerable<IDnlibDef> members = InjectHelper.Inject(typeDef, module.GlobalType, module);
            MethodDef injectedMethodDef = (MethodDef)members.Single(method => method.Name == methodName);

            foreach (MethodDef md in module.GlobalType.Methods)
            {
                if (md.Name == ".ctor")
                {
                    module.GlobalType.Remove(md);
                    break;
                }
            }

            return injectedMethodDef;
        }
        public  string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        private  ModuleDef EncryptStrings(ModuleDef inModule)
        {
            ModuleDef module = inModule;
            MethodDef decryptMethod = InjectMethod(module, "Decrypt_Base64");

            foreach (TypeDef type in module.Types)
            {
                if (type.IsGlobalModuleType || type.Name == "Resources" || type.Name == "Settings")
                    continue;

                foreach (MethodDef method in type.Methods)
                {
                    if (!method.HasBody)
                        continue;
                    if (method == decryptMethod)
                        continue;

                    method.Body.KeepOldMaxStack = true;

                    for (int i = 0; i < method.Body.Instructions.Count; i++)
                    {
                        if (method.Body.Instructions[i].OpCode == OpCodes.Ldstr)	// String
                        {
                            string oldString = method.Body.Instructions[i].Operand.ToString();	//Original String

                            method.Body.Instructions[i].Operand = Base64Encode(oldString);
                            method.Body.Instructions.Insert(i + 1, new Instruction(OpCodes.Call, decryptMethod));
                        }
                    }

                    method.Body.SimplifyBranches();
                    method.Body.OptimizeBranches();
                }
            }

            return module;
        }
    }
}