using System;

namespace Oblig6_Oppg1_KnutLucasAndersen {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Oppgave1 game = new Oppgave1())
            {
                game.Run();
            }
        }
    }
#endif
}

