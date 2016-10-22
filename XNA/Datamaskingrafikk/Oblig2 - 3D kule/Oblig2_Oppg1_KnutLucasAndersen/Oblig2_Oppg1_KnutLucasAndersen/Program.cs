using System;

namespace Oblig2_Oppg1_KnutLucasAndersen {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Oblig2_Oppg1 game = new Oblig2_Oppg1())
            {
                game.Run();
            }
        }
    }
#endif
}

