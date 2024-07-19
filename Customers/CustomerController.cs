using Microsoft.AspNetCore.Mvc;

using MyApi.Business.CustomerServ;
using MyApi.Customers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApi.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }



        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            var newCustomer = await _customerService.PostCustomer(customer);
            if (newCustomer == null)
            {
                return Conflict(new { message = "Kullan�c� ad� daha �nce al�nm��!" });
            }
            return CreatedAtAction("GetCustomer", new { id = newCustomer.CustomerId }, newCustomer);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

      

    }
}