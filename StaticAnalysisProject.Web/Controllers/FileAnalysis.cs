using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaticAnalysisProject.Helpers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StaticAnalysisProject.Web.Controllers
{
    public class FileAnalysis : Controller
    {
        // GET: /<controller>/
        [HttpPost("FileUpload")]
        public async Task<IActionResult> Index(IFormFile file)
        {
            if (file != null)
            {
                string path = Path.Combine(Path.GetTempPath(), "test-files");
                long size = file.Length;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filePath = Path.Combine(path, file.FileName);
                if (file.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                //string sha256 = new StaticAnalysisProject.Helpers.Hash.SHA256().GetHash(filePath);
                IFileReport report = new FileReport(filePath);
                string json = report.ToJson();

                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                return Content(json);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Ok("{\"error\"}");
        }
    }
}
