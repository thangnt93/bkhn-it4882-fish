using FishServer.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FishTest
{
    
    
    /// <summary>
    ///This is a test class for AquariumControlTest and is intended
    ///to contain all AquariumControlTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AquariumControlTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for NewClientId
        ///</summary>
        [TestMethod()]
        public void NewClientIdTest()
        {
            AquariumControl target = new AquariumControl(); // TODO: Initialize to an appropriate value
            target.AddClientWindow(new FishServer.ClientWindow(0, target, new System.Drawing.Rectangle()));
            target.AddClientWindow(new FishServer.ClientWindow(2, target, new System.Drawing.Rectangle()));
            target.AddClientWindow(new FishServer.ClientWindow(1, target, new System.Drawing.Rectangle()));

            int expected = 1; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.NewClientId();
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
