using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpMessager
{
    #region Configuration
    /// <summary>
    /// Configuration
    /// </summary>
    /// <seealso cref="okTools.ConfigurationHelper.ConfigurationHelper" />
    class Configuration : okTools.ConfigurationHelper.ConfigurationHelper
    {
        #region Properties
        /// <summary>
        /// Gets the sound file.
        /// </summary>
        /// <value>
        /// The sound file.
        /// </value>
        public static string SoundFile {
            get { return GetValue<string>("SoundFile"); } 
        }
        /// <summary>
        /// Gets the automatic updater URL.
        /// </summary>
        /// <value>
        /// The automatic updater URL.
        /// </value>
        public static string AutoUpdaterUrl
        {
            get { return GetValue<string>("AutoUpdaterUrl"); }
        }

        /// <summary>
        /// Columns the table containers.
        /// </summary>
        /// <returns></returns>
        public static IPConnection IPConnection()
        {
            return InitOptions<IPConnection>("IPConnection");
        }
        #endregion
    }
    #endregion

    #region Configuration Model - IPConnection
    /// <summary>
    /// IPConnection
    /// </summary>
    public class IPConnection
    {
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int Port { get; set; }
    }
    #endregion
}
