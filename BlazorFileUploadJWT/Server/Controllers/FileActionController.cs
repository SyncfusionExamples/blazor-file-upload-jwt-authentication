using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;


namespace BlazorFileUploadJWT.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileActionController : Controller
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnv;
        public IActionResult Index()
        {
            return View();
        }
        public FileActionController(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }

        [HttpPost("[action]")]
        public async void Save(IList<IFormFile> UploadFiles)
        {
            //to get authorization Header to handle save file on server side  
            var authorizationHeader = Request.Headers["Authorization"];
            if (authorizationHeader.Count == 0 || authorizationHeader[0] != "test123")
            {
                Response.Clear();
                Response.StatusCode = 401;
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Unauthorized";
                return;
            }
            try
            {
                foreach (var file in UploadFiles)
                {
                    if (UploadFiles != null)
                    {
                        var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        filename = hostingEnv.WebRootPath + $@"\{filename}";
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
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "File failed to upload";
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = e.Message;
            }

        }
        [HttpPost("[action]")]
        public void Remove(IList<IFormFile> UploadFiles)
        {
            //to get authorization Header to handle save file on server side  
            var authorizationHeader = Request.Headers["Authorization"];
            if (authorizationHeader.Count == 0 || authorizationHeader[0] != "test123")
            {
                Response.Clear();
                Response.StatusCode = 401;
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Unauthorized";
                return;
            }
            try
            {
                var filename = hostingEnv.ContentRootPath + $@"\{UploadFiles[0].FileName}";
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
