using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using VidyaVahini.Entities.Response;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.WebApi.Controllers
{
    public class SchoolController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly ISchoolService _schoolService;

        public SchoolController(IConfiguration configuration, ISchoolService schoolService)
        {
            _schoolService = schoolService;
            _configuration = configuration;
        }

        [Route("[action]")]
        [HttpPost]
        public Response<SchoolDataUploadModel> AddSchools()
        {
            var acceptedFileTypes = _configuration.GetSection("SchoolDataFileTypes").Get<string[]>();

            var file = HttpContext.Request.Form.Files.Count > 0
                ? HttpContext.Request.Form.Files[0]
                : null;

            if (file == null)
            {
                throw new FileNotFoundException("File not sent to server.");               
            }
            else if (!acceptedFileTypes.Contains(new FileInfo(file.FileName).Extension.Trim()))
            {
                throw new FileLoadException("Please upload supported file formats only (.xlsx, .xls, .csv).");
            }
            else if (file.Length <= 0 || !file.OpenReadStream().CanRead || file.OpenReadStream().Length <= 0)
            {
                throw new FileLoadException("Please upload file that can be readable with data.");
            }
            else
            {
                return GetResponse(_schoolService.AddSchools(file.OpenReadStream()));
            }
        }
    }
}