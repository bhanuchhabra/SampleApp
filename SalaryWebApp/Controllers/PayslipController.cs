using SalaryBuinessLayer;
using SalaryContracts;
using SalaryWebApp.Models;
using System.Linq;
using System.Web.Http;

namespace SalaryWebApp.Controllers
{
    public class PayslipController : ApiController
    {
        private readonly int FINANCIAL_YEAR = 2017;
        private readonly TaxedPayslipGenerator taxedPayslipGenerator;
        private readonly IProxyRepo<ITaxableEmployee> _mockRepo;
        public PayslipController()
        {
            if (taxedPayslipGenerator == null)
                taxedPayslipGenerator = new TaxedPayslipGenerator();

            if (ProxyRepoStorage.Repo == null)
            {
                ProxyRepoStorage.Repo = new ProxyEmployeeRepository();
            }
            _mockRepo = ProxyRepoStorage.Repo;
        }

        /// <summary>
        /// Constructor for Testing
        /// </summary>
        /// <param name="repoToInject"></param>
        public PayslipController(IProxyRepo<ITaxableEmployee> repoToInject) : this()
        {
            _mockRepo = repoToInject;
            ProxyRepoStorage.Repo = _mockRepo;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            return Json(new EmptyResult() { Message = "user api/Payslip/id to get payslip of Employee" });
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {

            if (_mockRepo.Entities.Count() >= id && _mockRepo.Entities.Select(ent => _mockRepo.Entities.IndexOf(ent)).Contains(id))
            {
                taxedPayslipGenerator.SetTaxStrategy(new TaxStrategy2017(FINANCIAL_YEAR));

                return Json(taxedPayslipGenerator.GeneratePayslip(_mockRepo.Entities[id]));
            }
            else
            {
                return Json(new EmptyResult { Message = "No such employee" });
            }
        }
    }
}