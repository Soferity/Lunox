#region Imports

using Lunox.Core.ViewModels.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skylark.Standard.Extension.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

#endregion

namespace Lunox.Core.Views.Json
{
    #region SerializePage

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class SerializePage : Page
    {
        #region Functions

        /// <summary>
        /// 
        /// </summary>
        public SerializeViewModel ViewModel { get; } = new SerializeViewModel();

        /// <summary>
        /// 
        /// </summary>
        public SerializePage()
        {
            InitializeComponent();
        }

        private Color currentColor = Colors.DodgerBlue;

        private static string FormatJson1(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }

        private static string FormatJson2(string json)
        {
            return JToken.Parse(json).ToString();
        }

        private static string FormatJson3(string json)
        {
            return JsonExtension.ToBeauty(json);
        }

        private void Serialize_Click(object sender, RoutedEventArgs e)
        {
            editor.Document.GetText(TextGetOptions.None, out string text);

            editor.Document.SetText(TextSetOptions.None, FormatJson3(text));

            editor.Document.Selection.CharacterFormat.ForegroundColor = currentColor;
        }

        internal async Task<string> UsingStream(StorageFile sampleFile)
        {
            IRandomAccessStream stream = await sampleFile.OpenAsync(FileAccessMode.Read);
            ulong size = stream.Size;
            string text = string.Empty;
            using (IInputStream inputStream = stream.GetInputStreamAt(0))
            {
                using DataReader dataReader = new(inputStream);
                uint numBytesLoaded = await dataReader.LoadAsync((uint)size);
                text = dataReader.ReadString(numBytesLoaded);
            }
            return text;
        }

        private async void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            // Open a text file.
            FileOpenPicker open = new()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            open.FileTypeFilter.Add(".rtf");
            open.FileTypeFilter.Add(".txt");
            open.FileTypeFilter.Add(".json");

            StorageFile file = await open.PickSingleFileAsync();

            if (file != null)
            {
                switch (file.FileType)
                {
                    case ".rtf":
                        using (IRandomAccessStream randAccStream = await file.OpenAsync(FileAccessMode.Read))
                        {
                            // Load the file into the Document property of the RichEditBox.
                            editor.Document.LoadFromStream(TextSetOptions.FormatRtf, randAccStream);
                        }
                        break;
                    case ".txt" or ".json":
                        editor.Document.SetText(TextSetOptions.None, await UsingStream(file));
                        break;
                    default:
                        break;
                }
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker savePicker = new()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Rich Text Format File", new List<string>() { ".rtf" });
            savePicker.FileTypeChoices.Add("Plain Text File", new List<string>() { ".txt" });
            savePicker.FileTypeChoices.Add("JavaScript Object Notation File", new List<string>() { ".json" });

            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "Json Serialize";

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Prevent updates to the remote version of the file until we 
                // finish making changes and call CompleteUpdatesAsync.
                CachedFileManager.DeferUpdates(file);
                // write to file
                using (IRandomAccessStream randAccStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    switch (file.FileType)
                    {
                        case ".rtf":
                            editor.Document.SaveToStream(TextGetOptions.FormatRtf, randAccStream);
                            break;
                        case ".txt" or ".json":
                            editor.Document.SaveToStream(TextGetOptions.None, randAccStream);
                            break;
                        default:
                            break;
                    }
                }

                // Let Windows know that we're finished changing the file so the 
                // other app can update the remote version of the file.
                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                if (status != FileUpdateStatus.Complete)
                {
                    MessageDialog errorBox = new("File " + file.Name + " couldn't be saved.");
                    await errorBox.ShowAsync();
                }
            }
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            editor.Document.Selection.CharacterFormat.Bold = FormatEffect.Toggle;
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            editor.Document.Selection.CharacterFormat.Italic = FormatEffect.Toggle;
        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            // Extract the color of the button that was clicked.
            Button clickedColor = (Button)sender;
            Rectangle rectangle = (Rectangle)clickedColor.Content;
            Color color = ((SolidColorBrush)rectangle.Fill).Color;

            editor.Document.Selection.CharacterFormat.ForegroundColor = color;

            fontColorButton.Flyout.Hide();
            editor.Focus(FocusState.Keyboard);
            currentColor = color;
        }

        private void FindBoxHighlightMatches()
        {
            //5-10k neyse karakter sınırı koy. ondan fazla karakter varsa aramasın
            //ya da kendini gizlesin
            FindBoxRemoveHighlights();

            Color highlightBackgroundColor = (Color)App.Current.Resources["SystemColorHighlightColor"];
            Color highlightForegroundColor = (Color)App.Current.Resources["SystemColorHighlightTextColor"];

            string textToFind = findBox.Text;
            if (textToFind != null)
            {
                ITextRange searchRange = editor.Document.GetRange(0, 0);
                while (searchRange.FindText(textToFind, TextConstants.MaxUnitCount, FindOptions.None) > 0)
                {
                    searchRange.CharacterFormat.BackgroundColor = highlightBackgroundColor;
                    searchRange.CharacterFormat.ForegroundColor = highlightForegroundColor;
                }
            }
        }

        private void FindBoxRemoveHighlights()
        {
            ITextRange documentRange = editor.Document.GetRange(0, TextConstants.MaxUnitCount);
            SolidColorBrush defaultBackground = editor.Background as SolidColorBrush;
            SolidColorBrush defaultForeground = editor.Foreground as SolidColorBrush;

            documentRange.CharacterFormat.BackgroundColor = defaultBackground.Color;
            documentRange.CharacterFormat.ForegroundColor = defaultForeground.Color;
        }

        private void Editor_GotFocus(object sender, RoutedEventArgs e)
        {
            editor.Document.GetText(TextGetOptions.UseCrlf, out string currentRawText);

            // reset colors to correct defaults for Focused state
            ITextRange documentRange = editor.Document.GetRange(0, TextConstants.MaxUnitCount);
            SolidColorBrush background = (SolidColorBrush)App.Current.Resources["TextControlBackgroundFocused"];

            if (background != null)
            {
                documentRange.CharacterFormat.BackgroundColor = background.Color;
            }
        }

        private void Editor_TextChanged(object sender, RoutedEventArgs e)
        {
            if (editor.Document.Selection.CharacterFormat.ForegroundColor != currentColor)
            {
                editor.Document.Selection.CharacterFormat.ForegroundColor = currentColor;
            }
        }

        #endregion
    }

    #endregion
}