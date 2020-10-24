using dnlib.DotNet;

namespace TestObfuscator.Modules
{
    public interface ICustomModule
    {
        public ModuleDefMD Module { get; set; }
        public void Execute();
    }
}