using System;

namespace Oblig3_KnutLucasAndersen {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Planetsystem game = new Planetsystem())
            {
                game.Run();
            }
        }
    }
#endif
}

