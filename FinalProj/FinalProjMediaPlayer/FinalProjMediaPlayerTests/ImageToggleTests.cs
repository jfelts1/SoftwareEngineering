using System.Windows.Controls;
using System.Windows.Media.Imaging;
using FinalProjMediaPlayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinalProjMediaPlayerTests
{
    [TestClass()]
    public class ImageToggleTests
    {
        [TestMethod]
        public void ImageToggleTest()
        {
            Image i = new Image {Source = null};
            ImageToggle test = new ImageToggle(new BitmapImage(), ref i);
            Assert.IsNull(i.Source);

            test.toggle();
            Assert.IsTrue(test.Toggled);
            Assert.IsTrue(i.Source is BitmapImage);

            test.toggle();
            Assert.IsFalse(test.Toggled);
            Assert.IsNull(i.Source);

            test.toggle();
            Assert.IsTrue(test.Toggled);
            Assert.IsTrue(i.Source is BitmapImage);
        }

        [TestMethod]
        public void ForceOffTest()
        {
            Image i = new Image { Source = null };
            ImageToggle test = new ImageToggle(new BitmapImage(), ref i);
            Assert.IsNull(i.Source);

            test.forceOff();
            Assert.IsNull(i.Source);
            Assert.IsFalse(test.Toggled);
        }

        [TestMethod]
        public void ForceOnTest()
        {
            Image i = new Image { Source = null };
            ImageToggle test = new ImageToggle(new BitmapImage(), ref i);
            Assert.IsNull(i.Source);

            test.forceOn();
            Assert.IsTrue(test.Toggled);
            Assert.IsTrue(i.Source is BitmapImage);
        }
    }
}