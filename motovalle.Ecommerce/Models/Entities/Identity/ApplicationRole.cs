// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationRole.cs" company="Innova Marketing Systems S.A.S">
//    © All rights reserved
// </copyright>
// <summary>
//   Identity role extended model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.Entities.Identity
{
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Identity role extended model
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityRole{System.Int32}" />
    public class ApplicationRole : IdentityRole<int>
    {
    }
}
