
namespace HttpMessenger
{
    partial class FrmMessenger
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslText = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpMessage = new System.Windows.Forms.TabPage();
            this.btnIncomeMessageTest = new System.Windows.Forms.Button();
            this.btnSoundTest = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtAdresses = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpLog = new System.Windows.Forms.TabPage();
            this.btnStopListener = new System.Windows.Forms.Button();
            this.btnStartListener = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.libStatus = new System.Windows.Forms.ListBox();
            this.chbAddToAutostart = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpMessage.SuspendLayout();
            this.tpLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(595, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 411);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(595, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslText
            // 
            this.tsslText.Name = "tsslText";
            this.tsslText.Size = new System.Drawing.Size(0, 17);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tpMessage);
            this.tabControl.Controls.Add(this.tpLog);
            this.tabControl.Location = new System.Drawing.Point(0, 28);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(595, 380);
            this.tabControl.TabIndex = 2;
            // 
            // tpMessage
            // 
            this.tpMessage.Controls.Add(this.chbAddToAutostart);
            this.tpMessage.Controls.Add(this.btnIncomeMessageTest);
            this.tpMessage.Controls.Add(this.btnSoundTest);
            this.tpMessage.Controls.Add(this.btnSend);
            this.tpMessage.Controls.Add(this.txtAdresses);
            this.tpMessage.Controls.Add(this.label2);
            this.tpMessage.Controls.Add(this.txtMessage);
            this.tpMessage.Controls.Add(this.label1);
            this.tpMessage.Location = new System.Drawing.Point(4, 24);
            this.tpMessage.Name = "tpMessage";
            this.tpMessage.Padding = new System.Windows.Forms.Padding(3);
            this.tpMessage.Size = new System.Drawing.Size(587, 352);
            this.tpMessage.TabIndex = 0;
            this.tpMessage.Text = "Message";
            this.tpMessage.UseVisualStyleBackColor = true;
            // 
            // btnIncomeMessageTest
            // 
            this.btnIncomeMessageTest.Location = new System.Drawing.Point(361, 218);
            this.btnIncomeMessageTest.Name = "btnIncomeMessageTest";
            this.btnIncomeMessageTest.Size = new System.Drawing.Size(129, 24);
            this.btnIncomeMessageTest.TabIndex = 6;
            this.btnIncomeMessageTest.Text = "Income Message Test";
            this.btnIncomeMessageTest.UseVisualStyleBackColor = true;
            this.btnIncomeMessageTest.Click += new System.EventHandler(this.btnIncomeMessageTest_Click);
            // 
            // btnSoundTest
            // 
            this.btnSoundTest.Location = new System.Drawing.Point(496, 218);
            this.btnSoundTest.Name = "btnSoundTest";
            this.btnSoundTest.Size = new System.Drawing.Size(83, 24);
            this.btnSoundTest.TabIndex = 5;
            this.btnSoundTest.Text = "Sound Test";
            this.btnSoundTest.UseVisualStyleBackColor = true;
            this.btnSoundTest.Click += new System.EventHandler(this.btnSoundTest_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(68, 218);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(83, 24);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtAdresses
            // 
            this.txtAdresses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdresses.Location = new System.Drawing.Point(68, 168);
            this.txtAdresses.Name = "txtAdresses";
            this.txtAdresses.Size = new System.Drawing.Size(511, 23);
            this.txtAdresses.TabIndex = 3;
            this.txtAdresses.Text = "localhost";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Adresses:";
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.Location = new System.Drawing.Point(68, 14);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(513, 133);
            this.txtMessage.TabIndex = 1;
            this.txtMessage.Text = "Message from HttpMessenger, check it out!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Message:";
            // 
            // tpLog
            // 
            this.tpLog.Controls.Add(this.btnStopListener);
            this.tpLog.Controls.Add(this.btnStartListener);
            this.tpLog.Controls.Add(this.btnClear);
            this.tpLog.Controls.Add(this.libStatus);
            this.tpLog.Location = new System.Drawing.Point(4, 24);
            this.tpLog.Name = "tpLog";
            this.tpLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpLog.Size = new System.Drawing.Size(587, 352);
            this.tpLog.TabIndex = 1;
            this.tpLog.Text = "Logging";
            this.tpLog.UseVisualStyleBackColor = true;
            // 
            // btnStopListener
            // 
            this.btnStopListener.Location = new System.Drawing.Point(169, 221);
            this.btnStopListener.Name = "btnStopListener";
            this.btnStopListener.Size = new System.Drawing.Size(110, 23);
            this.btnStopListener.TabIndex = 7;
            this.btnStopListener.Text = "Stop Listener";
            this.btnStopListener.UseVisualStyleBackColor = true;
            this.btnStopListener.Click += new System.EventHandler(this.btnStopListener_Click);
            // 
            // btnStartListener
            // 
            this.btnStartListener.Location = new System.Drawing.Point(14, 221);
            this.btnStartListener.Name = "btnStartListener";
            this.btnStartListener.Size = new System.Drawing.Size(115, 23);
            this.btnStartListener.TabIndex = 6;
            this.btnStartListener.Text = "Start Listener";
            this.btnStartListener.UseVisualStyleBackColor = true;
            this.btnStartListener.Click += new System.EventHandler(this.btnStartListener_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(261, 316);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(78, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // libStatus
            // 
            this.libStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.libStatus.FormattingEnabled = true;
            this.libStatus.ItemHeight = 15;
            this.libStatus.Location = new System.Drawing.Point(8, 6);
            this.libStatus.Name = "libStatus";
            this.libStatus.Size = new System.Drawing.Size(573, 199);
            this.libStatus.TabIndex = 0;
            // 
            // chbAddToAutostart
            // 
            this.chbAddToAutostart.AutoSize = true;
            this.chbAddToAutostart.Location = new System.Drawing.Point(69, 262);
            this.chbAddToAutostart.Name = "chbAddToAutostart";
            this.chbAddToAutostart.Size = new System.Drawing.Size(167, 19);
            this.chbAddToAutostart.TabIndex = 7;
            this.chbAddToAutostart.Text = "Runs when Windows starts";
            this.chbAddToAutostart.UseVisualStyleBackColor = true;
            this.chbAddToAutostart.CheckedChanged += new System.EventHandler(this.chbAddToAutostart_CheckedChanged);
            // 
            // FrmMessenger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 433);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmMessenger";
            this.Text = "Http Messenger";
            this.Load += new System.EventHandler(this.FrmMessager_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tpMessage.ResumeLayout(false);
            this.tpMessage.PerformLayout();
            this.tpLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
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
    }
}

