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
    public partial class FrmMessage : Form
    {
        #region variable
        /// <summary>
        /// The message
        /// </summary>
        public string message = "";
        #endregion

        #region form
        /// <summary>
        /// Initializes a new instance of the <see cref="FrmMessage"/> class.
        /// </summary>
        public FrmMessage()
        {
            InitializeComponent();
            lblMessage.Text = "";
        }
        /// <summary>
        /// Handles the Shown event of the FrmMessage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FrmMessage_Shown(object sender, EventArgs e)
        {
            lblMessage.Text = message;
        }
        #endregion

        #region UI
        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
