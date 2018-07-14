using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FXPlayer {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Cognito2Desk.Cognito2Desk desk = new Cognito2Desk.Cognito2Desk("localhost");

            desk.AttributeFade(1, 50);
            desk.AttributeFade(2, 25, 24.5);
            desk.AttributeFade(24, desk.RED_CYC, 100, 20);
            desk.AttributeFade(24, desk.BLUE_CYC, 0);
            desk.AttributeFade(24, desk.GREEN_CYC, 0);
            desk.AttributeFade(24, 100, 30);

            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
            */
        }
    }
}
