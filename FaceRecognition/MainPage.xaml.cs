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
            SizeChanged += MainPage_SizeChanged;
            DisableButtons();
            org = new lab01biometria.Memento.Originator();
            caretaker = new lab01biometria.Memento.Caretaker();
            
        }

        lab01biometria.Memento.Originator org;
        lab01biometria.Memento.Caretaker caretaker;
        image_as_tab ImageToWork;
        BitmapDecoder decoder;
        Guid decoderId;
        byte[] sourcePixels;
        Visitor method = null;
        private void DisableButtons()
        {
            HSVmethod.IsEnabled=false;
            SV_method.IsEnabled = false;
            RGBmethod.IsEnabled = false;
            Join_method.IsEnabled = false;
            find_elents.IsEnabled = false;

        }
        private void ableButtons()
        {
            HSVmethod.IsEnabled = true;
            SV_method.IsEnabled = true;
            RGBmethod.IsEnabled = true;
            Join_method.IsEnabled = true;
            find_elents.IsEnabled = true;

        }
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
               
                //v = new lab01biometria.imageoperation.Otsu();
                //v.rob(ImageToWork);
                bitmpe(ImageToWork);
                ableButtons();
                org.State = ImageToWork;
                caretaker.Memento = org.SaveMemento();
                
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
            Size aspectRatio = new Size();
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
                int w = bitmapImage.PixelWidth;
                int h = bitmapImage.PixelHeight;
                ImageToWork = new image_RGB(sourcePixels, w, h);

                show.Source = bitmapImage;
                ableButtons();
              

                org.State = ImageToWork;
                caretaker.Memento = org.SaveMemento();
            }
            
        }

        private void RGBmethod_Click(object sender, RoutedEventArgs e)
        {
            method = new EarRecognition.Countur();
            method.rob(ImageToWork);
            bitmpe(ImageToWork);
            org.RestoreMemento(caretaker.Memento);
            ImageToWork = org.State;
        }

        private void HSVmethod_Click(object sender, RoutedEventArgs e)
        {
            method = new EarRecognition.maskashowphoto();
            method.rob(ImageToWork);
            //methodrob(method);
            bitmpe(ImageToWork);
            org.RestoreMemento(caretaker.Memento);
            ImageToWork = org.State;
        }

        private void SV_method_Click(object sender, RoutedEventArgs e)
        {
            method = new EarRecognition.ShowContour();
            method.rob(ImageToWork);
            //methodrob(method);
            bitmpe(ImageToWork);
            org.RestoreMemento(caretaker.Memento);
            ImageToWork = org.State;
        }

        private void Join_method_Click(object sender, RoutedEventArgs e)
        {

            method = new EarRecognition.maskashow();
            method.rob(ImageToWork);
            //methodrob(method);
            bitmpe(ImageToWork);
            org.RestoreMemento(caretaker.Memento);
            ImageToWork = org.State;

        }

        private void find_elents_Click(object sender, RoutedEventArgs e)
        {
            method = new EarRecognition.Points();
            method.rob(ImageToWork);
            //methodrob(method);
            bitmpe(ImageToWork);
            org.RestoreMemento(caretaker.Memento);
            ImageToWork = org.State;
        }
        private async  void methodrob(Visitor v)
        {
            
            await Windows.System.Threading.ThreadPool.RunAsync(new Windows.System.Threading.WorkItemHandler((IAsyncAction action) =>
            {
                v.rob(ImageToWork);
            }));
            
        }


        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            if (e.NewSize.Height / e.NewSize.Width >= 1)
            {

                Grid.SetColumn(this.ramka, 0);
                Grid.SetRow(this.ramka, 0);
                Grid.SetColumnSpan(this.ramka, 2);
                Grid.SetRowSpan(this.ramka, 1);
                //-------------------------------
                Grid.SetColumn(this.text, 0);
                Grid.SetRow(this.text, 1);
                //-------------------------------
                Grid.SetColumn(this.buton, 1);
                Grid.SetRow(this.buton, 1);


               
            }

            else if (e.NewSize.Height / e.NewSize.Width < 1)
            {
                Grid.SetColumn(this.ramka, 0);
                Grid.SetRow(this.ramka, 0);
                Grid.SetColumnSpan(this.ramka, 1);
                Grid.SetRowSpan(this.ramka, 2);
                //-------------------------------
                Grid.SetColumn(this.text, 1);
                Grid.SetRow(this.text, 0);
                //-------------------------------
                Grid.SetColumn(this.buton, 1);
                Grid.SetRow(this.buton, 1);

                
            }
        }
    }

}