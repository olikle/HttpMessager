using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpMessenger
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FrmMessenger : Form
    {
        #region variable
        /// <summary>
        /// The simple HTTP listener
        /// </summary>
        private SimpleHttpListener _simpleHttpListener;

        string soundFile = "";
        
        private FormWindowState _lastFormWindowState;

        private bool _closeFromNotifyIcon = false;
        #endregion

        #region contructor
        /// <summary>
        /// Initializes a new instance of the <see cref="FrmMessenger"/> class.
        /// </summary>
        public FrmMessenger()
        {
            InitializeComponent();
            soundFile = Configuration.SoundFile;
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

            chbAddToAutostart.Checked = Functions.ExistsInAutostart();

            //_simpleHttpListener = new SimpleHttpListener(8888);
            //_simpleHttpListener = new SimpleHttpListener(8355);
            //_simpleHttpListener = new SimpleHttpListener(8080);
            _simpleHttpListener = new SimpleHttpListener(30120);
            _simpleHttpListener.RecieveStatus += SimpleHttpListener_RecieveStatus;
            _simpleHttpListener.RecieveMessage += SimpleHttpListener_RecieveMessage;
            _simpleHttpListener.StartWebServer();
            
            SetNotifyIcon();
            
            _lastFormWindowState = this.WindowState;
        }
        private void FrmMessenger_FormClosing(object sender, FormClosingEventArgs e)
        {
            // close if Application.Exit();
            if (_closeFromNotifyIcon) return;
            this.Hide();
            MessageBox.Show("The HttpMessenger only hides but runs in background.\n\nTo end the programm use the systray icon context menu.", "Hide", MessageBoxButtons.OK, MessageBoxIcon.Information);
            e.Cancel = true;
        }
        #endregion

        #region Notify Icon
        // https://stackoverflow.com/questions/995195/how-can-i-make-a-net-windows-forms-application-that-only-runs-in-the-system-tra
        // https://stackoverflow.com/questions/1617784/how-to-start-the-application-directly-in-system-tray-net-c
        /// <summary>
        /// Setnotifies the icon.
        /// </summary>
        void SetNotifyIcon()
        {
            //notifyIcon1.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            notifyIcon1.ContextMenuStrip.Items.Add("Show", null, this.ContextMenuShow_Click);
            notifyIcon1.ContextMenuStrip.Items.Add("Hide", null, this.ContextMenuHide_Click);
            notifyIcon1.ContextMenuStrip.Items.Add("Exit Application", null, this.ContextMenuExit_Click);
            //notifyIcon1.ContextMenuStrip.Items.Add("Exit Application", null, (s, e) => { Application.Exit(); });
            notifyIcon1.Visible = true;
        }
        /// <summary>
        /// Handles the DoubleClick event of the notifyIcon1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            _lastFormWindowState = FormWindowState.Normal;
            this.WindowState = FormWindowState.Normal;
            this.Focus();
        }
        /// <summary>
        /// Handles the Click event of the ContextMenuShow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void ContextMenuShow_Click(object sender, EventArgs e)
        {
            this.Show();
            _lastFormWindowState = FormWindowState.Normal;
            this.WindowState = FormWindowState.Normal;
            this.Focus();
        }
        /// <summary>
        /// Handles the Click event of the ContextMenuHide control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void ContextMenuHide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        /// <summary>
        /// Handles the Click event of the ContextMenuExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void ContextMenuExit_Click(object sender, EventArgs e)
        {
            _closeFromNotifyIcon = true;
            Application.Exit();
        }
        #endregion

        #region WindowsState
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.ClientSizeChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnClientSizeChanged(EventArgs e)
        {
            if (this.WindowState != _lastFormWindowState)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.Hide();
                }
                _lastFormWindowState = this.WindowState;
            }
            base.OnClientSizeChanged(e);
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
            AddStatus(e.Message);
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

            if (!string.IsNullOrEmpty(soundFile))
            {
                Sound.LoadAndPlaySoundFile(soundFile);
            }
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
            libStatus.Items.Insert(0, DateTime.Now.ToString("dd:MM:yyyy") + "-" + DateTime.Now.ToString("HH:mm:ss") + ", " + status);
        }
        #endregion

        #region btnIncomeMessageTest_Click, btnSoundTest_Click
        /// <summary>
        /// Handles the Click event of the btnIncomeMessageTest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnIncomeMessageTest_Click(object sender, EventArgs e)
        {
            ShowMessage("Local machine", "Income message output test...");
        }
        /// <summary>
        /// Handles the Click event of the btnSoundTest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSoundTest_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(soundFile))
            {
                if (File.Exists(soundFile))
                {
                    Sound.LoadAndPlaySoundFile(soundFile);
                    MessageBox.Show($"Sound file '{soundFile}' played.");
                }
                else
                {
                    MessageBox.Show($"File not found '{soundFile}'");
                }
                //var play = PlaySound();
                //MessageBox.Show($"Sound play = {play.Result}");
            }
            else
            {
                MessageBox.Show("No Sound file defined");
            }
        }
        #endregion

        #region chbAddToAutostart_CheckedChanged
        /// <summary>
        /// Handles the CheckedChanged event of the chbAddToAutostart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chbAddToAutostart_CheckedChanged(object sender, EventArgs e)
        {
            if (chbAddToAutostart.Checked)
                Functions.AddToAutostart();
            else
                Functions.RemoveAutostart();
        }
        #endregion

    }
}
