using System.Windows.Controls;
using System.Windows.Media.Imaging;
using FinalProjMediaPlayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinalProjMediaPlayerTests
{
    [TestClass]
    public class FunctionImageToggleTests
    {
        private int _val;
        [TestMethod]
        public void FunctionImageToggleTest()
        {
            Image i;
            FunctionImageToggle testData = initTestData(out i);
            Assert.AreEqual(_val ,0);
            Assert.IsNull(i.Source);

            testData.toggle();
            Assert.AreEqual(_val,2);
            Assert.IsTrue(testData.Toggled);
            Assert.IsTrue(i.Source is BitmapImage);

            testData.toggle();
            Assert.AreEqual(_val,1);
            Assert.IsFalse(testData.Toggled);
            Assert.IsNull(i.Source);

            testData.toggle();
            Assert.AreEqual(_val, 2);
            Assert.IsTrue(testData.Toggled);
            Assert.IsTrue(i.Source is BitmapImage);
        }


        [TestMethod]
        public void forceOffTest()
        {
            Image i;
            FunctionImageToggle testData = initTestData(out i);
            Assert.AreEqual(_val, 0);
            Assert.IsNull(i.Source);

            testData.forceOff();
            Assert.IsNull(i.Source);
            Assert.IsFalse(testData.Toggled);
            Assert.AreEqual(_val,1);
        }

        [TestMethod]
        public void forceOnTest()
        {
            Image i;
            FunctionImageToggle testData = initTestData(out i);
            Assert.AreEqual(_val, 0);
            Assert.IsNull(i.Source);

            testData.forceOn();
            Assert.IsTrue(i.Source is BitmapImage);
            Assert.AreEqual(_val,2);
            Assert.IsTrue(testData.Toggled);
        }

        private int _genVal;
        [TestMethod]
        public void GenericFunctionImageToggleTests()
        {
            Image i;
            FunctionImageToggle<int> testData = initGenericFunctionImageToggle(out i);
            Assert.AreEqual(_genVal, 0);
            Assert.IsNull(i.Source);

            testData.toggle(1,2);
            Assert.AreEqual(_genVal, 2);
            Assert.IsTrue(testData.Toggled);
            Assert.IsTrue(i.Source is BitmapImage);

            testData.toggle(1,2);
            Assert.AreEqual(_genVal, 1);
            Assert.IsFalse(testData.Toggled);
            Assert.IsNull(i.Source);

            testData.toggle(1,2);
            Assert.AreEqual(_genVal, 2);
            Assert.IsTrue(testData.Toggled);
            Assert.IsTrue(i.Source is BitmapImage);
        }

        [TestMethod]
        public void GenericForceOffTests()
        {
            Image i;
            FunctionImageToggle<int> testData = initGenericFunctionImageToggle(out i);
            Assert.AreEqual(_genVal, 0);
            Assert.IsNull(i.Source);

            testData.forceOff(1);
            Assert.IsNull(i.Source);
            Assert.IsFalse(testData.Toggled);
            Assert.AreEqual(_genVal, 1);
        }

        [TestMethod]
        public void GenericForceOnTests()
        {
            Image i;
            FunctionImageToggle<int> testData = initGenericFunctionImageToggle(out i);
            Assert.AreEqual(_genVal, 0);
            Assert.IsNull(i.Source);

            testData.forceOn(2);
            Assert.IsTrue(i.Source is BitmapImage);
            Assert.AreEqual(_genVal, 2);
            Assert.IsTrue(testData.Toggled);
        }

        private FunctionImageToggle<int> initGenericFunctionImageToggle(out Image i)
        {
            i = new Image() {Source = null};
            _genVal = 0;
            var testData = new FunctionImageToggle<int>(new BitmapImage(),
                ref i,
                val => { _genVal = val; }
                ,
                val => { _genVal = val; });
            return testData;
        }

        private FunctionImageToggle initTestData(out Image i)
        {
            i = new Image {Source = null};
            _val = 0;
            FunctionImageToggle testData = new FunctionImageToggle(new BitmapImage(),
                ref i,
                () => { _val = 1; }
                ,
                () => { _val = 2; });
            return testData;
        }
        
    }
}