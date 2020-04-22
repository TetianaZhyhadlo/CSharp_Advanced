using System.Collections.Generic;
using System.Linq;

using IteaLinqToSql.Models.Entities;
using IteaLinqToSql.Models.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IteaLinqToSql.Controllers
{
    [Route("api/apps")]
    [ApiController]
    public class AppsController : ControllerBase
    {
        public List<Apps> List;
        public AppsController(List<Apps> list)
        {
            List = list;
        }

        [HttpGet]
        public List<Apps> GetList()
        {
            return List;
                
        }

    }
}
