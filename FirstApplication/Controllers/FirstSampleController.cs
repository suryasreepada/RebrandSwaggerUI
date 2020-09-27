using System;
using Microsoft.AspNetCore.Mvc;
using RebrandSwaggerUI.Models;

namespace FirstApplication.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    [Route("v{version:apiVersion}")]
    public class FirstSampleController : ControllerBase
    {
        public FirstSampleController()
        {

        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetUser")]
        public ActionResult<UserModel> GetUserV1()
        {
            return Ok(new UserModel { Name = "First UserName", UserId = "v1" });
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        [Route("GetUser")]
        public ActionResult<UserModelV2> GetUserV2()
        {
            return Ok(new UserModelV2 { Name = "First UserName", UserId = "v2", StartDate = DateTime.Now });
        }
    }
}
