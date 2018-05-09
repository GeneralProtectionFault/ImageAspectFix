using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageAspectFix
{
    public static class AspectFix
    {
        public static EncoderParameters group4TiffEncoder = new EncoderParameters(1);
        public static ImageCodecInfo tiffCodec = null;


        /*  Create a list of acceptable codecs!!
         * 
         * 
         *  METHODS AND WHAT THEY DO

            GrabImageAttributes:
                Input path to image, get back SumoImage object which has the height & width as well as the resolutions
                which can be passed to other methods
            FixImage:
                Takes Image, returns image.  If ration < .5, correct to 8.5 x 11 in returned image
                OVERLOAD 1: optional outputfile path to write the image to that location
            FixImageAuto:
                Same as FixImage, except sets the ration to 8.5 x 11 regardless of inpjut
                OVERLOAD 1: Do the above, and save to whatever is specified as the output path
         * 
         * 
         * 
        */


        /// <summary>
        /// Returns SumoImage object
        /// Gets the pixel height & width as well as the resolutions of the image
        /// Takes the path to the image (also stores it as a property)
        /// USE THIS OBJECT TO PASS TO OTHER METHODS
        /// </summary>
        /// <param name="pathToImage"></param>
        public static SumoImage GrabImageAttributes(string pathToImage)
        {
            // Store the path int he SumoImage object
            SumoImage statistics = new SumoImage();
            statistics.Path = pathToImage;

            // Set the SumoImage property so we can pass this bitmap to other methods
            Bitmap ActualImage = new Bitmap(pathToImage);
            statistics.ImageBitmap = ActualImage;

            using (FileStream file = new FileStream(pathToImage, FileMode.Open, FileAccess.Read))
            {
                using (Image tif = Image.FromStream(stream: file,
                                                    useEmbeddedColorManagement: false,
                                                    validateImageData: false))
                {
                    statistics.Width = Convert.ToInt32(tif.PhysicalDimension.Width);
                    statistics.Height = Convert.ToInt32(tif.PhysicalDimension.Height);
                    statistics.HorizontalResolution = Convert.ToInt32(tif.HorizontalResolution);
                    statistics.VerticalResolution = Convert.ToInt32(tif.VerticalResolution);
                }

                return statistics;
            }

        }




        /// <summary>
        /// If length - width or width - length ratio is less than .5, correct to 8.5 x 11 ratio and return the corrected image as a bitmap object
        /// </summary>
        /// <param name="filePath">Pass the full path to the image</param>
        /// <param name="DPI">The resolution which will be set if the image is corrected - DEFAULT IS 200</param>
        public static Bitmap FixImage(SumoImage Image, int DPI = 200, float widthRatio = 8.5f, float heightRatio = 11f)
        {
            // If screwed up and portrait
            if (Image.Width / Image.Height < 0.5)
            {
                float targetHeight = Image.Width / (widthRatio / heightRatio);

                Bitmap returnImage = new Bitmap(Image.ImageBitmap, (int)Image.Width, (int)targetHeight);
                returnImage.SetResolution(DPI, DPI);

                return returnImage;

            }
            // If screwed up and landscape...
            else if (Image.Height / Image.Width < 0.5)
            {
                float targetWidth = Image.Height / (widthRatio / heightRatio);

                Bitmap returnImage = new Bitmap(Image.ImageBitmap, (int)targetWidth, (int)Image.Height);
                returnImage.SetResolution(DPI, DPI);

                return returnImage;
            }
            else
                return Image.ImageBitmap;
        }




        /// <summary>
        /// If length - width or width - length ratio is less than .5, correct to 8.5 x 11 ratio and write image to outputFilePath
        /// Output is Group 4 Fax Tiff format
        /// </summary>
        /// <param name="inputFilePath">Pass the full path to the image</param>
        /// <param name="DPI">The resolution which will be set if the image is corrected - DEFAULT IS 200</param>
        /// <param name="outputFilePath">Location you wish to save the modified file to
        /// ***Set this to the same as inputFilePath (SumoImage.Path) to OVERWRITE the original file
        /// Output format is Group 4 TIFF</param>
        public static void FixImage(SumoImage Image, string outputFilePath, int DPI = 200, float widthRatio = 8.5f, float heightRatio = 11f)
        {
            // Get the Group 4 Fax codec (JPEG OR TIFF ONLY)
            group4TiffEncoder.Param[0] = new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionCCITT4);
            ImageCodecInfo[] codec = ImageCodecInfo.GetImageEncoders();

            for (int i = 0; i < codec.Length; i++)
            {
                // ADD IN HERE!  IF CODEC TYPE == "image/ + VARIABLE"????
                if (codec[i].MimeType == "image/tiff")
                {
                    tiffCodec = codec[i];
                    break;
                }
            }

            // Error if the desired codec is not installed
            if (tiffCodec == null)
            {
                throw new Exception("Error: Attempt to save image in a codec that is not installed.");
            }


            // If screwed up and portrait
            if (Image.Width / Image.Height < 0.5)
            {
                float targetHeight = Image.Width / (widthRatio / heightRatio);

                // Open the TIFF image 
                Bitmap returnImage = new Bitmap(Image.ImageBitmap, (int)Image.Width, (int)targetHeight);
                returnImage.SetResolution(DPI, DPI);

                if (File.Exists(outputFilePath))
                    File.Delete(outputFilePath);

                returnImage.Save(outputFilePath, tiffCodec, group4TiffEncoder);
                returnImage.Dispose();

            }
            // If screwed up and landscape...
            else if (Image.Height / Image.Width < 0.5)
            {
                float targetWidth = Image.Height / (widthRatio / heightRatio);

                // Open the TIFF image 
                Bitmap returnImage = new Bitmap(Image.ImageBitmap, (int)targetWidth, (int)Image.Height);
                returnImage.SetResolution(DPI, DPI);

                if (File.Exists(outputFilePath))
                    File.Delete(outputFilePath);

                // filepath, encoder, encoder parameters
                // encoder is container (tiff, etc...), parameter is compression
                returnImage.Save(outputFilePath, tiffCodec, group4TiffEncoder);
                returnImage.Dispose();
            }
        }







        /// <summary>
        /// Sets image to 8.5 x 11 ratio (default) REGARDLESS of input.
        /// Returns corrected image as a Bitmap object
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="DPI"></param>
        public static Bitmap FixImageAuto(SumoImage Image, int DPI = 200, float widthRatio = 8.5f, float heightRatio = 11f)
        {
            if (Image.Width < Image.Height) // Portrait
            {
                float targetHeight = Image.Width / (widthRatio / heightRatio);

                Bitmap returnBitmap = new Bitmap(Image.ImageBitmap, (int)Image.Width, (int)targetHeight);
                returnBitmap.SetResolution(DPI, DPI);

                return returnBitmap;

            }
            else // Landscape
            {
                float targetWidth = Image.Height / (widthRatio / heightRatio);

                Bitmap returnBitmap = new Bitmap(Image.ImageBitmap, (int)targetWidth, (int)Image.Height);
                returnBitmap.SetResolution(DPI, DPI);

                return returnBitmap;
            }
        }




        /// <summary>
        /// Automatically change image to 8.5/11 ratio (default) AND
        /// saves to outputFilePath
        /// </summary>
        /// <param name="Image"></param>
        /// <param name="outputFilePath">Set to same as input Path (SumoImage.Path) to overwrite
        /// Output format is Group 4 TIFF</param>
        /// <param name="DPI"></param>
        /// <returns></returns>
        public static void FixImageAuto(SumoImage Image, string outputFilePath, int DPI = 200, float widthRatio = 8.5f, float heightRatio = 11f)
        {
            // Get the Group 4 Fax codec
            group4TiffEncoder.Param[0] = new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionCCITT4);
            ImageCodecInfo[] codec = ImageCodecInfo.GetImageEncoders();

            for (int i = 0; i < codec.Length; i++)
            {
                if (codec[i].MimeType == "image/tiff")
                {
                    tiffCodec = codec[i];
                    break;
                }
            }


            if (Image.Width < Image.Height) // Portrait
            {
                float targetHeight = Image.Width / (widthRatio / heightRatio);

                Bitmap returnBitmap = new Bitmap(Image.ImageBitmap, (int)Image.Width, (int)targetHeight);
                returnBitmap.SetResolution(DPI, DPI);

                if (System.IO.File.Exists(outputFilePath))
                    System.IO.File.Delete(outputFilePath);

                returnBitmap.Save(outputFilePath, tiffCodec, group4TiffEncoder);
                returnBitmap.Dispose();

            }
            else // Landscape
            {
                float targetWidth = Image.Height / (widthRatio / heightRatio);

                Bitmap returnBitmap = new Bitmap(Image.ImageBitmap, (int)targetWidth, (int)Image.Height);
                returnBitmap.SetResolution(DPI, DPI);

                if (System.IO.File.Exists(outputFilePath))
                    System.IO.File.Delete(outputFilePath);

                returnBitmap.Save(outputFilePath, tiffCodec, group4TiffEncoder);
                returnBitmap.Dispose();
            }
        }




    }






}
