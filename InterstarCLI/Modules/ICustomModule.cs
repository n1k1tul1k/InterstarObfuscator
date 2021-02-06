using dnlib.DotNet;

namespace InterstarObfuscator.Modules
{
    public interface ICustomModule
    {
         ModuleDefMD ModuleDef { get; set; }
         void Execute();
    }
}