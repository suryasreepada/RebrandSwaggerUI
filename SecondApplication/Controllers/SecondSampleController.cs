using System;
using Microsoft.AspNetCore.Mvc;
using RebrandSwaggerUI.Models;

namespace SecondApplication.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    [Route("v{version:apiVersion}")]
    public class SecondSampleController : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetUser")]
        public ActionResult<UserModel> GetUserV1()
        {
            return Ok(new UserModel { Name = "Second UserName", UserId = "v1" });
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        [Route("GetUser")]
        public ActionResult<UserModelV2> GetUserV2()
        {
            return Ok(new UserModelV2 { Name = "Second UserName", UserId = "v2", StartDate = DateTime.Now });
        }
    }
}
