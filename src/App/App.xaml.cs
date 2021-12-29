using Lunox.Views;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Lunox
{
    /// <summary>
    /// Varsayılan Uygulama sınıfını tamamlayacak uygulamaya özgü davranış sağlar.
    /// </summary>
    public sealed partial class App : Application
    {
        /// <summary>
        /// Tek örnekli uygulama nesnesini başlatır. Kodunuzun çalışacak ilk satırıdır.
        /// Mantıksal olarak main() veya WinMain() eşdeğeridir. 
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        /// <summary>
        /// Uygulama son kullanıcı tarafından normal olarak başlatıldığında çağrılır. Diğer giriş noktaları
        /// uygulamanın belirli bir dosyayı açmak için çalıştırılması gibi durumlarda kullanılır.
        /// </summary>
        /// <param name="e">Başlatma isteği ve işlemi ile ilgili ayrıntılar.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Pencerede içerik varken uygulama başlatmayı tekrarlamayın,
            // pencerenin etkin olduğundan emin olun
            if (rootFrame == null)
            {
                // Gezinti bağlamı olarak kullanılacak bir Çerçeve oluşturun ve ilk sayfaya gidin
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Önceki askıya alınmış uygulamadan durumu yükle
                }

                // Çerçeveyi geçerli Pencereye yerleştirin
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // Gezinme yığını geri yüklenmediğinde, ilk sayfaya gidin,
                    // gerekli bilgiyi gezinti parametresi olarak geçirerek
                    // oluşturun
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Geçerli pencerenin etkin olduğundan emin olun
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Belirli bir sayfaya gitme işlemi başarısız olduğunda çağrılır
        /// </summary>
        /// <param name="sender">Gitme işleminde başarısız olan çerçeve</param>
        /// <param name="e">Gitme işlemi hatasıyla ilgili ayrıntılar</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Uygulama yürütmesi askıya alınırken çağırılır. Uygulama durumu kaydedilir
        /// uygulamanın sonlandırılacağı veya bellekte bulunan içerikle kaldığı yerden devam edeceği
        /// bilinmeden kaydedilir.
        /// </summary>
        /// <param name="sender">Askıya alma isteğinin kaynağı.</param>
        /// <param name="e">Askıya alma isteğiyle ilgili ayrıntılar.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Uygulama durumunu kaydet ve tüm arka plan etkinliklerini durdur
            deferral.Complete();
        }
    }
}
