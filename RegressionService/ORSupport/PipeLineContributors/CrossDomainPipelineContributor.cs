using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace WiM.PipeLineContributors
{
    public class CrossDomainPipelineContributor:IPipelineContributor
    {
        public void Initialize(IPipeline pipelineRunner)
        {
            pipelineRunner.Notify(processOptions).Before<KnownStages.IUriMatching>();
        }
        private PipelineContinuation processOptions(ICommunicationContext context)
        {
            addHeaders(context);
            if (context.Request.HttpMethod == "OPTIONS")
            {
                context.Response.StatusCode = 200;
                context.OperationResult = new OperationResult.NoContent();
                return PipelineContinuation.RenderNow;
            }
            return PipelineContinuation.Continue;
        }
        private void addHeaders(ICommunicationContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS, PUT, DELETE");
            context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
        }
    }//end class
}//end namespace