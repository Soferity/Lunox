using Lunox.Core.Enum;
using Windows.UI.Xaml.Controls;

// Boş Sayfa öğe şablonu https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x41f adresinde açıklanmaktadır

namespace Lunox.Views
{
    /// <summary>
    /// Kendi başına kullanılabilecek ya da bir Çerçeve içine taşınabilecek boş bir sayfa.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            _ = new ContentDialog
            {
                Title = "TEST",
                Content = $"{ImageType.ARW}",
                CloseButtonText = "Close",
                IsPrimaryButtonEnabled = true,
                IsSecondaryButtonEnabled = true
            }.ShowAsync();
        }
    }
}