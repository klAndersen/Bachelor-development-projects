using System;

namespace Prosjekt1_KnutLucasAndersen {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (StartDemo game = new StartDemo())
            {
                game.Run();
            }
        }
    }
#endif
}

