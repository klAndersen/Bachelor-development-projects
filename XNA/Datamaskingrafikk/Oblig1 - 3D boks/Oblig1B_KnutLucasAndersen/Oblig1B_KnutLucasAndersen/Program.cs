using System;

namespace Oblig1B_KnutLucasAndersen {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Oblig1B game = new Oblig1B())
            {
                game.Run();
            }
        }
    }
#endif
}

