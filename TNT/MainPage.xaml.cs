using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace TNT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void AskButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = await Utils.SpeechToTextAsync();
                inputTextBox.Text = text;
            }
            catch (System.Runtime.InteropServices.COMException ex) when (ex.HResult == unchecked((int)0x80045509))
            {
                ContentDialog Dialog = new ContentDialog()
                {
                    Title = "The speech privacy policy was not accepted",
                    Content = "You need to turn on a button called 'Get to know me'...",
                    PrimaryButtonText = "Shut up",
                    SecondaryButtonText = "Shut up and show me the setting"
                };
                if (await Dialog.ShowAsync() == ContentDialogResult.Secondary)
                {
                    const string uriToLaunch = "ms-settings:privacy-speechtyping";
                    //"http://stackoverflow.com/questions/42391526/exception-the-speech-privacy-policy-" + 
                    //"was-not-accepted-prior-to-attempting-a-spee/43083877#43083877";
                    var uri = new Uri(uriToLaunch);

                    var success = await Windows.System.Launcher.LaunchUriAsync(uri);

                    if (!success) await new ContentDialog
                    {
                        Title = "Oops! Something went wrong...",
                        Content = "The settings app could not be opened.",
                        PrimaryButtonText = "Shut your mouth up!"
                    }.ShowAsync();
                }
            }

        }
    }
}
