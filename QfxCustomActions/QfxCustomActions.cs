using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QfxCustomActions
{
    [RunInstaller(true)]
    public partial class QfxCustomActions : Installer
    {
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
        }

        public override void Commit(IDictionary savedState)
        {
            Registry.SetValue("HKEY_CLASSES_ROOT\\SystemFileAssociations\\.qfx\\Shell\\Qbo_Converter", "", "Convert to QBO File");
            Registry.SetValue("HKEY_CLASSES_ROOT\\SystemFileAssociations\\.qfx\\Shell\\Qbo_Converter\\Command", "", "\"C:\\Program Files (x86)\\QfxConverter\\QfxConverter.exe\" \"%1\"");
            base.Commit(savedState);
        }

        public override void Uninstall(IDictionary savedState)
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey("SystemFileAssociations").OpenSubKey(".qfx", true);
            if (key != null)
            {
                key.DeleteSubKeyTree("Shell"); 
            }
            base.Uninstall(savedState);
        }
    }
}
