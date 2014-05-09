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
using Windows.Media.MediaProperties;
using Windows.Media.Capture;
using Windows.Media;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;




// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FaceRecognition
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
       
        internal async void btnStartDevice_Click(Object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CameraCaptureUI dialog = new CameraCaptureUI();
            Size aspectRatio = new Size(16, 9);
            dialog.PhotoSettings.CroppedAspectRatio = aspectRatio;
            StorageFile file = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (file != null)
            {
                BitmapImage bitmapImage = new BitmapImage();
                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    bitmapImage.SetSource(fileStream);
                }
            }
            
        }
        
    }
}