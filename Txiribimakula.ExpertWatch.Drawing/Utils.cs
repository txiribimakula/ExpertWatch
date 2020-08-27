using System;

namespace Txiribimakula.ExpertWatch.Drawing
{
    public class Utils
    {
        public static double AbsOuterPow10(double x) {
            return Math.Pow(10, Math.Ceiling(Math.Log10(Math.Abs(x))));
        }
    }
}
