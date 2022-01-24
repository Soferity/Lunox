using Lunox.Core.ViewModels;
using Xunit;

namespace Lunox.Tests.XUnit
{
    // TODO WTS: Add appropriate tests
    public class Tests
    {
        [Fact]
        public void TestMethod1()
        {
            //TODO WTS: xXx
        }

        // TODO WTS: Add tests for functionality you add to GlanceViewModel.
        [Fact]
        public void TestGlanceViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            GlanceViewModel vm = new GlanceViewModel();
            Assert.NotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to SettingsViewModel.
        [Fact]
        public void TestSettingsViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            SettingsViewModel vm = new SettingsViewModel();
            Assert.NotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to HelpViewModel.
        [Fact]
        public void TestHelpViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            HelpViewModel vm = new HelpViewModel();
            Assert.NotNull(vm);
        }
    }
}