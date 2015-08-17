using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GuiGame {

    /// <summary>
    /// The starting point for the GUI game.
    /// </summary>
    static class GuiMain {

        /// <summary>
        /// The Main method for the GUI game.
        /// Pre:  none
        /// Post: the user has finished playing the game.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HareAndTortoiseForm());
        }
    }
}
