using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpMessenger
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var port = Configuration.IPConnection().Port;

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // not work - show in form
            //using (NotifyIcon notifyIcon = new NotifyIcon())
            //{
            //    notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            //    //icon.ContextMenu = new ContextMenu(new MenuItem[] {
            //    //    new MenuItem("Show form", (s, e) => {new Form1().Show();}),
            //    //    new MenuItem("Exit", (s, e) => { Application.Exit(); }),
            //    //});
            //    notifyIcon.Visible = true;

            //    Application.Run(new FrmMessenger());
            //    notifyIcon.Visible = false;
            //}


            Application.Run(new FrmMessenger());
        }
    }
}
