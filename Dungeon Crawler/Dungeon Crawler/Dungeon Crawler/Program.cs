using System;

namespace Dungeon_Crawler
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (DCGame game = new DCGame())
            {
                game.Run();
            }
        }
    }
#endif
}

