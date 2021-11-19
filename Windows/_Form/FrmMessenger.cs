using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpMessenger
{
    public partial class FrmMessenger : Form
    {
        #region variable
        /// <summary>
        /// The simple HTTP listener
        /// </summary>
        private SimpleHttpListener _simpleHttpListener;
        #endregion

        #region contructor
        /// <summary>
        /// Initializes a new instance of the <see cref="FrmMessenger"/> class.
        /// </summary>
        public FrmMessenger()
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
            //http://localhost:8888/message?text=Hallo
            //tabControl.SelectedTab = tpLog;
            btnStartListener.Enabled = false;
            btnStopListener.Enabled = true;

            //_simpleHttpListener = new SimpleHttpListener(8888);
            //_simpleHttpListener = new SimpleHttpListener(8355);
            //_simpleHttpListener = new SimpleHttpListener(8080);
            _simpleHttpListener = new SimpleHttpListener(30120);
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
        /// <param name="e">The <see cref="HttpMessengerEventArgs" /> instance containing the event data.</param>
        private void SimpleHttpListener_RecieveMessage(object sender, HttpMessengerEventArgs e)
        {
            ShowMessage("Incoming Message", e.Message);
        }

        /// <summary>
        /// Handles the RecieveStatus event of the _simpleHttpListener control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="HttpMessengerEventArgs"/> instance containing the event data.</param>
        private void SimpleHttpListener_RecieveStatus(object sender, HttpMessengerEventArgs e)
        {
            AddStatus(DateTime.Now.ToString("HH:mm:ss") + ", " + e.Message);
        }
        #endregion

        #region Message
        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowMessage(string title, string message)
        {
            AddStatus($"Show Message: {title}, {message}");
            Communication.ToastNotification(title, message, 0);
            FrmMessage frmMessage = new FrmMessage();
            frmMessage.Text = title;
            frmMessage.message = message;
            frmMessage.Show();
            frmMessage.Focus();
        }
        #endregion

        #region btnSend_Click
        /// <summary>
        /// Handles the Click event of the btnSend control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnSend_Click(object sender, EventArgs e)
        {
            JsonRPCRequest jsonRPCRequest = new JsonRPCRequest("SendMessage");
            jsonRPCRequest.Params.from = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
            jsonRPCRequest.Params.message = txtMessage.Text;

            var addresses = txtAdresses.Text.Split(",");
            foreach (var address in addresses)
            {
                await Communication.SendMessageAsync($"http://{address}:30120/jsonrpc", jsonRPCRequest.ToString());
            }
            //
            // do not call on localhost
            //var result = Communication.SendMessageAsync("http://localhost:8888/jsonrpc", jsonRPCRequest.ToString()).GetAwaiter().GetResult();
            //AddStatus($"Result: {result}");
        }
        #endregion
        #region AddStatus
        /// <summary>
        /// Adds the status.
        /// </summary>
        /// <param name="status">The status.</param>
        public void AddStatus(string status)
        {
            libStatus.Items.Insert(0, DateTime.Now.ToString("HH:mm:ss") + ", " + status);
        }
        #endregion
    }
}
