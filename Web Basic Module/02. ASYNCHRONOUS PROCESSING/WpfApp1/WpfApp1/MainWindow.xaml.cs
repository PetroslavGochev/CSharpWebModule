using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DownloadImage(this.Image1, "https://dogtime.com/assets/uploads/gallery/french-bulldog-dog-breed-pictures/1-puppy.jpg");
            DownloadImage(this.Image2, "https://vetstreet.brightspotcdn.com/dims4/default/5298792/2147483647/crop/0x0%2B0%2B0/resize/645x380/quality/90/?url=https%3A%2F%2Fvetstreet-brightspot.s3.amazonaws.com%2F3d%2F174bd0a0d511e0a2380050568d634f%2Ffile%2FFrench-Bulldog-5-645mk062111.jpg");
            DownloadImage(this.Image3, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRFDgzaB4pqngz_t2rpxdM550RcSxSSCUDOSQ&usqp=CAU");
        }
        
        private void DownloadImage(Image image, string url)
        {
            var client = new HttpClient();
            var request = client.GetAsync(url).GetAwaiter().GetResult();
            var dataBytes = request.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
            image.Source = this.LoadImage(dataBytes);
        }
        private  BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
