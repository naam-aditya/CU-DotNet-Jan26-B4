using Microsoft.AspNetCore.Mvc;
using AccountService.Services;
using AccountService.Dtos;
using AccountService.Exceptions;

namespace AccountService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        // GET: api/Account
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAccountResponse>>> GetAccount()
        {
            var accounts = await _service.GetAllccounts();
            return Ok(accounts);
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetAccountResponse>> GetAccount(int id)
        {
            try
            {
                var account = await _service.GetAccountById(id);
                return account;
            }
            catch (BadRequestException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT: api/Account/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/deposit")]
        public async Task<IActionResult> DepositAmount(int id, Transaction transaction)
        {
            if (id != transaction.AccountId)
            {
                return BadRequest();
            }

            try
            {
                await _service.Deposit(transaction);
            }
            catch (BadRequestException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPut("{id}/withdraw")]
        public async Task<IActionResult> WithdrawAmount(int id, Transaction transaction)
        {
            if (id != transaction.AccountId)
            {
                return BadRequest();
            }

            try
            {
                await _service.Withdraw(transaction);
            }
            catch (BadRequestException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Account/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetAccountResponse>> PostAccount(CreateAccountRequest request)
        {
            try
            {
                var response = await _service.AddAccount(request);
                return CreatedAtAction("GetAccount", new { id = response.Id }, response);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            try
            {
                await _service.RemoveAccountById(id);
                return NoContent();
            }
            catch (BadRequestException ex)
            {
                return NotFound(new { ex.Message });
            }
        }
    }
}
