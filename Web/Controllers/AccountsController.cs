using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.Controllers
{
    [Route("[controller]")]
    public class AccountsController : Controller
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("add")]
        public IActionResult GetAdd()
        {
            return View("Update", new Accounts.ResultModels.AccountResult());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync([FromRoute] Accounts.Commands.GetAccountsQuery query)
        {
            var result = await _mediator.Send(query);
            return View("Update", result.First());
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(Accounts.Commands.UpdateAccountCommand command)
        {
            var result = await _mediator.Send(command);

            return View("Update", result);
        }

        [HttpGet("delete/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Accounts.Commands.DeleteAccountCommand command)
        {
            await _mediator.Send(command);

            return RedirectToAction("index", "dashboard");
        }

        [HttpPost("{id:int}/import")]
        public async Task<IActionResult> ImportAsync(int id, IFormFile file)
        {
            await _mediator.Send(new Transactions.Commands.ImportCsvCommand
            {
                Account = id,
                Content = file.OpenReadStream(),
            });

            return RedirectToAction("index", "transactions", new
            {
                AccountId = id,
            });
        }

        [HttpPost("{id:int}/match")]
        public async Task<IActionResult> MatchAsync(int id, IFormFile file)
        {
            var matches = new Models.MatchesModel
            {
                Account = id,
                Transactions = await _mediator.Send(new Transactions.Commands.MatchCommand
                {
                    Account = id,
                    Content = file.OpenReadStream(),
                })
            };

            return View(matches);
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmAsync(Transactions.Commands.ImportMatchesCommand command)
        {
            await _mediator.Send(command);

            return RedirectToAction("index", "transactions", new
            {
                AccountId = command.Account,
            });
        }
    }
}