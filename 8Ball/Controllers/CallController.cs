using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _8Ball.Common;

namespace _8Ball.Controllers
{
    public class TwilioCallRequest
    {
        public string CallSid { get; set; }
        public string AccountSid { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string CallStatus { get; set; }
        public string ApiVersion { get; set; }
        public string Direction { get; set; }
        public string ForwardedFrom { get; set; }

        public string FromCity { get; set; }
        public string FromState { get; set; }
        public string FromZip { get; set; }
        public string FromCountry { get; set; }
        public string ToCity { get; set; }
        public string ToState { get; set; }
        public string ToZip { get; set; }
        public string ToCountry { get; set; }
    }

    public class CallController : Controller
    {
        [HttpPost]
        public ActionResult New(TwilioCallRequest request)
        {
            return new AskForQuestionResult();
        }

        [HttpPost]
        public ActionResult Question(TwilioCallRequest request)
        {
            return new AnswerQuestionResult("The Magical 8 Ball Says. " + Magic8BallAnswerer.GetAnswer());
        }
    }
}
