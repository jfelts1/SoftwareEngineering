using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using FinalProjMediaPlayer;

namespace FinalProjMediaPlayerTests
{
    [TestClass]
    public class VolumeHandlerTests
    {
        [TestMethod]
        public void VolumeHandlerTest()
        {
            MediaElement ele;
            Slider slid;
            Image img;
            VolumeHandler testData = initVolumeHandler(out ele, out slid, out img);
            Assert.IsNull(img.Source);
            Assert.AreEqual(slid.Value,Globals.MaxSliderValue);
            Assert.AreEqual(ele.Volume,Globals.MaxVolume);
            Assert.IsTrue(testData.Toggled);

            testData.toggle();
            Assert.IsTrue(img.Source is BitmapImage);
            Assert.AreEqual(slid.Value,0);
            Assert.AreEqual(ele.Volume,0);
            Assert.AreEqual(testData.LastOnVolumeValue,Globals.MaxVolume);
            Assert.AreEqual(testData.LastOnVolumeSliderValue,Globals.MaxSliderValue);
            Assert.IsFalse(testData.Toggled);

            testData.toggle();
            Assert.IsNull(img.Source);
            Assert.AreEqual(slid.Value,Globals.MaxSliderValue);
            Assert.AreEqual(ele.Volume,Globals.MaxVolume);
            Assert.AreEqual(testData.LastOnVolumeValue,Globals.MaxVolume);
            Assert.IsTrue(testData.Toggled);

            testData.toggle();
            Assert.IsTrue(img.Source is BitmapImage);
            Assert.AreEqual(slid.Value, 0);
            Assert.AreEqual(ele.Volume, 0);
            Assert.AreEqual(testData.LastOnVolumeValue, Globals.MaxVolume);
            Assert.AreEqual(testData.LastOnVolumeSliderValue, Globals.MaxSliderValue);
            Assert.IsFalse(testData.Toggled);

        }

        [TestMethod]
        public void forceOffTest()
        {
            MediaElement ele;
            Slider slid;
            Image img;
            VolumeHandler testData = initVolumeHandler(out ele, out slid, out img);
            Assert.IsNull(img.Source);
            Assert.AreEqual(slid.Value, Globals.MaxSliderValue);
            Assert.AreEqual(ele.Volume, Globals.MaxVolume);
            Assert.IsTrue(testData.Toggled);

            testData.forceOff();
            Assert.IsTrue(img.Source is BitmapImage);
            Assert.AreEqual(slid.Value, 0);
            Assert.AreEqual(ele.Volume, 0);
            Assert.AreEqual(testData.LastOnVolumeValue, Globals.MaxVolume);
            Assert.AreEqual(testData.LastOnVolumeSliderValue, Globals.MaxSliderValue);
            Assert.IsFalse(testData.Toggled);
        }

        [TestMethod]
        public void forceOnTest()
        {
            MediaElement ele;
            Slider slid;
            Image img;
            VolumeHandler testData = initVolumeHandler(out ele, out slid, out img);
            Assert.IsNull(img.Source);
            Assert.AreEqual(slid.Value, Globals.MaxSliderValue);
            Assert.AreEqual(ele.Volume, Globals.MaxVolume);
            Assert.IsTrue(testData.Toggled);

            testData.forceOn();
            Assert.IsNull(img.Source);
            Assert.AreEqual(slid.Value, Globals.MaxSliderValue);
            Assert.AreEqual(ele.Volume, Globals.MaxVolume);
            Assert.AreEqual(testData.LastOnVolumeValue, Globals.MaxVolume);
            Assert.IsTrue(testData.Toggled);

        }

        [TestMethod]
        public void convertSliderPosToVolLevelTest()
        {
            double expected = 0.5;
            double testData = 5;

            Assert.AreEqual(expected,VolumeHandler.convertSliderPosToVolLevel(testData));

            expected = 0;
            testData = 0;
            
            Assert.AreEqual(expected,VolumeHandler.convertSliderPosToVolLevel(testData));

            expected = 1;
            testData = 10;

            Assert.AreEqual(expected,VolumeHandler.convertSliderPosToVolLevel(testData));

            testData = 11;
            try
            {
                VolumeHandler.convertSliderPosToVolLevel(testData);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                
            }

            testData = -1;
            try
            {
                VolumeHandler.convertSliderPosToVolLevel(testData);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }

        }

        [TestMethod]
        public void setVolumeTest()
        {
            MediaElement ele;
            Slider slid;
            Image img;
            VolumeHandler testData = initVolumeHandler(out ele, out slid, out img);
            //on
            Assert.IsNull(img.Source);
            Assert.AreEqual(slid.Value, Globals.MaxSliderValue);
            Assert.AreEqual(ele.Volume, Globals.MaxVolume);
            Assert.IsTrue(testData.Toggled);

            //off
            testData.setVolume(new object(),
                new RoutedPropertyChangedEventArgs<double>(testData.LastOnVolumeSliderValue, 0));
            Assert.IsTrue(img.Source is BitmapImage);
            Assert.AreEqual(slid.Value, 0);
            Assert.AreEqual(ele.Volume, 0);
            Assert.AreEqual(testData.LastOnVolumeValue, Globals.MaxVolume);
            Assert.AreEqual(testData.LastOnVolumeSliderValue, Globals.MaxSliderValue);
            Assert.IsFalse(testData.Toggled);

            //on
            testData.setVolume(new object(),
                new RoutedPropertyChangedEventArgs<double>(testData.LastOnVolumeSliderValue, Globals.MaxSliderValue));
            Assert.IsNull(img.Source);
            Assert.AreEqual(slid.Value, Globals.MaxSliderValue);
            Assert.AreEqual(ele.Volume, Globals.MaxVolume);
            Assert.AreEqual(testData.LastOnVolumeValue, Globals.MaxVolume);
            Assert.IsTrue(testData.Toggled);

            //on
            double newSliderPosition = 5;
            testData.setVolume(new object(),
                new RoutedPropertyChangedEventArgs<double>(testData.LastOnVolumeSliderValue, newSliderPosition));
            Assert.IsNull(img.Source);
            Assert.AreEqual(slid.Value, newSliderPosition);
            Assert.AreEqual(ele.Volume, VolumeHandler.convertSliderPosToVolLevel(newSliderPosition));
            Assert.AreEqual(testData.LastOnVolumeValue, VolumeHandler.convertSliderPosToVolLevel(newSliderPosition));
            Assert.IsTrue(testData.Toggled);

            //off
            testData.toggle();
            Assert.IsTrue(img.Source is BitmapImage);
            Assert.AreEqual(slid.Value, 0);
            Assert.AreEqual(ele.Volume, 0);
            Assert.AreEqual(testData.LastOnVolumeValue, VolumeHandler.convertSliderPosToVolLevel(newSliderPosition));
            Assert.AreEqual(testData.LastOnVolumeSliderValue, newSliderPosition);
            Assert.IsFalse(testData.Toggled);

            //on
            testData.toggle();
            Assert.IsNull(img.Source);
            Assert.AreEqual(slid.Value, newSliderPosition);
            Assert.AreEqual(ele.Volume, VolumeHandler.convertSliderPosToVolLevel(newSliderPosition));
            Assert.AreEqual(testData.LastOnVolumeValue, VolumeHandler.convertSliderPosToVolLevel(newSliderPosition));
            Assert.IsTrue(testData.Toggled);

            //invalid
            newSliderPosition = 11;
            try
            {
                testData.setVolume(new object(),
                    new RoutedPropertyChangedEventArgs<double>(testData.LastOnVolumeSliderValue, newSliderPosition));
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                
            }

            //invalid
            newSliderPosition = -1;
            try
            {
                testData.setVolume(new object(),
                    new RoutedPropertyChangedEventArgs<double>(testData.LastOnVolumeSliderValue, newSliderPosition));
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }

        }

        private static VolumeHandler initVolumeHandler(out MediaElement ele, out Slider slid, out Image img)
        {
            ele = new MediaElement() {Volume = Globals.MaxVolume};
            slid = new Slider() {Value = Globals.MaxSliderValue};
            img = new Image() {Source = null};
            return new VolumeHandler(ref ele, ref slid, ref img,new BitmapImage());
        }
    }
}