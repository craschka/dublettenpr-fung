// //   Copyright...:  (c)  Schleupen AG
namespace Dateidupletten
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class Dublettenprüfung : IDublettenprüfung
    {
        public IEnumerable<IDublette> Sammle_Kandidaten(string pfad)
        {
            return Sammle_Kandidaten(pfad, Vergleichsmodi.Größe_und_Name);
        }

        public IEnumerable<IDublette> Sammle_Kandidaten(string pfad, Vergleichsmodi modus)
        {
            var files = Directory.GetFiles(pfad, "*", SearchOption.AllDirectories);

            var fileinfos = files.Select(x => new VergleichsInfo(x));

            return fileinfos.GroupBy(x => FeldByModus(x, modus)).Where(x=>x.Count()>1).Select(x => new Dublette(x.Select(y => y.Pfad)));
        }

        public IEnumerable<IDublette> Prüfe_Kandidaten(IEnumerable<IDublette> kandidaten)
        {
            var md5 = MD5.Create();

            return kandidaten.Where(x => x.Dateipfade.Select(y => ComputeHash(md5, y)).Distinct().Count() == 1);
        }

        private static string FeldByModus(VergleichsInfo x, Vergleichsmodi modi)
        {
            switch (modi)
            {
                case Vergleichsmodi.Größe_und_Name:return x.NameUndGröße;
                case Vergleichsmodi.Größe:return x.Größe.ToString();
                default:
                    throw new ArgumentOutOfRangeException(nameof(modi), modi, null);
            }
        }
        
        private static string ComputeHash(MD5 md5, string file)
        {
            using (var stream = File.OpenRead(file))
            {
                return Encoding.UTF8.GetString(md5.ComputeHash(stream));
            }
        }
    }
}