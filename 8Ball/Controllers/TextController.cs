using System.Web.Mvc;
using _8Ball.Common;
using TwilioSharp.MVC3.Controllers;
using TwilioSharp.Request;

namespace _8Ball.Controllers
{
    public class TextController : TwiMLController
    {
        public ActionResult Answer()
        {
            return Json(new { answer = Magic8BallAnswerizer3000.GetAnswer() });
        }

        [HttpPost]
        public ActionResult New(TextRequest request)
        {
            var answer = "The Magical 8-Ball Says: " + Magic8BallAnswerizer3000.GetAnswer();
            
            return TwiML(response => response
                                        .Sms(answer));

            // Alternatively:
            //return TwiMLBuilder
            //            .Build()
            //            .Sms(answer)
            //            .ToTwiMLResult();
        }

    }
}
