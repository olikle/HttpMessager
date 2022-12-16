using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System;

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
        public static bool ExistsInAutostart(bool showErrorMessagr)
        {
            string appPath = "";
            string appName = Application.ProductName;
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registryRunPath, true);
                appPath = (string)registryKey.GetValue(appName);
            }catch(Exception ex)
            {
                if (!showErrorMessagr) return true;
                MessageBox.Show($"ExistsInAutostart Error: {ex.Message}\nApp: {appName}\nregistryRunPath: {registryRunPath}\n\ne.g. Permissions missing.");
                return true;
            }
            return !string.IsNullOrEmpty(appPath);
        }
        /// <summary>
        /// Adds to autostart.
        /// https://dotnet-snippets.de/snippet/auto-start-util/15153
        /// https://dotnet-snippets.de/snippet/autostart-eintrag-hinzufuegen-registrykey/12084
        /// </summary>
        public static void AddToAutostart()
        {
            if (ExistsInAutostart(true)) return;
            string appExecutablePath = "", appName = "", appPath = "";
            try
            {
                appExecutablePath = Application.ExecutablePath;
                appName = Application.ProductName;
                appPath = Application.ExecutablePath;
                if (Path.HasExtension(appPath))
                {
                    RegistryKey startKey = Registry.LocalMachine.OpenSubKey(registryRunPath, true);
                    startKey.SetValue(appName, appPath);
                }
            }catch(Exception ex)
            {
                MessageBox.Show($"AddToAutostart Error: {ex.Message}\nappName: {appName}\nappPath: {appPath}\nappExecutablePath: {appExecutablePath}\nregistryRunPath: {registryRunPath}\n\ne.g. Permissions missing.");
            }
        }
        /// <summary>
        /// Removes the autostart.
        /// </summary>
        public static void RemoveAutostart()
        {
            if (!ExistsInAutostart(true)) return;
            string appName = "";
            try
            {
                appName = Application.ProductName;
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registryRunPath, true);
                registryKey.DeleteValue(appName, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"RemoveAutostart Error: {ex.Message}\nappName: {appName}\nregistryRunPath: {registryRunPath}\n\ne.g. Permissions missing.");
            }
        }
        #endregion

        #endregion
    }
}
