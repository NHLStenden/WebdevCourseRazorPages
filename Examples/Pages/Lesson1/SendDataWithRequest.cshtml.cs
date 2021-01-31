using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examples.Pages.Lesson1
{
    public class PostRequestWithParameterBinding : PageModel
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }

        [BindProperty]
        public string Nickname { get; set; }


        [BindProperty(SupportsGet = true)] public string NicknameGet { get; set; }

        public void OnGet(string surname)
        {
            if (Request.Query.ContainsKey("firstname") && !string.IsNullOrWhiteSpace(Request.Query["firstname"]))
            {
                Firstname = Request.Query["firstname"].First().ToUpper();
            }

            Surname = surname;
        }

        public void OnPost(string surname)
        {
            if (Request.Form.ContainsKey("firstname") && !string.IsNullOrWhiteSpace(Request.Form["firstname"]))
            {
                Firstname = Request.Form["firstname"].First().ToUpper();
            }

            Surname = surname;
        }
    }
}
