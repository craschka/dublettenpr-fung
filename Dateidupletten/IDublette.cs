namespace Dateidupletten
{
    using System.Collections.Generic;

    public interface IDublette
    {
        IEnumerable<string> Dateipfade { get; }
    }
}