
namespace HttpMessager
{
    partial class FrmMessager
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMessager));
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            tsslText = new System.Windows.Forms.ToolStripStatusLabel();
            tabControl = new System.Windows.Forms.TabControl();
            tpMessage = new System.Windows.Forms.TabPage();
            chbAddToAutostart = new System.Windows.Forms.CheckBox();
            btnIncomeMessageTest = new System.Windows.Forms.Button();
            btnSoundTest = new System.Windows.Forms.Button();
            btnSend = new System.Windows.Forms.Button();
            txtAdresses = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            txtMessage = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            tpLog = new System.Windows.Forms.TabPage();
            btnStopListener = new System.Windows.Forms.Button();
            btnStartListener = new System.Windows.Forms.Button();
            btnClear = new System.Windows.Forms.Button();
            libStatus = new System.Windows.Forms.ListBox();
            notifyIcon1 = new System.Windows.Forms.NotifyIcon(components);
            statusStrip1.SuspendLayout();
            tabControl.SuspendLayout();
            tpMessage.SuspendLayout();
            tpLog.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { tsslText });
            statusStrip1.Location = new System.Drawing.Point(0, 411);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(595, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // tsslText
            // 
            tsslText.Name = "tsslText";
            tsslText.Size = new System.Drawing.Size(0, 17);
            // 
            // tabControl
            // 
            tabControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabControl.Controls.Add(tpMessage);
            tabControl.Controls.Add(tpLog);
            tabControl.Location = new System.Drawing.Point(0, 28);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(595, 380);
            tabControl.TabIndex = 2;
            // 
            // tpMessage
            // 
            tpMessage.Controls.Add(chbAddToAutostart);
            tpMessage.Controls.Add(btnIncomeMessageTest);
            tpMessage.Controls.Add(btnSoundTest);
            tpMessage.Controls.Add(btnSend);
            tpMessage.Controls.Add(txtAdresses);
            tpMessage.Controls.Add(label2);
            tpMessage.Controls.Add(txtMessage);
            tpMessage.Controls.Add(label1);
            tpMessage.Location = new System.Drawing.Point(4, 24);
            tpMessage.Name = "tpMessage";
            tpMessage.Padding = new System.Windows.Forms.Padding(3);
            tpMessage.Size = new System.Drawing.Size(587, 352);
            tpMessage.TabIndex = 0;
            tpMessage.Text = "Message";
            tpMessage.UseVisualStyleBackColor = true;
            // 
            // chbAddToAutostart
            // 
            chbAddToAutostart.AutoSize = true;
            chbAddToAutostart.Location = new System.Drawing.Point(69, 262);
            chbAddToAutostart.Name = "chbAddToAutostart";
            chbAddToAutostart.Size = new System.Drawing.Size(167, 19);
            chbAddToAutostart.TabIndex = 7;
            chbAddToAutostart.Text = "Runs when Windows starts";
            chbAddToAutostart.UseVisualStyleBackColor = true;
            chbAddToAutostart.CheckedChanged += chbAddToAutostart_CheckedChanged;
            // 
            // btnIncomeMessageTest
            // 
            btnIncomeMessageTest.Location = new System.Drawing.Point(361, 218);
            btnIncomeMessageTest.Name = "btnIncomeMessageTest";
            btnIncomeMessageTest.Size = new System.Drawing.Size(129, 24);
            btnIncomeMessageTest.TabIndex = 6;
            btnIncomeMessageTest.Text = "Income Message Test";
            btnIncomeMessageTest.UseVisualStyleBackColor = true;
            btnIncomeMessageTest.Click += btnIncomeMessageTest_Click;
            // 
            // btnSoundTest
            // 
            btnSoundTest.Location = new System.Drawing.Point(496, 218);
            btnSoundTest.Name = "btnSoundTest";
            btnSoundTest.Size = new System.Drawing.Size(83, 24);
            btnSoundTest.TabIndex = 5;
            btnSoundTest.Text = "Sound Test";
            btnSoundTest.UseVisualStyleBackColor = true;
            btnSoundTest.Click += btnSoundTest_Click;
            // 
            // btnSend
            // 
            btnSend.Location = new System.Drawing.Point(68, 218);
            btnSend.Name = "btnSend";
            btnSend.Size = new System.Drawing.Size(83, 24);
            btnSend.TabIndex = 4;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // txtAdresses
            // 
            txtAdresses.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtAdresses.Location = new System.Drawing.Point(68, 168);
            txtAdresses.Name = "txtAdresses";
            txtAdresses.Size = new System.Drawing.Size(511, 23);
            txtAdresses.TabIndex = 3;
            txtAdresses.Text = "localhost";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(8, 171);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(56, 15);
            label2.TabIndex = 2;
            label2.Text = "Adresses:";
            // 
            // txtMessage
            // 
            txtMessage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtMessage.Location = new System.Drawing.Point(68, 14);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new System.Drawing.Size(513, 133);
            txtMessage.TabIndex = 1;
            txtMessage.Text = "Message from HttpMessager, check it out!";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 17);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(56, 15);
            label1.TabIndex = 0;
            label1.Text = "Message:";
            // 
            // tpLog
            // 
            tpLog.Controls.Add(btnStopListener);
            tpLog.Controls.Add(btnStartListener);
            tpLog.Controls.Add(btnClear);
            tpLog.Controls.Add(libStatus);
            tpLog.Location = new System.Drawing.Point(4, 24);
            tpLog.Name = "tpLog";
            tpLog.Padding = new System.Windows.Forms.Padding(3);
            tpLog.Size = new System.Drawing.Size(587, 352);
            tpLog.TabIndex = 1;
            tpLog.Text = "Logging";
            tpLog.UseVisualStyleBackColor = true;
            // 
            // btnStopListener
            // 
            btnStopListener.Location = new System.Drawing.Point(169, 221);
            btnStopListener.Name = "btnStopListener";
            btnStopListener.Size = new System.Drawing.Size(110, 23);
            btnStopListener.TabIndex = 7;
            btnStopListener.Text = "Stop Listener";
            btnStopListener.UseVisualStyleBackColor = true;
            btnStopListener.Click += btnStopListener_Click;
            // 
            // btnStartListener
            // 
            btnStartListener.Location = new System.Drawing.Point(14, 221);
            btnStartListener.Name = "btnStartListener";
            btnStartListener.Size = new System.Drawing.Size(115, 23);
            btnStartListener.TabIndex = 6;
            btnStartListener.Text = "Start Listener";
            btnStartListener.UseVisualStyleBackColor = true;
            btnStartListener.Click += btnStartListener_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new System.Drawing.Point(261, 316);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(78, 23);
            btnClear.TabIndex = 1;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // libStatus
            // 
            libStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            libStatus.FormattingEnabled = true;
            libStatus.ItemHeight = 15;
            libStatus.Location = new System.Drawing.Point(8, 6);
            libStatus.Name = "libStatus";
            libStatus.Size = new System.Drawing.Size(573, 199);
            libStatus.TabIndex = 0;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "HttpMessenger";
            notifyIcon1.Visible = true;
            notifyIcon1.DoubleClick += notifyIcon1_DoubleClick;
            // 
            // FrmMessager
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(595, 433);
            Controls.Add(tabControl);
            Controls.Add(statusStrip1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "FrmMessager";
            Text = "Http Messenger";
            FormClosing += FrmMessager_FormClosing;
            Load += FrmMessager_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tabControl.ResumeLayout(false);
            tpMessage.ResumeLayout(false);
            tpMessage.PerformLayout();
            tpLog.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslText;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpMessage;
        private System.Windows.Forms.TextBox txtAdresses;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpLog;
        private System.Windows.Forms.ListBox libStatus;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnStopListener;
        private System.Windows.Forms.Button btnStartListener;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnSoundTest;
        private System.Windows.Forms.Button btnIncomeMessageTest;
        private System.Windows.Forms.CheckBox chbAddToAutostart;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

