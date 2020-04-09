using System.Collections.Generic;
using System.Linq;

using IteaLinqToSql.Models.Entities;
using IteaLinqToSql.Models.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IteaLinqToSql.Controllers
{
    [Route("api/loginhistory")]
    [ApiController]
    public class LoginHistoryController : ControllerBase
    {
        readonly IService<LoginHistory> service;

        public LoginHistoryController(IService<LoginHistory> service)
        {
            this.service = service;
        }

        [HttpGet]
        public List<LoginHistory> Get()
        {
            return service
                .GetQuery()
                .Include(x => x.IPAddress)
                .Where(x => x.IPAddress != null)
                .ToList();
        }

        [HttpGet("{id}")]
        public LoginHistory Get(int id)
        {
            return service.FindById(id);
        }

        [HttpPost("save")]
        public List<LoginHistory> Post([FromBody] LoginHistory value)
        {
            return service
                .GetAll()
                .Where(x => x.IPAddress.Contains(value.IPAddress) ||
                            x.UserDevice.Contains(value.UserDevice) ||
                            x.Id == value.Id)
                .ToList();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            //service.Create();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Delete(service.FindById(id));              
                
        }
    }
}
