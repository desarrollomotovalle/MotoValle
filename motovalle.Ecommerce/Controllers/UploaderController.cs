// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UploaderController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Uploader Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers
{
    using Inventek.ERP.DAL.EF;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http.Headers;

    /// <summary>
    /// Uploader Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize]
    public class UploaderController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly inventekContext _context;

        /// <summary>
        /// The hosting env
        /// </summary>
        private readonly IWebHostEnvironment hostingEnv;

        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UploaderController(IWebHostEnvironment env, inventekContext context, IHttpContextAccessor httpContextAccessor)
        {
            hostingEnv = env;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Index View</returns>
        public IActionResult Index()
        {
            return View(_context.EcommerceCustomPages.ToList());
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="UploadFiles">The upload files.</param>
        /// <returns>Save File</returns>
        [AcceptVerbs("Post")]
        public ActionResult SaveFile(IList<IFormFile> UploadFiles)
        {
            try
            {
                string shortFileName;
                foreach (IFormFile file in UploadFiles)
                {
                    if (UploadFiles != null)
                    {
                        string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        shortFileName = "\\files" + $@"\{filename}";
                        filename = hostingEnv.WebRootPath + "\\files" + $@"\{filename}";

                        if (!Directory.Exists(Path.GetDirectoryName(filename)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(filename));
                        }

                        if (!System.IO.File.Exists(filename))
                        {
                            using (FileStream fs = System.IO.File.Create(filename))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                        else
                        {
                            Response.Clear();
                            Response.StatusCode = 204;
                            Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "File already exists.";
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.StatusCode = 204;
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "No Content";
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = e.Message;
            }
            return Content("");
        }

        // to delete uploaded chunk-file from server
        /// <summary>
        /// Removes the file.
        /// </summary>
        /// <param name="UploadFiles">The upload files.</param>
        public void RemoveFile(IList<IFormFile> UploadFiles)
        {
            string picDir = "\\files";

            try
            {
                var filename = hostingEnv.WebRootPath + $@"\{picDir}" + $@"\{UploadFiles[0].FileName}";
                if (System.IO.File.Exists(filename))
                {
                    System.IO.File.Delete(filename);
                }
            }
            catch (Exception e)
            {
                Response.Clear();
                Response.StatusCode = 200;
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "File removed successfully";
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = e.Message;
            }
        }

    }
}
