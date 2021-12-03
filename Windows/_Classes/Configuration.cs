using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpMessenger
{
    #region Configuration
    /// <summary>
    /// Configuration
    /// </summary>
    /// <seealso cref="okTools.ConfigurationHelper.ConfigurationHelper" />
    class Configuration : okTools.ConfigurationHelper.ConfigurationHelper
    {
        #region Properties
        public static string SoundFile {
            get { return GetValue<string>("SoundFile"); } 
        }
        #endregion

        /// <summary>
        /// Columns the table containers.
        /// </summary>
        /// <returns></returns>
        public static IPConnection IPConnection()
        {
            return InitOptions<IPConnection>("IPConnection");
        }
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
