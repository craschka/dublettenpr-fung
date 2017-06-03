// //   Copyright...:  (c)  Schleupen AG
namespace Dateidupletten.Tests
{
    using System.IO;
    using System.Linq;
    using NUnit.Framework;

    public class PrüfeKandidatenTests
    {
        [SetUp]
        public void Setup()
        {
            Directory.CreateDirectory("test");
            Directory.CreateDirectory("test/test1");
            Directory.CreateDirectory("test/test2");
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete("test", true);
        }

        [Test]
        public void PrüfeKandidaten_ShouldReturnDubletten_WennMD5HashGleichIst()
        {
            var sut = new Dublettenprüfung();

            File.AppendAllText("test/test1/file1.txt", "text ist wichtig");
            File.AppendAllText("test/test2/file2.txt", "text ist wichtig");

            var kandidaten = sut.Sammle_Kandidaten("test", Vergleichsmodi.Größe).ToList();

            var result = sut.Prüfe_Kandidaten(kandidaten).ToList();

            Assert.That(result.Single().Dateipfade.First(), Is.EqualTo("test\\test1\\file1.txt"));
            Assert.That(result.Single().Dateipfade.Skip(1).Single(), Is.EqualTo("test\\test2\\file2.txt"));
        }

        [Test]
        public void PrüfeKandidaten_ShouldNotReturnDubletten_WennMD5HashNichtGleichIst()
        {
            var sut = new Dublettenprüfung();

            File.AppendAllText("test/test1/file1.txt", "text ist wichtig");
            File.AppendAllText("test/test2/file2.txt", "text ist anders.");

            var kandidaten = sut.Sammle_Kandidaten("test", Vergleichsmodi.Größe).ToList();

            var result = sut.Prüfe_Kandidaten(kandidaten).ToList();

            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}