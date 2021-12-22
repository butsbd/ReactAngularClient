using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace GSPClient.Controllers
{
    public class Fallback : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Fallback(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            string requestPath = _httpContextAccessor.HttpContext.Request.Path;
            string folderName = requestPath.Split('/')[1];
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", folderName)))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", folderName, "static", "css"));
                string[] cssfilePaths = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "static","css"));
                foreach (var filename in cssfilePaths)
                {
                    string file = filename.ToString();
                    string str = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, "static", "css", Path.GetFileName(file));
                    if (!System.IO.File.Exists(str))
                    {
                        System.IO.File.Copy(file, str);
                    }

                }
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, "static", "js"));
                string[] jsfilePaths = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "static", "js"));
                foreach (var filename in jsfilePaths)
                {
                    string file = filename.ToString();
                    string str = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, "static", "js", Path.GetFileName(file));
                    if (!System.IO.File.Exists(str))
                    {
                        System.IO.File.Copy(file, str);
                    }

                }
            }
            
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "index.html"), "text/HTML");
        }
    }
}