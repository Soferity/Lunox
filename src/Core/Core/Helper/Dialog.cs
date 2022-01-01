#region Imports

using Windows.UI.Xaml.Controls;

#endregion

namespace Lunox.Cores.Helper
{
    #region DialogHelper

    /// <summary>
    /// 
    /// </summary>
    public static class Dialog
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Content"></param>
        public static void SendDialog(string Content = "Content")
        {
            SendDialog(Content, null, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="Title"></param>
        public static void SendDialog(string Content = "Content", string Title = "Title")
        {
            SendDialog(Content, Title, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="Title"></param>
        /// <param name="Drag"></param>
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

        #endregion
    }

    #endregion
}
