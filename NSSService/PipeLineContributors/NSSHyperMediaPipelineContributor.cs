using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiM.Hypermedia;
using WiM.PipeLineContributors;
using NSSDB;
using WiM.Resources;
using NSSService.Resources;

namespace NSSService.PipeLineContributors
{
    public class NSSHyperMediaPipelineContributor:HypermediaPipelineContributor
    {
        protected override List<Link> GetReflectedHypermedia(IHypermedia entity)
        {
            List<Link> results = null;
            switch (entity.GetType().Name)
            {
                case "Region":
                    results = new List<Link>();
                    results.Add(new Link(BaseURI, "citations", String.Format(Configuration.regionResource + "/{0}/" + Configuration.citationResource,((Region)entity).ID), refType.GET));
                    results.Add(new Link(BaseURI, "regressiontypes", String.Format(Configuration.regionResource + "/{0}/" + Configuration.regressionTypeResource, ((Region)entity).ID), refType.GET));
                    results.Add(new Link(BaseURI, "statisticgroups", String.Format(Configuration.regionResource + "/{0}/" + Configuration.statisticGroupTypeResource, ((Region)entity).ID), refType.GET));
                    results.Add(new Link(BaseURI, "scenarios", String.Format(Configuration.regionResource + "/{0}/" + Configuration.scenarioResource, ((Region)entity).ID), refType.GET));
                    results.Add(new Link(BaseURI, "regressionregions", String.Format(Configuration.regionResource + "/{0}/" + Configuration.RegressionRegionResource, ((Region)entity).ID), refType.GET));
                    break;

                case "RegressionRegion":
                    results = new List<Link>();
                    results.Add(new Link(BaseURI, "citations", String.Format(Configuration.citationResource+"/{0}",((RegressionRegion)entity).CitationID), refType.GET));
                    break;


                default:
                    break;
            }

            return results;
        }
        protected override List<Link> GetEnumeratedHypermedia(IHypermedia entity)
        {
            List<Link> results = null;
            switch (entity.GetType().Name)
            {
                case "Region":
                    results = new List<Link>();
                    results.Add(new Link(BaseURI, "self", Configuration.regionResource + "/" + ((Region)entity).ID, refType.GET));
                    results.Add(new Link(BaseURI, "self", Configuration.regionResource + "/" + ((Region)entity).Code, refType.GET));                    
                    break;
                case "RegressionRegion":
                    results = new List<Link>();
                    results.Add(new Link(BaseURI, "self", Configuration.RegressionRegionResource + "/" + ((RegressionRegion)entity).ID, refType.GET)); 
                    break;
                case "Citation":
                    results = new List<Link>();
                    results.Add(new Link(BaseURI, "self", Configuration.citationResource + "/" + ((Citation)entity).ID, refType.GET));
                    break;
                case "StatisticGroupType":
                    results = new List<Link>();
                    results.Add(new Link(BaseURI, "self", Configuration.statisticGroupTypeResource + "/" + ((StatisticGroupType)entity).ID, refType.GET));
                    break;
                case "Scenario":
                    results = new List<Link>();
                    results.Add(new Link(BaseURI, "citations", Configuration.citationResource + "?" + Configuration.RegressionRegionResource+"=" + String.Join(",",((Scenario)entity).RegressionRegions.Select(r=>r.ID)), refType.GET));
                    
                    break;
                default:
                    break;
            }

            return results;
        }


        
    }//end class
}//end namespace
