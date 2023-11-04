using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;
using Talabat.Core.Specifications;

namespace Talabat.Controllers
{

    public class EmployeeController : ApiBaseController
    {
        private readonly IGenericRepository<Employee> empRepo;

        public EmployeeController(IGenericRepository<Employee> EmpRepo)
        {
            empRepo = EmpRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmp() {
            EmployeeSpecification spec = new EmployeeSpecification();
            var Employees = await empRepo.GetAllBySpecificationAsync(spec);
            return Ok(Employees);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmpById(int id)
        {
            EmployeeSpecification spec = new EmployeeSpecification(id);
            var Employee = await empRepo.GetByIdSpecificationAsync(spec);
            return Ok(Employee);

        }


    }
}
