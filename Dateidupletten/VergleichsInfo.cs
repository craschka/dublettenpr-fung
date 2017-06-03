namespace Dateidupletten
{
    using System.IO;

    public class VergleichsInfo
    {
        public VergleichsInfo(string pfad)
        {
            this.Pfad = pfad;
            var fileInfo = new FileInfo(pfad);
            Größe = fileInfo.Length;
            NameUndGröße = fileInfo.Name + Größe;
        }

        public long Größe { get; }

        public string NameUndGröße { get; }

        public string Pfad { get; }
    }
}