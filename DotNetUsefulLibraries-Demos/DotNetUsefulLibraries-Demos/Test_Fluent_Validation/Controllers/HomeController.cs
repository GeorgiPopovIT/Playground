using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test_Fluent_Validation.Models;
using Test_Fluent_Validation.Validations;

namespace Test_Fluent_Validation.Controllers
{
    //C#12 new feature - primary contructor
    public class HomeController(IValidator<Person> _validator) : Controller
    {
        private IValidator<Person> _validator = _validator;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Privacy(Person person)
        {
            ValidationResult result = await _validator.ValidateAsync(person);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View("Privacy", person);
            }


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
