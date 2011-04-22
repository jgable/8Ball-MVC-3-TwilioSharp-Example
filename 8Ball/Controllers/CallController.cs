using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _8Ball.Common;
using TwilioSharp.Request;
using TwilioSharp.MVC3.Controllers;

namespace _8Ball.Controllers
{
    public class CallController : TwiMLController
    {
        [HttpPost]
        public ActionResult New(CallRequest request)
        {
            return TwiML(response => response
                                        .Say("Thanks for calling the All Knowing Magical 8 Ball.")
                                        .Say("Ask a Question after the Beep.")
                                        .Say("Press Pound when done.")
                                        .Record(Url.Action("Question")));
        }

        [HttpPost]
        public ActionResult Question(CallRecordRequest request)
        {
            var answer = "The Magical 8 Ball Says. " + Magic8BallAnswerizer3000.GetAnswer();

            return TwiML(response => response
                                        .Say(answer)
                                        .Pause(3)                                        
                                        .GatherWhileSaying("Press Any Key To Ask Another Question.  Or Pound to Exit.", 
                                            actionUrl: Url.Action("New"),
                                            timeoutSeconds: 7)
                                        .Gather()
                                        .Say("Goodbye")
                                        .Hangup());
        }
    }
}
