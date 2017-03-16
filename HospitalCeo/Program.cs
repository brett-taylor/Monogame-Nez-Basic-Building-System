using System;

/*
 * Brett Taylor
 * Main class
 */

namespace HospitalCeo
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (HospitalCeo game = new HospitalCeo())
            {
                game.Run();
            }
        }
    }
}
