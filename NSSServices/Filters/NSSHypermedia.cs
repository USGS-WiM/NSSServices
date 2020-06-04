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
using Microsoft.AspNetCore.Mvc;
using NSSAgent.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WIM.Hypermedia;
using WIM.Services.Filters;
using NSSDB.Resources;
using SharedDB.Resources;


namespace NSSServices.Filters
{
    public class NSSHypermedia : HypermediaBase
    {
        
        protected override List<WIM.Resources.Link> GetEnumeratedHypermedia(IHypermedia entity)
        {
            List<WIM.Resources.Link> results = null;

            // grab path base (nssservices or whatever is alias in IIS)
            var request = UrlHelper.ActionContext.HttpContext.Request;
            string pathBase = request.PathBase.ToUriComponent();

            switch (entity.GetType().Name.ToLower())
            {
                //case "citation":
                //    results = new List<WIM.Resources.Link>();
                //    results.Add(Hyperlinks.Generate(BaseURI, "Citation", UrlHelper.RouteUrl("Citation",((Citation)entity).ID), WIM.Resources.refType.GET));
                //    break;
                case "scenario":
                    results = new List<WIM.Resources.Link>();
                    results.Add(Hyperlinks.Generate("https://" + BaseURI + pathBase,"Citations",$"/citations?regressionregions={String.Join(",", ((Scenario)entity).RegressionRegions.Select(r => r.ID))}",WIM.Resources.refType.GET));
                    break;
                default:
                    break;
            }

            return results;

        }

        protected override List<WIM.Resources.Link> GetReflectedHypermedia(IHypermedia entity)
        {
            List<WIM.Resources.Link> results = null;

            // grab path base (nssservices or whatever is alias in IIS)
            var request = UrlHelper.ActionContext.HttpContext.Request;
            string pathBase = request.PathBase.ToUriComponent();

            switch (entity.GetType().Name.ToLower())
            {
                case "citation":
                    //results = new List<WIM.Resources.Link>();
                    //results.Add(Hyperlinks.Generate(BaseURI, "add new citation", this.URLQuery + "/", WIM.Resources.refType.POST));

                    break;
                case "scenario":
                    results = new List<WIM.Resources.Link>();
                    results.Add(Hyperlinks.Generate("https://" + BaseURI + pathBase, "Citations", $"/citations?regressionregions={String.Join(",", ((Scenario)entity).RegressionRegions.Select(r => r.ID))}", WIM.Resources.refType.GET));
                    break;
                default:
                    break;
            }
            return results;
        }
    }
}