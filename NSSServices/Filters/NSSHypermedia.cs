//------------------------------------------------------------------------------
//----- NavigationHypermedia ---------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2017 WiM - USGS

//    authors:  Jeremy K. Newson USGS Web Informatics and Mapping
//              
//  
//   purpose:   Intersects the pipeline after
//
//discussion:   Controllers are objects which handle all interaction with resources. 
//              
//
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WIM.Hypermedia;
using WIM.Services.Filters;

namespace NSSServices.Filters
{
    public class NSSHypermedia : HypermediaBase
    {
        protected override List<Link> GetEnumeratedHypermedia(IHypermedia entity)
        {
            List<Link> results = null;
            switch (entity.GetType().Name.ToLower())
            {
                case "citation":
                    results = new List<Link>();
                    results.Add(new Link(BaseURI, "self by id", this.URLQuery + "/", WIM.Resources.refType.GET));
                    break;

                default:
                    break;
            }

            return results;

        }

        protected override List<Link> GetReflectedHypermedia(IHypermedia entity)
        {
            List<Link> results = null;
            switch (entity.GetType().Name.ToLower())
            {
                case "citation":
                    results = new List<Link>();
                    results.Add(new Link(BaseURI, "add new citation", this.URLQuery + "/", WIM.Resources.refType.POST));

                    break;
                default:
                    break;
            }

            return results;
        }

        //protected override void Load(object obj)
        //{
        //    if(obj.GetType().IsGenericType && obj.GetType().GetInterfaces().Any(t => t.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
        //        //this is not loading the hypermedia /shrug.
        //        ((IEnumerable<IHypermedia>)obj).ToList().ForEach(e => e.Links = GetEnumeratedHypermedia(e));
        //    else
        //        ((IHypermedia)obj).Links = GetReflectedHypermedia((IHypermedia)obj);

            
        //}
    }
}