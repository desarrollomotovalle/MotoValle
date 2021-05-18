// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHeroImagesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Hero Images Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Hero Images Repo facade
    /// </summary>
    public interface IHeroImagesRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Hero Images</returns>
        List<HeroImages> GetAllRecords();

        /// <summary>
        /// Gets the enable records.
        /// </summary>
        /// <returns>List Of Hero Images</returns>
        List<HeroImages> GetEnableRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Hero Images by id</returns>
        HeroImages GetRecord(long id);

        /// <summary>
        /// Gets the home record.
        /// </summary>
        /// <param name="makesId">The makes identifier.</param>
        /// <returns>Hero Image to Home</returns>
        HeroImages GetHomeRecord(int? makesId = null);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Hero Images created</returns>
        HeroImages CreateRecord(HeroImages item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, HeroImages updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
