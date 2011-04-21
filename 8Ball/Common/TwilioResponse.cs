using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace _8Ball.Common
{
    public class ReplySMSResult : ActionResult
    {
        public string Text { get; set; }

        public ReplySMSResult()
            : this(string.Empty)
        { }

        public ReplySMSResult(string text)
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

    public class AskForQuestionResult : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {

            // Example
            //<Response>
            //    <Say>
            //        Please leave a message at the beep. 
            //        Press the star key when finished. 
            //    </Say>
            //    <Record
            //        action="http://foo.edu/handleRecording.php"
            //        method="GET"
            //        maxLength="20"
            //        finishOnKey="*"
            //        />
            //    <Say>I did not receive a recording</Say>
            //</Response>

            var root = new XElement("Response");
            var say = new XElement("Say") { Value = "Ask the all knowing 8 Ball a question after the beep.  Press pound when you are done." };

            var actionAttrib = new XAttribute("action", "http://8ball.apphb.com/Call/Question");
            var methodAttrib = new XAttribute("method", "POST");
            var maxLenAttrib = new XAttribute("maxLength", "10");
            var finishAttrib = new XAttribute("finishOnKey", "#");

            var record = new XElement("Record");
            record.Add(actionAttrib);
            record.Add(methodAttrib);
            record.Add(maxLenAttrib);
            record.Add(finishAttrib);

            var say2 = new XElement("Say") { Value = "I did not receive a question" };

            root.Add(say);
            root.Add(record);
            root.Add(say2);

            var xs = new XmlSerializer(root.GetType());
            context.HttpContext.Response.ContentType = "text/xml";

            xs.Serialize(context.HttpContext.Response.Output, root);
        }
    }

    public class AnswerQuestionResult : ActionResult
    {
        public string Text { get; set; }

        public AnswerQuestionResult()
            : this(string.Empty)
        { }

        public AnswerQuestionResult(string text)
        {
            this.Text = text;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (String.IsNullOrWhiteSpace(Text))
                throw new ArgumentNullException("Text");

            var root = new XElement("Response");
            var say = new XElement("Say") { Value = Text };

            root.Add(say);

            var xs = new XmlSerializer(root.GetType());
            context.HttpContext.Response.ContentType = "text/xml";

            xs.Serialize(context.HttpContext.Response.Output, root);
        }
    }


}