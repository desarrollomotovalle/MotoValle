// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="Innova Marketing Systems S.A.S">
//   All rights reserved
// </copyright>
// <summary>
//   Enum Extensions
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Enum Extensions
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>Display Name</returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            ?.GetCustomAttribute<DisplayAttribute>()
                            ?.GetName();
        }
    }
}
