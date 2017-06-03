// //   Copyright...:  (c)  Schleupen AG
namespace Dateidupletten
{
    using System.Collections.Generic;

    public class Dublette : IDublette
    {
        public Dublette(IEnumerable<string> dateipfade)
        {
            Dateipfade = dateipfade;
        }

        public IEnumerable<string> Dateipfade { get; }
    }
}