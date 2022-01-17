using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace HttpMessenger
{
    /// <summary>
    /// </summary>
    class Functions
    {

        #region Autostart

        #region variable
        const string registryRunPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        #endregion

        #region ExistsInAutostart, AddToAutostart, RemoveAutostart
        /// <summary>
        /// Existses the in autostart.
        /// </summary>
        /// <returns></returns>
        public static bool ExistsInAutostart()
        {
            string appName = Application.ProductName;
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registryRunPath, true);
            string appPath = (string)registryKey.GetValue(appName);
            return !string.IsNullOrEmpty(appPath);
        }
        /// <summary>
        /// Adds to autostart.
        /// https://dotnet-snippets.de/snippet/auto-start-util/15153
        /// https://dotnet-snippets.de/snippet/autostart-eintrag-hinzufuegen-registrykey/12084
        /// </summary>
        public static void AddToAutostart()
        {
            if (ExistsInAutostart()) return;
            var appExecutablePath = Application.ExecutablePath;
            string appName = Application.ProductName;
            string appPath = Application.ExecutablePath;
            if (Path.HasExtension(appPath))
            {
                RegistryKey startKey = Registry.LocalMachine.OpenSubKey(registryRunPath, true);
                startKey.SetValue(appName, appPath);
            }
        }
        /// <summary>
        /// Removes the autostart.
        /// </summary>
        public static void RemoveAutostart()
        {
            if (!ExistsInAutostart()) return;
            string appName = Application.ProductName;
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registryRunPath, true);
            registryKey.DeleteValue(appName, true);
        }
        #endregion
    }
}
