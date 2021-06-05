using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpMessager
{
    public partial class FrmMessager : Form
    {
        #region variable
        /// <summary>
        /// The simple HTTP listener
        /// </summary>
        private SimpleHttpListener _simpleHttpListener;
        #endregion

        #region contructor
        /// <summary>
        /// Initializes a new instance of the <see cref="FrmMessager"/> class.
        /// </summary>
        public FrmMessager()
        {
            InitializeComponent();
        }
        #endregion

        #region form
        /// <summary>
        /// Handles the Load event of the FrmMessager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FrmMessager_Load(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tpLog;
            btnStartListener.Enabled = false;
            btnStopListener.Enabled = true;

            _simpleHttpListener = new SimpleHttpListener(this, libStatus);
            _simpleHttpListener.RecieveStatus += SimpleHttpListener_RecieveStatus;
            _simpleHttpListener.RecieveMessage += SimpleHttpListener_RecieveMessage;
            _simpleHttpListener.StartWebServer();
        }

        #endregion

        #region UI
        /// <summary>
        /// Handles the Click event of the btnClear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            libStatus.Items.Clear();
        }

        /// <summary>
        /// Handles the Click event of the btnStartListener control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnStartListener_Click(object sender, EventArgs e)
        {
            btnStartListener.Enabled = false;
            btnStopListener.Enabled = true;
            _simpleHttpListener.StartWebServer();
        }

        /// <summary>
        /// Handles the Click event of the btnStopListener control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnStopListener_Click(object sender, EventArgs e)
        {
            btnStartListener.Enabled = true;
            btnStopListener.Enabled = false;
            _simpleHttpListener.StopWebServer();
        }
        #endregion

        #region Http Events
        /// <summary>
        /// Handles the RecieveMessage event of the _simpleHttpListener control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="HttpMessagerEventArgs"/> instance containing the event data.</param>
        private void SimpleHttpListener_RecieveMessage(object sender, HttpMessagerEventArgs e)
        {
            ShowMessage("Incoming Message", e.Message);
        }

        /// <summary>
        /// Handles the RecieveStatus event of the _simpleHttpListener control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="HttpMessagerEventArgs"/> instance containing the event data.</param>
        private void SimpleHttpListener_RecieveStatus(object sender, HttpMessagerEventArgs e)
        {
            libStatus.Items.Insert(0, DateTime.Now.ToString("HH:mm:ss") + ", " + e.Message);
        }
        #endregion

        #region Message
        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowMessage(string title, string message)
        {
            libStatus.Items.Add($"Show Message: {title}, {message}");
            Communication.ToastNotification(title, message, 0);
            FrmMessage frmMessage = new FrmMessage();
            frmMessage.Text = title;
            frmMessage.message = message;
            frmMessage.Show();
            frmMessage.Focus();
        }
        #endregion

    }
}
