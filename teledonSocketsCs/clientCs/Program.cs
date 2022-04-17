using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using networking;
using services;

namespace clientCs
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ITeledonServices services = new TeledonServerProxy("127.0.0.1", 55556);
            TeledonClientController controller = new TeledonClientController(services);
            Application.Run(new LoginWindow(controller));
        }
    }
}