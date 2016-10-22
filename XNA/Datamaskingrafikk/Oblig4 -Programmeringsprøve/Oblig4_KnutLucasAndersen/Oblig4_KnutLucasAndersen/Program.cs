using System;

namespace Oblig4_KnutLucasAndersen {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Oblig4_KnutLucasAndersen game = new Oblig4_KnutLucasAndersen())
            {
                game.Run();
            }
        }
    }
#endif
}

