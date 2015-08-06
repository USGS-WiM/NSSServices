//------------------------------------------------------------------------------
//----- FlowTimeSeries ---------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2014 WiM - USGS

//    authors:  Jeremy K. Newson USGS Wisconsin Internet Mapping
//              
//  
//   purpose:   Inherits from Base TimeSeries object
//           
//discussion: 
//
#region "Comments"
//08.07.2014 jkn - adapted from old vb code
//10.30.2012 jkn - Created

#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace WiM.TimeSeries
{
    public class FlowTimeSeries:TimeSeriesBase
    {
        #region "Properties"
        #endregion
        #region "Collections & Dictionaries"
        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        public FlowTimeSeries(String tsName, String description)
            : base(tsName, 1, description)
        {
        }
        #endregion
        #region IDisposable Support
        // Track whether Dispose has been called.
        private bool disposed = false;
        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                base.Dispose(disposing);

                if (disposing)
                {

                    // TODO:Dispose managed resources here.
                    //ie component.Dispose();

                }//EndIF

                // TODO:Call the appropriate methods to clean up
                // unmanaged resources here.
                //ComRelease(Extent);

                // Note disposing has been done.
                disposed = true;


            }//EndIf
        }//End Dispose
        #endregion
        #endregion
        #region "Methods"
        public Boolean LoadNWISDailyValues(String stationID, String nwisdv)
        {
            string[] ts = null;
            try
            {
                //removes headers
                IEnumerable<string> body = nwisdv.Split('\n').SkipWhile(x => x.Contains('#'));
                var hIndex = body.First().Split('\t').ToList<string>();

                var qIndex = hIndex.FindIndex(s => s.EndsWith("_00060_00003"));
                var dIndex = hIndex.IndexOf("datetime");

                //USGS file usually contains 2 rows before data 
                foreach (string row in body.Skip(2))
                {
                    ts = row.Split('\t');
                    if (ts.Count() != hIndex.Count()) continue;

                    string date = ts[dIndex];

                    DateTime d;
                    if (!DateTime.TryParse(date, out d)) continue;

                    Double? v;
                    v = ToNullable(ts[qIndex]);                       

                    if (v.HasValue) Add(new TimeSeriesObservation(d, v));
                    else if (!string.IsNullOrEmpty(ts[qIndex])) Add(new TimeSeriesObservation(d, v, ts[qIndex]));

                }//next row

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion
        #region "Helper Methods"
        public Double? ToNullable(string s) {
            Double v;
            if (Double.TryParse(s, out v)) return v;
            return null;
        }
        #endregion
        #region "Structures"
        //A structure is a value type. When a structure is created, the variable to which the struct is assigned holds
        //the struct's actual data. When the struct is assigned to a new variable, it is copied. The new variable and
        //the original variable therefore contain two separate copies of the same data. Changes made to one copy do not
        //affect the other copy.

        //In general, classes are used to model more complex behavior, or data that is intended to be modified after a
        //class object is created. Structs are best suited for small data structures that contain primarily data that is
        //not intended to be modified after the struct is created.
        #endregion
        #region "Asynchronous Methods"

        #endregion
        #region "Enumerated Constants"
        #endregion


    }//end class
}//end namespace
