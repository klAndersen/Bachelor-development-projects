using System;

namespace Oblig1A_KnutLucasAndersen {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Oblig1A game = new Oblig1A())
            {
                game.Run();
            }
        }
    }
#endif
}

