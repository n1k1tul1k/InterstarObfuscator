using dnlib.DotNet;

namespace InterstarCore.Modules
{
    public class MsilBodyDecryptionModule
    {
        public ModuleDefMD ModuleDefMd { get; set; }
        public MsilBodyDecryptionModule(ModuleDefMD moduleDefMd)
        {
            ModuleDefMd = moduleDefMd;
        }
    }
}