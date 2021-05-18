// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Error Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    /// <summary>
    /// Error Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class ErrorController : Controller
    {
        /// <summary>
        /// HTTPs the status code handler.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <returns>View of status code</returns>
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case (int)HttpStatusCode.NotFound:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found.";
                    return this.View("NotFound");
                default:
                    break;
            };

            return this.View();
        }
    }
}