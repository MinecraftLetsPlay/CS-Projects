using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;

namespace Hardware_Monitor
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Check if program already has administrator privileges
            WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool hasAdminRight = principal.IsInRole(WindowsBuiltInRole.Administrator);

            if (!hasAdminRight)
            {
                MessageBox.Show("This application needs administrator privileges to run!",
                              "Administrator-privileges required!",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
