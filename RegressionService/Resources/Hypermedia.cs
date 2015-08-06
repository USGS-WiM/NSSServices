//------------------------------------------------------------------------------
//----- Link -------------------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2012 WiM - USGS

//    authors:  Jeremy K. Newson USGS Wisconsin Internet Mapping
//              
//  
//   purpose:   A generic link resource for implementing hypermedia. Each link element
//              points to a related resource.
//
//discussion:   Benifits of hypermedia are that it allows for the server to change
//              it's URI scheme without breaking clients, It helps developers explore the protocols, and
//              it allows the server team to advertise new capabilities 
//              http://martinfowler.com/articles/richardsonMaturityModel.html
//
//              Hypermedia provides a means of navigation from one resource to another.
//
//     
#region Comments
// 06.06.12 - JKN - Created
#endregion


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WiM.Resources
{
    public class Link
    {
        #region Properties

        /// <summary>
        /// conveys the semantics of the link, 
        /// such as what resource does the URI refer to,
        /// what is the significance of the link
        /// what kind of action can a client perform on the resource at the URI, and
        /// what are the supported representation formats for requests and responses for that resource
        /// </summary>
        [XmlAttribute("rel")]
        public string Rel { get; set; }

        /// <summary>
        /// Absolute URI, or location of the resource
        /// </summary>
        [XmlAttribute("Href")]
        public string Href { get; set; }

        /// <summary>
        /// Method
        /// </summary>
        [XmlAttribute("method")]
        public string method { get; set; }

        #endregion

        #region Constructors
        
        public Link() { }

        /// <summary>
        /// overloaded construcor   
        /// </summary>
        /// <param name="rel">indicates the type of the link</param>
        /// <param name="href">the link's URI</param>
        public Link(string rel, string href)
        {
            this.Rel = rel;
            this.Href = href;
            this.method = "";


        }//end link

        /// <summary>
        /// overloaded constructor
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="rel"></param>
        /// <param name="href"></param>
        //public Link(string baseUri, string rel, string href)
        //{ 
        //    //insure base uri has /
        //   if (!baseUri[baseUri.Length - 1].Equals('/'))
        //       baseUri = baseUri.Insert(baseUri.Length, "/");

        //    this.Rel = rel;
        //    this.Href = baseUri + href;
        //    //this.method = httpMethod;
        //}// end link

        public Link(string baseUri, string rel, string href, string httpMethod)
        { 
            //insure base uri has /
           if (!baseUri[baseUri.Length - 1].Equals('/'))
               baseUri = baseUri.Insert(baseUri.Length, "/");

            this.Rel = rel;
            this.Href = baseUri + href;
            this.method = httpMethod;
        }// end link

        #endregion

    }//end class Link

    
    public abstract class HypermediaEntity
    {

        #region Fields
        private List<Link> _links = new List<Link>();
        #endregion

        #region Properties
        [XmlArray(ElementName = "LINKS"), XmlArrayItem(ElementName = "LINK")]
        public List<Link> LINKS
        {
            get
            {
                return _links;
            }
        }

        #endregion

        #region Methods

        virtual public void LoadLinks(string baseURI, linkType ltype)
        {
            switch (ltype)
            {
                case linkType.e_group:
                    //self
                    this.LINKS.Add(GetLinkResource(baseURI, "self", refType.GET));
                    this.LINKS.Add(GetLinkResource(baseURI, "add", refType.POST));
               
                    break;

                case linkType.e_individual:

                    //Update
                    this.LINKS.Add(GetLinkResource(baseURI, "update", refType.PUT));
                    //Delete
                    this.LINKS.Add(GetLinkResource(baseURI, "delete", refType.DELETE));

                    //add related object links
                    addRelatedLinks(baseURI);
                    break;

                default:
                    break;
            }



        }//end LoadReferences

        #endregion

        #region Helper Methods
        protected Link GetLinkResource(string baseURI, string relation, refType Method)
        {
            string absoluteURI = getRelativeURI(Method);

            return new Link(baseURI, relation, absoluteURI, Method.ToString());
        }
        protected Link GetLinkResource(string baseURI, string relation, refType Method,string addedResource)
        {
            string absoluteURI = getRelativeURI(Method) + addedResource;

            return new Link(baseURI, relation, absoluteURI, Method.ToString());
        }
        protected Link SetLinkResource(string baseURI, string relation, refType Method, string addedResource)
        {
            string absoluteURI = addedResource;

            return new Link(baseURI, relation, absoluteURI, Method.ToString());
        }
        // cannot make method abstract because class that is inheriting will be partial class
        virtual protected string getRelativeURI(refType rType)
        {
            return string.Empty;
        }

        /// <summary>
        /// adds links to related/associated objects
        /// </summary>
        /// <param name="baseURI"></param>
        virtual protected void addRelatedLinks(string baseURI){}

        #endregion

        public enum refType
        {
            GET,
            PUT,
            POST,
            DELETE
        }
    }//End class ResourceBase

    public enum linkType
    {
        e_individual = 0,
        e_group = 1

    }

}//end namespace