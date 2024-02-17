// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusManagementSystem.Areas.Identity.Pages.Account
{
    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class AccessDeniedModel : PageModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// 
        UserManager<IdentityUser> manager;
        AccessDeniedModel(UserManager<IdentityUser> manager)
        {
            this.manager = manager;
        }
        public async Task OnGetAsync()
        {
            var user = await manager.GetUserAsync(User);
            bool userIsAdmin = await manager.IsInRoleAsync(user, "Admin");

        }
    }
}
