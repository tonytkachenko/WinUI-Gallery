using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using muxc = Microsoft.UI.Xaml.Controls;
using controls = Microsoft.Toolkit.Uwp.UI.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Text;
using Windows.Storage;

namespace AppUIBasics.ControlPages
{
    public sealed partial class TwoPaneViewPage : Page
    {
        public TwoPaneViewPage()
        {
            this.InitializeComponent();
        }

        private void EditZone_TextChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
            {
                string text = Toolbar.Formatter?.Text;
                Previewer.Text = string.IsNullOrWhiteSpace(text) ? "Nothing to Preview" : text;
            }

#pragma warning disable 612, 618
            /// <summary>
            /// If the Markdown Editor window scrolls, scroll the Editor window as well and vice-versa.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            /// 
            private void MarkEditor_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
            {
                // Note: we are using "obsolete" ScrollToVerticalOffset and ScrollToHorizontalOffset
                // methods by design.

                var scrollViewer = (ScrollViewer)sender;
                if (scrollViewer == Editor)
                {
                    MarkEditor.ScrollToVerticalOffset(scrollViewer.VerticalOffset);
                    MarkEditor.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset);

                    // Note: This is the "new" way of scrolling, but with current implementation provides
                    // a "jumpy" scrolling experience, thus the "obsolete" methods above. 
                    // This is put here for reference in case that problem is fixed in future builds.
                    //MarkEditor.ChangeView(scrollViewer.HorizontalOffset, scrollViewer.VerticalOffset, null);
                }
                else
                {
                    Editor.ScrollToVerticalOffset(scrollViewer.VerticalOffset);
                    Editor.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset);

                    // Note: This is the "new" way of scrolling, but with current implementation provides
                    // a "jumpy" scrolling experience, thus the older "obsolete" methods above. 
                    // This is put here for reference in case that problem is fixed in future builds.
                    //Editor.ChangeView(scrollViewer.HorizontalOffset, scrollViewer.VerticalOffset, null);
                }
            }
#pragma warning restore 612, 618

            /// <summary>
            /// Load our markdown sample text and place it into the RichTextBox control.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private async void Page_Loaded(object sender, RoutedEventArgs e)
            {

              /*  if (Previewer != null)
                {
                    Previewer.LinkClicked += Previewer_LinkClicked;
                    Previewer.ImageClicked += Previewer_ImageClicked;
                    Previewer.CodeBlockResolving += Previewer_CodeBlockResolving;
                }*/

                // Load the initial demo data from the file.  Make sure the file properties are set to 
                // Build Action - Content and Copy to Output Directory - Always
                try
                {
                    StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///ControlPagesSampleCode/TwoPaneView/sample.txt"));
                    Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                    EditZone.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, fileStream);
                }
                catch (Exception)
                {
                    if (EditZone != null)
                    {
                        EditZone.TextDocument.SetText(TextSetOptions.None, "## Error Loading Content ##");
                    }
                }

            }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb && twoPaneViewDemo != null)
            {
                string TallModeName = rb.Tag.ToString();

                switch (TallModeName)
                {
                    case "BottomTop":
                        twoPaneViewDemo.TallModeConfiguration = muxc.TwoPaneViewTallModeConfiguration.BottomTop;
                        break;
                    case "TopBottom":
                        twoPaneViewDemo.TallModeConfiguration = muxc.TwoPaneViewTallModeConfiguration.TopBottom;
                        break;
                }
            }
        }
    }
}
