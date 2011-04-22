using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwilioSharp.MVC3.Results;
using System.Web.Mvc;
using TwilioSharp.TwiML;

namespace TwilioSharp.MVC3.Controllers
{
    public class TwiMLController : Controller
    {
        protected TwiMLResult TwiML(TwiMLBuilder response)
        {
            return new TwiMLResult(response);
        }

        protected TwiMLResult TwiML(Func<TwiMLBuilder, TwiMLBuilder> responseFactory)
        {
            return new TwiMLResult(responseFactory(TwiMLBuilder.Build()));
        }
    }
}
