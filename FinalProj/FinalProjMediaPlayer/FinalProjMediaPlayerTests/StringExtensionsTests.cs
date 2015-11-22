using FinalProjMediaPlayer.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinalProjMediaPlayerTests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void removeNullTerminaterTest()
        {
            string testData = "\0\0\0\0\0";
            string expected = "";

            Assert.AreEqual(expected, testData.removeNullTerminater());

            testData = "C:\\\\test asdfas\\dirs\\asdfasdf.mp3\0\0\0\0";
            expected = "C:\\\\test asdfas\\dirs\\asdfasdf.mp3";

            Assert.AreEqual(expected, testData.removeNullTerminater());

            testData = "\0\0\0\0\0asdfasfasdf";
            expected = "";

            Assert.AreEqual(expected, testData.removeNullTerminater());
        }

        [TestMethod]
        public void removeControlCharactersTest()
        {
            string testData = "\fasdfasdf";
            string expected = "asdfasdf";

            Assert.AreEqual(expected,testData.removeControlCharacters());

            testData = "\f";
            expected = "";

            Assert.AreEqual(expected,testData.removeControlCharacters());
        }
    }
}