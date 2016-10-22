using System;

namespace Oblig5_KnutLucasAndersen {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Oblig5_Terreng game = new Oblig5_Terreng())
            {
                game.Run();
            }
        }
    }
#endif
}

