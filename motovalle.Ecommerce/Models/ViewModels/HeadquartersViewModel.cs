// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeadquartersViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Headquarters View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using System.Collections.Generic;
    
    /// <summary>
    /// Headquarters View Model
    /// </summary>
    public class HeadquartersViewModel
    {
        /// <summary>
        /// The price range minimum
        /// </summary>
        private string _nameWithAdress;

        /// <summary>
        /// Gets or sets the make.
        /// </summary>
        /// <value>
        /// The make.
        /// </value>
        public string Make { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get ; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the schedule.
        /// </summary>
        /// <value>
        /// The schedule.
        /// </value>
        public List<Dictionary<string, string>> Schedule { get; set; }

        /// <summary>
        /// Gets or sets the name with address.
        /// </summary>
        /// <value>
        /// The name with address.
        /// </value>
        public string NameWithAddress
        {
            get => string.Concat(this._nameWithAdress);
            set => this._nameWithAdress = string.Concat(this.Make, " - ", this.Name, " - ", this.Address);
        }
    }
}
