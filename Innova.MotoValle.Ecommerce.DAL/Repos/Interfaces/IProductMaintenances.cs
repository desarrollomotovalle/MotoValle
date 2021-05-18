// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProductMaintenancesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Product Maintenances Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Product Maintenances Repo facade
    /// </summary>
    public interface IProductMaintenancesRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Product Maintenances</returns>
        List<ProductMaintenances> GetAllRecords();

        /// <summary>
        /// Gets the enable records by make.
        /// </summary>
        /// <param name="makesName">Name of the makes.</param>
        /// <returns>
        /// List Of Hero Images
        /// </returns>
        List<ProductMaintenances> GetEnableRecordsByMake(string makesName);

        /// <summary>
        /// Gets the home record.
        /// </summary>
        /// <param name="productsId">The products identifier.</param>
        /// <returns>
        /// Product Maintenances
        /// </returns>
        List<ProductMaintenances> GetEnableRecordsByProduct(int productsId, bool onlyEnables = true);

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product Maintenances by id</returns>
        ProductMaintenances GetRecord(long id);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Product Maintenances created</returns>
        ProductMaintenances CreateRecord(ProductMaintenances item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, ProductMaintenances updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
