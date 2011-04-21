using System;
using System.Web.Mvc;
using System.Web.Routing;
using _8Ball.Models;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace _8Ball.Controllers
{
    public class AccountController : Controller
    {

        public IFormsAuthenticationService FormsService { get; set; }
        public OpenIdRelyingParty OpenId { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (OpenId == null) { OpenId = new OpenIdRelyingParty(); }

            base.Initialize(requestContext);
        }

        public ActionResult LogOn(string ReturnUrl)
        {
            if (!String.IsNullOrWhiteSpace(ReturnUrl))
                Session["ReturnUrl"] = ReturnUrl;

            return View();
        }

        [ValidateInput(false)]
        public ActionResult Authenticate(string returnUrl)
        {
            IAuthenticationResponse response = OpenId.GetResponse();

            if (response == null)
            {
                Identifier id;
                if (Identifier.TryParse(Request.Form["openid_identifier"], out id))
                {
                    try
                    {
                        return OpenId.CreateRequest(id).RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException pex)
                    {
                        ModelState.AddModelError("", pex.Message);
                        return View("LogOn");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Identifier");
                    return View("LogOn");
                }

            }
            else
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:

                        // The claimed Identifier is not very display friendly
                        // In the future we will need to have an option where we 
                        // redirect to a register page and get a friendly display name
                        FormsService.SignIn(response.ClaimedIdentifier, true);

                        // If the returnUrl was lost in the openId dance, try to get it from the Session
                        if (string.IsNullOrWhiteSpace(returnUrl) && Session["ReturnUrl"] != null)
                            returnUrl = Session["ReturnUrl"].ToString();

                        if (!string.IsNullOrWhiteSpace(returnUrl))
                        {
                            // Prevent Open Redirection Attacks 
                            // - Jon Galloway - http://weblogs.asp.net/jgalloway/archive/2011/01/25/preventing-open-redirection-attacks-in-asp-net-mvc.aspx
                            if (Url.IsLocalUrl(returnUrl))
                                return Redirect(returnUrl);
                        }

                        return RedirectToAction("Index", "Home");

                    case AuthenticationStatus.Canceled:
                        ModelState.AddModelError("", "Canceled at provider");
                        return View("Login");

                    case AuthenticationStatus.Failed:
                        ModelState.AddModelError("", response.Exception.Message);
                        return View("Login");
                }
            }

            return View("LogOn");
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

    }
}
