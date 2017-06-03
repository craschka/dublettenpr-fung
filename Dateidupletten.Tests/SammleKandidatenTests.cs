// //   Copyright...:  (c)  Schleupen AG
namespace Dateidupletten.Tests
{
    using System.IO;
    using System.Linq;
    using NUnit.Framework;

    public class SammleKandidatenTests
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
        public void SammleKandidatenMitGröße_ShouldReturnDuplette_WennZweiDateienGleichGrossSind()
        {
            var sut = new Dublettenprüfung();

            File.AppendAllText("test/test1/file1.txt","text ist egal1");
            File.AppendAllText("test/test2/file2.txt","text ist egal2");

            var result = sut.Sammle_Kandidaten("test", Vergleichsmodi.Größe).ToList();

            Assert.That(result.Single().Dateipfade.First(),Is.EqualTo("test\\test1\\file1.txt"));
            Assert.That(result.Single().Dateipfade.Skip(1).Single(),Is.EqualTo("test\\test2\\file2.txt"));
        }

        [Test]
        public void SammleKandidatenMitNamenUndGröße_ShouldReturnDuplette_WennZweiDateienGleichGrossSind()
        {
            var sut = new Dublettenprüfung();

            File.AppendAllText("test/test1/file1.txt", "text ist egal1");
            File.AppendAllText("test/test2/file1.txt", "text ist egal2");

            var result = sut.Sammle_Kandidaten("test").ToList();

            Assert.That(result.Single().Dateipfade.First(), Is.EqualTo("test\\test1\\file1.txt"));
            Assert.That(result.Single().Dateipfade.Skip(1).Single(), Is.EqualTo("test\\test2\\file1.txt"));
        }

        [Test]
        public void SammleKandidatenMitGröße_ShouldReturnDuplette_WennMehrereDateienGleichGrossSind()
        {
            var sut = new Dublettenprüfung();

            File.AppendAllText("test/test1/file1.txt", "text ist egal1");
            File.AppendAllText("test/test2/file2.txt", "text ist egal2");
            File.AppendAllText("test/test1/file3.txt", "andere Größe1");
            File.AppendAllText("test/test2/file4.txt", "andere Größe2");
            File.AppendAllText("test/test2/file5.txt", "andere Größe3");

            var result = sut.Sammle_Kandidaten("test", Vergleichsmodi.Größe).ToList();

            Assert.That(result.First().Dateipfade.First(), Is.EqualTo("test\\test1\\file1.txt"));
            Assert.That(result.First().Dateipfade.Skip(1).Single(), Is.EqualTo("test\\test2\\file2.txt"));
            Assert.That(result.Skip(1).Single().Dateipfade.First(), Is.EqualTo("test\\test1\\file3.txt"));
            Assert.That(result.Skip(1).Single().Dateipfade.Skip(1).First(), Is.EqualTo("test\\test2\\file4.txt"));
            Assert.That(result.Skip(1).Single().Dateipfade.Skip(2).Single(), Is.EqualTo("test\\test2\\file5.txt"));
        }

        [Test]
        public void SammleKandidatenMitGröße_ShouldNotReturnDuplette_WennZweiDateienUngleichGrossSind()
        {
            var sut = new Dublettenprüfung();

            File.AppendAllText("test/test1/file1.txt", "das ist ein großer Dateiinhalt");
            File.AppendAllText("test/test2/file2.txt", "datei ist kleiner");

            var result = sut.Sammle_Kandidaten("test", Vergleichsmodi.Größe).ToList();

            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}