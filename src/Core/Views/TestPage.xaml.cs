using Lunox.Services;
using Windows.UI.Xaml.Controls;

// Boş Sayfa öğe şablonu https://go.microsoft.com/fwlink/?LinkId=234238 adresinde açıklanmaktadır

namespace Lunox.Views
{
    /// <summary>
    /// Kendi başına kullanılabilecek ya da bir Çerçeve içine gezinebilecek boş bir sayfa.
    /// </summary>
    public sealed partial class TestPage : Page
    {
        /// <summary>
        /// 
        /// </summary>
        public TestPage()
        {
            InitializeComponent();
            Glance.Text = GlanceSelectorService.GlanceClass.Replace("Page", "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Glance_TextChanged(object sender, TextChangedEventArgs e)
        {
            await GlanceSelectorService.SetGlanceAsync(Glance.Text + "Page");
        }
    }
}