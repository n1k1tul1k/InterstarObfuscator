using dnlib.DotNet;

namespace InterstarObfuscator
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