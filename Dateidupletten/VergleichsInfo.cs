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
            Name = fileInfo.Name;
            NameUndGröße = Name + Größe;
        }

        public string Name { get; private set; }

        public long Größe { get; private set; }

        public string NameUndGröße { get; private set; }

        public string Pfad { get; private set; }
    }
}