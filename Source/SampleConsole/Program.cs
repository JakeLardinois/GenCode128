using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GenCode128;

namespace SampleConsole
{
    class Program
    {
        private const int BARCODEWEIGHT = 1;


        static void Main(string[] args)
        {
            Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Version);
            //CreateFontBarcode("*Sample123*");
            CreateGenCode128Barcode("*Sample123*");
            Console.WriteLine("Done!");
            //Console.ReadLine();
        }

        private static void CreateFontBarcode(string code)
        {
            var myBitmap = new Bitmap(500, 50);
            var g = Graphics.FromImage(myBitmap);
            var jgpEncoder = GetEncoder(ImageFormat.Jpeg);

            g.Clear(Color.White);

            //Draw a barcode from a string by just using a font already installed on your computer inside of 'C:\Windows\Fonts'
            var strFormat = new StringFormat { Alignment = StringAlignment.Center };
            g.DrawString(code.ToUpper(), new Font("Free 3 of 9", 50), Brushes.Black, new Rectangle(1, 1, 500, 50), strFormat);

            var myEncoder = Encoder.Quality;
            var myEncoderParameters = new EncoderParameters(1);

            var myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            myBitmap.Save(@"c:\Temp\Barcode.jpg", jgpEncoder, myEncoderParameters);
        }

        private static void CreateGenCode128Barcode(string code)
        {
            var myBitmap = new Bitmap(500, 50);
            var g = Graphics.FromImage(myBitmap);
            var jgpEncoder = GetEncoder(ImageFormat.Jpeg);

            g.Clear(Color.White);

            //Draw a Code 128 Barcode using GenCode128 Library
            g.DrawImage(Code128Rendering.MakeBarcodeImage(code, BARCODEWEIGHT, true), new System.Drawing.Rectangle(1, 1, 500, 50));

            var myEncoder = Encoder.Quality;
            var myEncoderParameters = new EncoderParameters(1);

            var myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            myBitmap.Save(@"c:\Temp\Barcode.jpg", jgpEncoder, myEncoderParameters);
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            var codecs = ImageCodecInfo.GetImageDecoders();

            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
