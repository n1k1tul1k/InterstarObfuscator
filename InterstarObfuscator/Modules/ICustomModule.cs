using dnlib.DotNet;

namespace TestObfuscator.Modules
{
    public interface ICustomModule
    {
         ModuleDefMD ModuleDef { get; set; }
         void Execute();
    }
}