using Lunox.Core.ViewModels;

namespace Lunox.Tests.MSTest
{
    // TODO WTS: Add appropriate tests
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestMethod1()
        {
            //TODO WTS: xXx
        }

        // TODO WTS: Add tests for functionality you add to GlanceViewModel.
        [TestMethod]
        public void TestMainViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            GlanceViewModel vm = new GlanceViewModel();
            Assert.IsNotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to SettingsViewModel.
        [TestMethod]
        public void TestSettingsViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            SettingsViewModel vm = new SettingsViewModel();
            Assert.IsNotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to HelpViewModel.
        [TestMethod]
        public void TestHelpViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            HelpViewModel vm = new HelpViewModel();
            Assert.IsNotNull(vm);
        }
    }
}