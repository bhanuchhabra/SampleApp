using SalaryContracts;
using SalaryWebApp.Models;
using System.Collections.Generic;
using System.Web.Http;
namespace SalaryWebApp.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IProxyRepo<ITaxableEmployee> _mockRepo;

        public EmployeeController()
        {
            
            if(ProxyRepoStorage.Repo == null)
            {
                ProxyRepoStorage.Repo = new ProxyEmployeeRepository();
            }
            _mockRepo = ProxyRepoStorage.Repo;
        }

        public EmployeeController(IProxyRepo<ITaxableEmployee> repoToInject) : this()
        {
            _mockRepo = repoToInject;
            ProxyRepoStorage.Repo = _mockRepo;
        }

        // GET api/<controller>
        public System.Web.Http.Results.JsonResult<List<ITaxableEmployee>> Get()
        {
            return Json(_mockRepo.Entities);
        }

        // GET api/<controller>/5
        public System.Web.Http.Results.JsonResult<ITaxableEmployee> Get(int id)
        {
            ITaxableEmployee employee = null;
            if (_mockRepo.Entities.Count >= id)
                employee = _mockRepo.Entities[id - 1];
            return Json(employee);
        }

        // POST api/<controller>
        public void Post([FromBody]Employee employee)
        {
            _mockRepo.Entities.Add(employee);
        }
    }
}