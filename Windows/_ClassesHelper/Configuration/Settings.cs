
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace okTools
{
    #region Settings
    /// <summary>
    /// Load/Save Settings in Register
    /// </summary>
    class Settings
    {
        #region Enumeration
        /// <summary>
        /// RegistryLocation Types
        /// </summary>
        public enum RegistryLocation
        {
            /// <summary></summary>
            CurrentUser,
            /// <summary></summary>
            CurrentUserCompany,
            /// <summary></summary>
            CurrentUserCompanyApp,
            /// <summary></summary>
            LocalMachine,
            /// <summary></summary>
            LocalMachineCompany,
            /// <summary></summary>
            LocalMachineCompanyApp
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        public static string CompanyName
        {
            get {
                SetKeyVariables();
                return _companyName;
            }
        }
        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        public static string ApplicationName
        {
            get
            {
                SetKeyVariables();
                return _applicationName;
            }
        }
        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        public static string ApplicationVersion
        {
            get
            {
                SetKeyVariables();
                return _applicationVersion;
            }
        }
        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        public static string ApplicationFileVersion
        {
            get
            {
                SetKeyVariables();
                return _applicationFileVersion;
            }
        }
        #endregion

        #region variable
        private static System.Reflection.Assembly entryAssembly = (System.Reflection.Assembly.GetEntryAssembly() == null) ? System.Reflection.Assembly.GetCallingAssembly() : System.Reflection.Assembly.GetEntryAssembly();
        private static string _companyName = null;
        private static string _applicationName = null;
        private static string _applicationVersion = null;
        private static string _applicationFileVersion = null;
        private static string _companyKey = null;
        private static string _companyAppKey = null;
        #endregion

        #region set key variables
        /// <summary>
        /// Sets the key variables.
        /// </summary>
        public static void SetKeyVariables()
        {
            if (!string.IsNullOrEmpty(_companyName)) return;




            _companyName = System.Windows.Forms.Application.CompanyName;
            _applicationName = "App";
            _applicationVersion = "1.0.0";
            _applicationFileVersion = "1.0.0";
            if (entryAssembly != null)
            {
                _applicationName = entryAssembly.GetName().Name;
                _applicationVersion = entryAssembly.GetName().Version?.ToString();
                //_applicationFileVersion = entryAssembly == null ? "1.0.0" : entryAssembly.GetName().;
                FileVersionInfo fileVersion = FileVersionInfo.GetVersionInfo(entryAssembly.Location);
                _applicationFileVersion = fileVersion.FileVersion;
            }
            // the _companyKey
            _companyKey = $"SOFTWARE\\{_companyName}";
            // the _companyAppKey
            _companyAppKey += $"SOFTWARE\\{_companyName}\\{_applicationName}";
        }
        #endregion


        #region WindowSizeLoad, WindowSizeSave
        /// <summary>
        /// Load the last saved windows size
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="windowRegistryPath">The sub registry path. default/empty = Windows, {windowRegistryPath}\\Windows.</param>
        public static void WindowSizeLoad(System.Windows.Forms.Form form, string windowRegistryPath = "")
        {
            // get the last saved size: Format top, left, height, width, windowstate
            var savedSize = GetRegistryValue(RegistryLocation.CurrentUserCompanyApp, string.IsNullOrEmpty(windowRegistryPath) ? "Windows" : $"{windowRegistryPath}\\Windows", form.Name);
            if (string.IsNullOrEmpty(savedSize)) return;

            string[] savedSizeS = savedSize.Split(',');
            // top, left
            if (int.Parse(savedSizeS[0]) != 0) form.Top = int.Parse(savedSizeS[0]);
            if (int.Parse(savedSizeS[1]) != 0) form.Left = int.Parse(savedSizeS[1]);
            // height, width
            if (form.FormBorderStyle == System.Windows.Forms.FormBorderStyle.Sizable || form.FormBorderStyle == System.Windows.Forms.FormBorderStyle.SizableToolWindow)
            {
                if (int.Parse(savedSizeS[2]) > 30) form.Height = int.Parse(savedSizeS[2]);
                if (int.Parse(savedSizeS[3]) > 30) form.Width = int.Parse(savedSizeS[3]);
            }
            // state
            form.WindowState = (System.Windows.Forms.FormWindowState)Enum.Parse(typeof(System.Windows.Forms.FormWindowState), savedSizeS[4], true);
        }
        /// <summary>
        /// Windows the size save.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="windowRegistryPath">The sub registry path. default/empty = Windows, {windowRegistryPath}\\Windows.</param>
        public static void WindowSizeSave(System.Windows.Forms.Form form, string windowRegistryPath = "")
        {
            if (form.WindowState == System.Windows.Forms.FormWindowState.Minimized) return;

            // set the current size: Format top, left, height, width, windowstate
            string saveSize = form.Top.ToString() + "," + form.Left.ToString() + "," + form.Height.ToString() + "," + form.Width.ToString() + "," + form.WindowState.ToString();
            SetRegistryValue(RegistryLocation.CurrentUserCompanyApp, string.IsNullOrEmpty(windowRegistryPath) ? "Windows" : $"{windowRegistryPath}\\Windows", form.Name, saveSize);
        }
        #endregion

        #region GetLastValue, SetLastValue
        /// <summary>
        /// Gets the last value.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="Name">The name.</param>
        /// <returns></returns>
        public static string GetLastValue(System.Windows.Forms.Form form, string name)
        {
            return GetLastValue(name, $"{form.Name}");
        }
        /// <summary>
        /// Gets the last value.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="windowRegistryPath">The window registry path.</param>
        /// <returns></returns>
        public static string GetLastValue(string name, string windowRegistryPath = "")
        {
            var savedValue = GetRegistryValue(RegistryLocation.CurrentUserCompanyApp, string.IsNullOrEmpty(windowRegistryPath) ? "LastValue" : $"{windowRegistryPath}\\LastValue", name);
            if (string.IsNullOrEmpty(savedValue)) return "";
            return savedValue;
        }
        /// <summary>
        /// Sets the last value.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static void SetLastValue(System.Windows.Forms.Form form, string name, string value)
        {
            SetLastValue(name, $"{form.Name}", value);
        }
        /// <summary>
        /// Sets the last value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="windowRegistryPath">The window registry path.</param>
        public static void SetLastValue(string name, string value, string windowRegistryPath = "")
        {
            SetRegistryValue(RegistryLocation.CurrentUserCompanyApp, string.IsNullOrEmpty(windowRegistryPath) ? "LastValue" : $"{windowRegistryPath}\\LastValue", name, value);
        }
        #endregion

        #region GetRegistryKey, GetRegistryValue
        /// <summary>
        /// Gets the registry value.
        /// </summary>
        /// <param name="registryLocation">The registry location.</param>
        /// <param name="subKey">The sub key.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public static string GetRegistryValue(RegistryLocation registryLocation, string subKey, string fieldName)
        {
            RegistryKey returnRegKey = GetRegistryKey(registryLocation, subKey);
            if (returnRegKey == null) return "";
            var value = returnRegKey.GetValue(fieldName);
            if (value == null) return "";
            return value.ToString();
        }
        /// <summary>
        /// Gets the registry key.
        /// </summary>
        /// <param name="registryLocation">The registry location.</param>
        /// <param name="subKey">The sub key.</param>
        /// <returns></returns>
        public static RegistryKey GetRegistryKey(RegistryLocation registryLocation, string subKey, bool writeable = false)
        {
            SetKeyVariables();
            RegistryKey returnRegKey = null;
            if (registryLocation == RegistryLocation.CurrentUserCompany || registryLocation == RegistryLocation.CurrentUserCompanyApp)
            {
                returnRegKey = Registry.CurrentUser.OpenSubKey(GetSubKeyPath(registryLocation, subKey), writeable);
            }
            if (registryLocation == RegistryLocation.LocalMachineCompany || registryLocation == RegistryLocation.LocalMachineCompanyApp)
            {
                returnRegKey = Registry.LocalMachine.OpenSubKey(GetSubKeyPath(registryLocation, subKey), writeable);
            }
            return returnRegKey;
        }
        #endregion

        #region SetRegistryValue
        /// <summary>
        /// Sets the registry value.
        /// </summary>
        /// <param name="registryLocation">The registry location.</param>
        /// <param name="subKey">The sub key.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.Exception">Cannot create subkey {GetSubKeyPath(registryLocation, subKey)}</exception>
        public static void SetRegistryValue(RegistryLocation registryLocation, string subKey, string fieldName, string value)
        {
            RegistryKey returnRegKey = GetRegistryKey(registryLocation, subKey, true);
            // create new key
            if (returnRegKey == null)
            {
                if (registryLocation == RegistryLocation.CurrentUserCompany || registryLocation == RegistryLocation.CurrentUserCompanyApp)
                    returnRegKey = Registry.CurrentUser.CreateSubKey(GetSubKeyPath(registryLocation, subKey));
                if (registryLocation == RegistryLocation.LocalMachineCompany || registryLocation == RegistryLocation.LocalMachineCompanyApp)
                    returnRegKey = Registry.LocalMachine.CreateSubKey(GetSubKeyPath(registryLocation, subKey));
            }
            if (returnRegKey == null)
                throw new Exception($"Cannot create subkey {GetSubKeyPath(registryLocation, subKey)}");
            returnRegKey.SetValue(fieldName, value);
        }
        #endregion

        #region GetSubKeyPath
        /// <summary>
        /// Gets the sub key path.
        /// </summary>
        /// <param name="registryLocation">The registry location.</param>
        /// <param name="subKey">The sub key.</param>
        public static string GetSubKeyPath(RegistryLocation registryLocation, string subKey)
        {
            string regSubKey = subKey;
            if (registryLocation == RegistryLocation.CurrentUserCompany || registryLocation == RegistryLocation.LocalMachineCompany) regSubKey = _companyKey + "\\" + subKey;
            if (registryLocation == RegistryLocation.CurrentUserCompanyApp || registryLocation == RegistryLocation.LocalMachineCompanyApp) regSubKey = _companyAppKey + "\\" + subKey;
            return regSubKey;
        }
        #endregion
    }
    #endregion
}
