using System;

namespace Oblig2_Oppg2_KnutLucasAndersen {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Oblig2_Oppg2 game = new Oblig2_Oppg2())
            {
                game.Run();
            }
        }
    }
#endif
}

