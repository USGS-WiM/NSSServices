using System;
using System.Linq;
using OpenRasta.Codecs;
using OpenRasta.TypeSystem;
using OpenRasta.Web.UriDecorators;
using OpenRasta.Web;

namespace WiM.URIDecorators
{
    public class WiMExtensionURIDecorator : IUriDecorator
    {
        readonly ICodecRepository _codecs;
        readonly ICommunicationContext _context;
        readonly IUriResolver _uris;
        UriRegistration _resourceMatch;
        CodecRegistration _selectedCodec;

        public WiMExtensionURIDecorator(ICommunicationContext context, IUriResolver uris, ICodecRepository codecs, ITypeSystem typeSystem)
        {
            _context = context;
            _codecs = codecs;
            _uris = uris;
        }

        public void Apply()
        {
            // other decorators may change the url later on and the match will have the wrong values
            // the content type however shouldn't change
            var entity = _context.Response.Entity;
            _context.PipelineData.ResponseCodec = _selectedCodec;

            // TODO: Check if this still works. 
            entity.ContentType = _selectedCodec.MediaType;
        }

        public bool Parse(Uri uri, out Uri processedUri)
        {
            processedUri = null;

            var appBaseUri = _context.ApplicationBaseUri.EnsureHasTrailingSlash();
            var fakeBaseUri = new Uri("http://localhost/", UriKind.Absolute);

            var uriRelativeToAppBase = appBaseUri
                .MakeRelativeUri(uri)
                .MakeAbsolute(fakeBaseUri);

            // find the resource type for the uri
            string lastUriSegment = GetSegments(uriRelativeToAppBase)[GetSegments(uriRelativeToAppBase).Length - 1];
            
            int lastDot = lastUriSegment.LastIndexOf(".");

            if (lastDot == -1)
            {
                return false;
            }

            var uriWithoutExtension = ChangePath(new Uri(lastUriSegment), 
                                                 srcUri =>
                                                 srcUri.AbsoluteUri.Substring(0, srcUri.AbsoluteUri.LastIndexOf(".")));

            _resourceMatch = _uris.Match(uriWithoutExtension);
            if (_resourceMatch == null)
                return false;

            string potentialExtension = lastUriSegment.Substring(lastDot + 1);

// _codecs.
            _selectedCodec = _codecs.FindByExtension(_resourceMatch.ResourceKey as IType, potentialExtension);

            if (_selectedCodec == null)
            {
                return false;
            }

            processedUri = fakeBaseUri.MakeRelativeUri(uriWithoutExtension)
                .MakeAbsolute(appBaseUri);

// TODO: Ensure that if the Accept: is not compatible with the overriden value a 406 is returned.
            return true;
        }

        Uri ChangePath(Uri uri, Func<Uri, string> getPath)
        {
            var builder = new UriBuilder(uri);
            builder.Path = getPath(uri);
            return builder.Uri;
        }

        public string[] GetSegments(Uri uri)
        {
            var result = uri.AbsoluteUri.Split('/').Where(s => s != string.Empty).ToArray();
            return result;
        }
    }//end class
}//end namespace

