using System;

namespace LazZiya.ImageResize
{
    public class RgbaToArray
    {
        private string[] rgbaArray { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="colorString">converts rgba(200,100,50,0.5) to 200,100,50,125</param>
        public RgbaToArray(string colorString)
        {
            rgbaArray = colorString.Remove(0, 5).TrimEnd(')').Split(',');
        }

        public int Alpha
        {
            get
            {
                var alpha1 = double.Parse(rgbaArray[3]);
                if (alpha1 == 1)
                    return 255;

                var alpha2 = Math.Floor(alpha1 / 100 * 255);
                return int.Parse(alpha2.ToString());
            }
        }

        public int Red { get { return int.Parse(rgbaArray[0]); } }
        public int Green { get { return int.Parse(rgbaArray[1]); } }
        public int Blue { get { return int.Parse(rgbaArray[2]); } }
    }
}
