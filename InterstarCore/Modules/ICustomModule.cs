using dnlib.DotNet;

namespace InterstarCore.Modules
{
    public interface ICustomModule
    {
         ModuleDefMD ModuleDef { get; set; }
         void Execute();
    }
}