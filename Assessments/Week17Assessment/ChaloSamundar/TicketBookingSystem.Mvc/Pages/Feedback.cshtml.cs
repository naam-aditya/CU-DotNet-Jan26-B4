using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketBookingSystem.Mvc.Services;
using TicketBookingSystem.Mvc.ViewModels;

namespace TicketBookingSystem.Mvc.Pages
{
    public class FeedbackModel : PageModel
    {
        private readonly FeedbackService _service;
        public string SuccessMessage { get; set; } = string.Empty;

        public FeedbackModel(FeedbackService service)
        {
            _service = service;
        }

        [BindProperty]
        public FeedbackViewmodel Feedback { get; set; } = new FeedbackViewmodel();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {

            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"{state.Key}: {error.ErrorMessage}");
                }
            }

            if (!ModelState.IsValid)
                return Page();

            await _service.CreateAsync(Feedback);

            SuccessMessage = "Feedback submitted successfully!";
            ModelState.Clear();

            return Page();
        }
    }
}
