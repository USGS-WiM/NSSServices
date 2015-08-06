//------------------------------------------------------------------------------
//----- TimeSeriesBase ---------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2014 WiM - USGS

//    authors:  Jeremy K. Newson USGS Wisconsin Internet Mapping
//              
//  
//   purpose:   Base TimeSeries object
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
using System.Threading.Tasks;

namespace WiM.TimeSeries
{
    public abstract class TimeSeriesBase:IDisposable
    {
    #region "Properties"
    public String Name { get; protected set; }
    public int SeriesID { get; protected set; }
    //public Boolean IsValid { get; protected set; }
    public String SeriesDescription { get; protected set; }
    public Double ValueMax 
    {
        get
        {
            if(Observations.Count > 0)
                return Observations.Where(v=>v.Value.HasValue).Max(o => o.Value.Value);
            return -999;
        }
    }
    public Double ValueMin 
    {
        get
        {
            if (Observations.Count > 0)
                return Observations.Where(v=>v.Value.HasValue).Min(o => o.Value.Value);
            return -999;
        }
    }
    public DateTime StartDate 
    {
        get
        {
            if (Observations.Count > 0)
                return Observations.Min(o => o.Date);
            return DateTime.MinValue;
        }
    }
    public DateTime EndDate
    {
        get
        {
            if (Observations.Count > 0)
                return Observations.Max(o => o.Date);
            return DateTime.MinValue;
        }
    }
    public String ValueUnits { get; protected set; }
    #endregion
    #region "Collections & Dictionaries"
    private List<TimeSeriesObservation> _observations;
    public List<TimeSeriesObservation> Observations { 
        get 
        {
            return _observations.OrderBy(o=>o.Date).ToList();
        }
    }

    public Int32 Count()
    {
        return _observations.Count();
    }
    public TimeSeriesObservation Observation(DateTime key)
    {
        return Observations.FirstOrDefault(o => o.Date == key);
    }
    public void Add(TimeSeriesObservation t)
    {
        try
        {
            //if (Observations.FirstOrDefault(o => o.Date == t.Date) == null)
                _observations.Add(t);
        }
        catch (Exception)
        {                
                
        }
    }
    public void Add(DateTime dt, Double val)
    {
        TimeSeriesObservation ts = new TimeSeriesObservation(dt, val);
        Add(ts);
    }
    public void Add(DateTime dt, Double val, string code)
    {
        TimeSeriesObservation ts = new TimeSeriesObservation(dt, val, code);
        Add(ts);
    }
    public void Remove(DateTime Key)
    {
        TimeSeriesObservation tx = Observations.FirstOrDefault(o => o.Date == Key);
        if (tx != null)
            _observations.Remove(tx);
    }
    public void Clear()
    {
        _observations.Clear();
    }
        
    #endregion
    #region "Constructor and IDisposable Support"
    #region Constructors
    public TimeSeriesBase(String tsName, Int32 id, String description)
    {
        this.Name = tsName;
        this.SeriesID = id;
        this.SeriesDescription = description;
        _observations = new List<TimeSeriesObservation>();
    }
    #endregion
    #region IDisposable Support
    // Track whether Dispose has been called.
    private bool disposed = false;

    // Implement IDisposable.
    // Do not make this method virtual.
    // A derived class should not be able to override this method.
    public void Dispose()
    {
        Dispose(true);
        // This object will be cleaned up by the Dispose method.
        // Therefore, you should call GC.SupressFinalize to
        // take this object off the finalization queue
        // and prevent finalization code for this object
        // from executing a second time.
        GC.SuppressFinalize(this);
    } //End Dispose

    // Dispose(bool disposing) executes in two distinct scenarios.
    // If disposing equals true, the method has been called directly
    // or indirectly by a user's code. Managed and unmanaged resources
    // can be disposed.
    // If disposing equals false, the method has been called by the
    // runtime from inside the finalizer and you should not reference
    // other objects. Only unmanaged resources can be disposed.
    protected virtual void Dispose(bool disposing)
    {
        // Check to see if Dispose has already been called.
        if (!this.disposed)
        {
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
    public Boolean SetTimeSeriesRange(DateTime sDate, DateTime eDate)
    {
        try
        {
            var tseries = _observations.Where(ts => ts.Date >= sDate.Date && ts.Date <= eDate.Date);
            _observations = tseries.ToList();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    public IDictionary<Double, TimeSeriesObservation> GetProbabilityOfExceedance()
    {
        Dictionary<Double, TimeSeriesObservation> result = new Dictionary<Double, TimeSeriesObservation>();
        List<TimeSeriesObservation> desending = Observations.OrderByDescending(o => o.Value).ToList();
        Double rank = 1;
        Double N = desending.Count;
        foreach (TimeSeriesObservation item in desending.Where(x=> x.Value != null))
        {
            //calculate exceedance
            //rank/n+1
            Double key = rank / (N + 1);
            if (!result.ContainsKey(key))
                result.Add(key, item);
            rank++;
        }//next item

        return result;
        
    }  
    #endregion
    #region "Helper Methods"
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
