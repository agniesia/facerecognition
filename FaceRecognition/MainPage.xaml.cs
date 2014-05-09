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
using Windows.Storage.Pickers;
using Windows.Graphics.Imaging;
using lab01biometria;
using System.Xml.Linq;




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
        image_as_tab ImageToWork;
        BitmapDecoder decoder;
        Guid decoderId;
        byte[] sourcePixels;
        private async void wczytajimage(object sender, RoutedEventArgs e)
        {
            IRandomAccessStream fileStream; // Wczytanie pliku do strumienia
            
            
            
            int w = 0;
            int h = 0;
            
            
            //przyciskiEnabled();
            FileOpenPicker FOP = new FileOpenPicker(); // Klasa okna wybierania pliku
            FOP.ViewMode = PickerViewMode.Thumbnail; // Rodzaj podglądu plików w oknie - tu jako małe obrazy
            FOP.SuggestedStartLocation = PickerLocationId.PicturesLibrary; // Od jakiego katalogu okno powinno zacząć wyświetlanie
            FOP.FileTypeFilter.Add(".bmp"); // filtry, które informują jakie rozszerzenia można wybrać
            FOP.FileTypeFilter.Add(".jpg");
            FOP.FileTypeFilter.Add(".jpeg");
            FOP.FileTypeFilter.Add(".png");
            FOP.FileTypeFilter.Add(".gif");
            StorageFile file = await FOP.PickSingleFileAsync();
            // Uruchomienie wybierania pliku pojedynczego

            if (file != null)
            {
                //przyciskiVisible();

                fileStream = await file.OpenAsync(FileAccessMode.Read);
                // Dekoder będzie potrzebny później przy pracy na obrazie
                BitmapImage bitmapImage = new BitmapImage(); // Stworzenie obiektu obrazu do wyświetlenia
                bitmapImage.SetSource(fileStream); // Przepisanie obrazu ze strumienia do obiektu obrazu przez wartosc
                this.show.Source = bitmapImage; // Przypisanie obiektu obrazu do elementu interfejsu typu "Image" o nazwie "Oryginał"
                // Poniżej znajduje się zapamiętanie dekodera
                w = bitmapImage.PixelWidth;
                h = bitmapImage.PixelHeight;

                switch (file.FileType.ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        decoderId = BitmapDecoder.JpegDecoderId;
                        break;
                    case ".bmp":
                        decoderId = BitmapDecoder.BmpDecoderId;
                        break;
                    case ".png":
                        decoderId = BitmapDecoder.PngDecoderId;
                        break;
                    case ".gif":
                        decoderId = BitmapDecoder.GifDecoderId;
                        break;
                    default:
                        return;
                }

                decoder = await BitmapDecoder.CreateAsync(decoderId, fileStream); // Dekodowanie strumienia za pomocą dekodera
                // Dekodowanie strumienia do klasy z informacjami o jego parametrach
                PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
                BitmapPixelFormat.Bgra8,// Warto tu zwrócić uwagę jak przechowywane są kolory!!!
                BitmapAlphaMode.Straight,
                new BitmapTransform(),
                ExifOrientationMode.IgnoreExifOrientation,
                ColorManagementMode.DoNotColorManage
                );
                sourcePixels = pixelData.DetachPixelData();
                ImageToWork = new image_RGB(sourcePixels, w, h);
                Visitor v = new FaceBiom();
                v.rob(ImageToWork);
                v = new lab01biometria.imageoperation.Otsu();
                v.rob(ImageToWork);
                bitmpe(ImageToWork);
                
                
            }
        }
        private async void bitmpe(image_as_tab obiekt)
        {
            WriteableBitmap writeableBitmap = new WriteableBitmap((int)obiekt.w, (int)obiekt.h);
            
            using (Stream stream = writeableBitmap.PixelBuffer.AsStream())
            {
                await stream.WriteAsync(obiekt.show(), 0, obiekt.show().Length);
            }
            this.show.Source = writeableBitmap;
            
            
        }
        
       
        internal async void btnStartDevice_Click(Object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CameraCaptureUI dialog = new CameraCaptureUI();
            Size aspectRatio = new Size(16, 9);
            dialog.PhotoSettings.CroppedAspectRatio = aspectRatio;
            StorageFile file = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);
            BitmapImage bitmapImage = new BitmapImage();
            IRandomAccessStream fileStream=null;
            if (file != null)
            {

                using (fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    bitmapImage.SetSource(fileStream);



                }

                switch (file.FileType.ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        decoderId = BitmapDecoder.JpegDecoderId;
                        break;
                    case ".bmp":
                        decoderId = BitmapDecoder.BmpDecoderId;
                        break;
                    case ".png":
                        decoderId = BitmapDecoder.PngDecoderId;
                        break;
                    case ".gif":
                        decoderId = BitmapDecoder.GifDecoderId;
                        break;
                    default:
                        return;
                }
                fileStream = await file.OpenAsync(FileAccessMode.Read);
                decoder = await BitmapDecoder.CreateAsync(decoderId, fileStream); // Dekodowanie strumienia za pomocą dekodera
                // Dekodowanie strumienia do klasy z informacjami o jego parametrach
                PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
                BitmapPixelFormat.Bgra8,// Warto tu zwrócić uwagę jak przechowywane są kolory!!!
                BitmapAlphaMode.Straight,
                new BitmapTransform(),
                ExifOrientationMode.IgnoreExifOrientation,
                ColorManagementMode.DoNotColorManage
                );
                sourcePixels = pixelData.DetachPixelData();
            }
            int w = bitmapImage.PixelWidth;
            int h = bitmapImage.PixelHeight;
            ImageToWork = new image_RGB(sourcePixels, w, h);
               
            show.Source= bitmapImage;
        }
        
    }

}