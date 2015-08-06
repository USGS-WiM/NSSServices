using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WiM.Utilities
{
    public static class MathOps
    {
        public static Double? LinearInterpolate(Double x1, Double x2, Double y1, Double y2, Double x)
        {
            try
            {
                return (((x2 - x) * y1 + (x - x1) * y2)) / (x2 - x1);
            }
            catch (Exception e)
            {
                
                throw new Exception("Cannot interpolate");
            }
            
        }
    }//end class
}//end namespace