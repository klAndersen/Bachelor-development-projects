using System;

namespace Oblig6_Oppg2_KnutLucasAndersen {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Oppgave2 game = new Oppgave2())
            {
                game.Run();
            }
        }
    }
#endif
}

