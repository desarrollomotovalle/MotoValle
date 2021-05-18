// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationRole.cs" company="Innova Marketing Systems S.A.S">
//    © All rights reserved
// </copyright>
// <summary>
//   Identity context extended using ApplicationUser and Application role
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.Entities.Identity.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Identity context extended using ApplicationUser and Application role and set PK like int
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext{motovalle.Ecommerce.Models.Entities.Identity.ApplicationUser, motovalle.Ecommerce.Models.Entities.Identity.ApplicationRole, System.Int32}" />
    public class EcommerceAppUsersContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public EcommerceAppUsersContext(DbContextOptions<EcommerceAppUsersContext> options) 
            : base(options)
        {
        }
    }
}