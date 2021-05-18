// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BlogController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Blog Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Inventek.CMS.DAL.Repos.Interfaces;
    using Inventek.CMS.DAL.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using System.Net.Http.Headers;
    using Microsoft.AspNetCore.Hosting;
    using System.IO;
    using Microsoft.AspNetCore.Http.Features;

    /// <summary>
    /// Blog Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [AllowAnonymous]
    public class BlogController : Controller
    {
        /// <summary>
        /// The blog repository
        /// </summary>
        private readonly IBlogRepository _blogRepository;

        /// <summary>
        /// The hosting env
        /// </summary>
        private readonly IWebHostEnvironment _hostingEnv;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogController" /> class.
        /// </summary>
        /// <param name="blogRepository">The blog repository.</param>
        /// <param name="env">The env.</param>
        public BlogController(IBlogRepository blogRepository, IWebHostEnvironment env)
        {
            this._blogRepository = blogRepository;
            this._hostingEnv = env;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Index view</returns>
        [HttpGet("[controller]/Page/{p?}")]
        public IActionResult Index(int p = 1)
        {
            var viewModel = new ListViewModel(this._blogRepository, p);
            var model = new WidgetViewModel(this._blogRepository)
            {
                ListViewModel = viewModel
            };

            TempData["currentPage"] = p;
            return this.View(model);
        }

        /// <summary>
        /// Postses the specified p.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>Post view</returns>
        public ViewResult Posts(int p = 1)
        {
            var viewModel = new ListViewModel(this._blogRepository, p);
            ViewBag.Title = "Latest Posts";
            return this.View("List", viewModel);
        }


        public IActionResult GetCategoryList(string category)
        {
            //need to modify this to only return the blogs from that specific category
            var viewModel = new ListViewModel(this._blogRepository, 1);
            var model = new WidgetViewModel(this._blogRepository)
            {
                ListViewModel = viewModel
            };

            TempData["currentPage"] = 1;
            return this.View("Index", model);
        }

        /// <summary>
        /// Categories the specified category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="p">The p.</param>
        /// <returns>Category view</returns>
        /// <exception cref="Exception">Category not found</exception>
        public ViewResult Category(string category, int p = 1)
        {
            var viewModel = new ListViewModel(this._blogRepository, category, "Category", p);
            if (viewModel.Category == null)
            {
                throw new Exception("Category not found");
            }

            ViewBag.Title = String.Format(@"Latest posts on category ""{0}""", viewModel.Category.CategoryName);
            return this.View("List", viewModel);
        }

        /// <summary>
        /// Tags the specified tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="p">The p.</param>
        /// <returns>Tag view</returns>
        /// <exception cref="Exception">Tag not found</exception>
        public ViewResult Tag(string tag, int p = 1)
        {
            var viewModel = new ListViewModel(this._blogRepository, tag, "Tag", p);
            if (viewModel.Tag == null)
            {
                throw new Exception("Tag not found");
            }

            ViewBag.Title = String.Format(@"Latest posts tagged on ""{0}""", viewModel.Tag.TagName);
            return this.View("List", viewModel);
        }

        public ViewResult Search(string s, int p = 1)
        {
            ViewBag.Title = String.Format(@"Lists of posts found for search text ""{0}""", s);
            var viewModel = new ListViewModel(this._blogRepository, s, "Search", p);
            return this.View("List", viewModel);
        }

        /// <summary>
        /// Posts the specified year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="title">The title.</param>
        /// <returns>
        /// Post view
        /// </returns>
        /// <exception cref="Exception">Post not found
        /// or
        /// The post is not published</exception>
        public ViewResult Post(int year, int month, string title)
        {
            var post = this._blogRepository.Post(year, month, title);

            if (post == null)
            {
                throw new Exception("Post not found");
            }

            if (post.Count == 0 && User.Identity.IsAuthenticated == false)
            {
                throw new Exception("The post is not published");
            }

            return this.View(post);
        }

        /// <summary>
        /// Gets the post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Post View fo Id</returns>
        [Route("[controller]/[action]/{id}")]
        public IActionResult Post(int id)
        {
            var result = this._blogRepository.GetPost(id);
            ViewBag.PostsFilter = "General";
            return this.View("Post", result);
        }

        /// <summary>
        /// Sidebarses this instance.
        /// </summary>
        /// <returns>Sidebars partial view</returns>
        public PartialViewResult Sidebars()
        {
            var widgetViewModel = new WidgetViewModel(this._blogRepository);
            return this.PartialView("_Sidebars", widgetViewModel);
        }

        ////Backend section
        
        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="UploadFiles">The upload files.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ActionResult SaveFile(IList<IFormFile> UploadFiles)
        {
            try
            {
                foreach (IFormFile file in UploadFiles)
                {
                    if (UploadFiles != null)
                    {
                        string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        filename = this._hostingEnv.WebRootPath + "\\img" + $@"\{filename}";
                        if (!System.IO.File.Exists(filename))
                        {
                            using FileStream fs = System.IO.File.Create(filename);
                            file.CopyTo(fs);
                            fs.Flush();
                            fs.Dispose();
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
            catch (Exception ex)
            {
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.StatusCode = 204;
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "No Content";
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = ex.Message;
            }

            return Content(string.Empty);
        }

        /// <summary>
        /// Uploads the file. (Upload method for chunk-upload and normal upload)
        /// </summary>
        /// <param name="chunkFile">The chunk file.</param>
        /// <param name="UploadFiles">The upload files.</param>
        [Authorize(Roles = "ADMIN")]
        public void UploadFile(IList<IFormFile> chunkFile, IList<IFormFile> UploadFiles)
        {
            long size = 0;
            try
            {
                //// for chunk-upload
                foreach (var file in chunkFile)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    filename = this._hostingEnv.WebRootPath + "\\img" + $@"\{filename}";
                    size += file.Length;

                    if (!System.IO.File.Exists(filename))
                    {
                        using FileStream fs = System.IO.File.Create(filename);
                        file.CopyTo(fs);
                        fs.Flush();
                        fs.Dispose();
                    }
                    else
                    {
                        using FileStream fs = System.IO.File.Open(filename, FileMode.Append);
                        file.CopyTo(fs);
                        fs.Flush();
                        fs.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Clear();
                Response.StatusCode = 204;
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "File failed to upload";
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = ex.Message;
            }

            //// for normal upload
            try
            {
                foreach (var file in UploadFiles)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    filename = this._hostingEnv.WebRootPath + "\\img" + $@"\{filename}";
                    size += file.Length;
                    if (!System.IO.File.Exists(filename))
                    {
                        using FileStream fs = System.IO.File.Create(filename);
                        file.CopyTo(fs);
                        fs.Flush();
                        fs.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Clear();
                Response.StatusCode = 204;
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "File failed to upload";
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = ex.Message;
            }
        }

        /// <summary>
        /// Removes the specified upload files. (to delete uploaded chunk-file from server)
        /// </summary>
        /// <param name="UploadFiles">The upload files.</param>
        [Authorize(Roles = "ADMIN")]
        public void Remove(IList<IFormFile> UploadFiles)
        {
            try
            {
                var filename = this._hostingEnv.WebRootPath + "\\img" + $@"\{UploadFiles[0].FileName}";
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