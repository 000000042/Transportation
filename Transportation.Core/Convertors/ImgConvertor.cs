using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation.Core.Convertors
{
    public class ImgConvertor
    {
        public void Image_resize(string input_Image_Path, string output_Image_Path, int new_Width)
        {

            const long quality = 50L;

            Bitmap source_Bitmap = new Bitmap(input_Image_Path);

            double dblWidth_origial = source_Bitmap.Width;

            double dblHeigth_origial = source_Bitmap.Height;

            double relation_heigth_width = dblHeigth_origial / dblWidth_origial;

            int new_Height = (int)(new_Width * relation_heigth_width);

            var new_DrawArea = new Bitmap(new_Width, new_Height);

            using (var graphic_of_DrawArea = Graphics.FromImage(new_DrawArea))

            {
                graphic_of_DrawArea.CompositingQuality = CompositingQuality.HighSpeed;

                graphic_of_DrawArea.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphic_of_DrawArea.CompositingMode = CompositingMode.SourceCopy;



                graphic_of_DrawArea.DrawImage(source_Bitmap, 0, 0, new_Width, new_Height);



                using (var output = System.IO.File.Open(output_Image_Path, FileMode.Create))

                {

                    var qualityParamId = System.Drawing.Imaging.Encoder.Quality;

                    var encoderParameters = new EncoderParameters(1);

                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);


                    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                    new_DrawArea.Save(output, codec, encoderParameters);

                    output.Close();
                }
                graphic_of_DrawArea.Dispose();

            }
            source_Bitmap.Dispose();
        }
    }
}
