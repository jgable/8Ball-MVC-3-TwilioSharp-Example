using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _8Ball.Common;

namespace _8Ball.Controllers
{
    public class TwilioRequest
    {
        public string SmsSid { get; set; }
        public string AccountSid { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        public string FromCity { get; set; }
        public string FromState { get; set; }
        public string FromZip { get; set; }
        public string FromCountry { get; set; }
        public string ToCity { get; set; }
        public string ToState { get; set; }
        public string ToZip { get; set; }
        public string ToCountry { get; set; }
    }

    public class Magic8BallAnswerer 
    {
        private static Random _random = new Random();
        private static List<string> _answers = new List<string>
        {
            "It is certain",
            "It is decidedly so",
            "Without a doubt",
            "Yes – definitely",
            "You may rely on it",
            "As I see it, yes",
            "Most likely",
            "Outlook good",
            "Signs point to yes",
            "Yes",
            "Reply hazy, try again",
            "Ask again later",
            "Better not tell you now",
            "Cannot predict now",
            "Concentrate and ask again",
            "Don't count on it",
            "My reply is no",
            "My sources say no",
            "Outlook not so good",
            "Very doubtful",
        };
        
        public static string GetAnswer()
        {
            return _answers[_random.Next(0, _answers.Count)];
        }
    }

    public class TextController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult New(string SmsSid,
        // string AccountSid,
        // string From,
        // string To,
        // string Body,
        // string FromCity,
        // string FromState,
        // string FromZip,
        // string FromCountry,
        // string ToCity,
        // string ToState,
        // string ToZip,
        // string ToCountry)
        //{
        //    return new ReplySMSAction(Magic8BallAnswerer.GetAnswer());
        //}

        public ActionResult Answer()
        {
            return new ReplySMSAction(Magic8BallAnswerer.GetAnswer());
        }

        [HttpPost]
        public ActionResult New(TwilioRequest request)
        {
            var msg = request == null ? "null req" : request.From == null ? "null from" : request.From + ", " + request.FromCity;
            return new ReplySMSAction(Magic8BallAnswerer.GetAnswer() + " - " + msg);
        }

    }
}
