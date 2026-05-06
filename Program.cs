using System;
using System.Windows.Forms;

namespace ShootingGame
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new GameForm());
        }
    }
}
