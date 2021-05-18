using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace motovalle.Ecommerce.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LockoutModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }


        public class InputModel
        {
            public DateTime LockoutEnd { get; set; }
        }

        public void OnGet(DateTime lockoutEnd)
        {
            Input = new InputModel
            {
                LockoutEnd = lockoutEnd
            };
        }
    }
}
