using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace _8Ball.Common
{
    public class ReplySMSAction : ActionResult
    {
        public string Text { get; set; }

        public ReplySMSAction()
            : this(string.Empty)
        { }

        public ReplySMSAction(string text)
        {
            this.Text = text;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (String.IsNullOrWhiteSpace(Text))
                throw new ArgumentNullException("Text");

            var root = new XElement("Response");
            var sms = new XElement("Sms") { Value = Text };
            root.Add(sms);

            var xs = new XmlSerializer(root.GetType());
            context.HttpContext.Response.ContentType = "text/xml";

            xs.Serialize(context.HttpContext.Response.Output, root);
        }
    }

}