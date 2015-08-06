using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace WiM.PipeLineContributors
{
    public class ErrorCheckingContributor:IPipelineContributor
    {
        public void Initialize(IPipeline pipelineRunner)
        {
            pipelineRunner
                .Notify(CheckRequestDecoding)
                .After<KnownStages.IOperationResultInvocation>()
                .And.Before<KnownStages.ICodecResponseSelection>();
        }

        private static PipelineContinuation CheckRequestDecoding(ICommunicationContext context)
        {
            if (context.ServerErrors.Count == 0)
            {
                return PipelineContinuation.Continue;
            }

            var first = context.ServerErrors[0];
         
                context.Response.Entity.ContentType = MediaType.TextPlain;
                context.Response.Entity.ContentLength = first.Exception.Message.Length;
                using (var sw = new StreamWriter(context.Response.Entity.Stream))
                {
                    sw.Write(first.Exception.Message);
                }

            return PipelineContinuation.Continue;
        } 

    }
}