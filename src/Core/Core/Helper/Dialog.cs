using Windows.UI.Xaml.Controls;

namespace Lunox.Core.Helper
{
    public static class Dialog
    {
        public static void SendDialog(string Content = "Content")
        {
            SendDialog(Content, null, false);
        }

        public static void SendDialog(string Content = "Content", string Title = "Title")
        {
            SendDialog(Content, Title, false);
        }

        public static void SendDialog(string Content = "Content", string Title = "Title", bool Drag = true)
        {
            try
            {
                _ = new ContentDialog
                {
                    Title = Title,
                    Content = Content,
                    CloseButtonText = "Çıkış",
                    PrimaryButtonText = "Öncelik",
                    SecondaryButtonText = "İptal",
                    IsPrimaryButtonEnabled = true,
                    IsSecondaryButtonEnabled = true,
                    CanDrag = true,
                }.ShowAsync();
            }
            catch
            {
                //
            }
        }
    }
}
