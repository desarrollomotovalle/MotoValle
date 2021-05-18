// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FundingRequestsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Funding Requests Repo implementations
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos
{
    using Ecommerce.DAL.EF;
    using Ecommerce.DAL.Repos.Interfaces;
    using Ecommerce.Models.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Funding Requests Repo implementations
    /// </summary>
    /// <seealso cref="Ecommerce.DAL.Repos.Interfaces.IFundingRequestsRepo" />
    public class FundingRequestsRepo : IFundingRequestsRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="FundingRequestsRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public FundingRequestsRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Funding Request created
        /// </returns>
        public FundingRequests CreateRecord(FundingRequests item)
        {
            this._db.FundingRequests.Add(item);
            this._db.SaveChanges();
            return item;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteRecord(long id)
        {
            var item = this.GetRecord(id);
            if (item != null)
            {
                this._db.FundingRequests.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of Funding requests
        /// </returns>
        public List<FundingRequests> GetAllRecords()
        {
            return this._db.FundingRequests
                       .AsNoTracking()
                       .Include(x => x.Orders)
                       .Include(x => x.FKCustomers)
                       .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Funding request for id
        /// </returns>
        public FundingRequests GetRecord(long id)
        {
            return this._db.FundingRequests
                       .AsNoTracking()
                       .Include(x => x.Orders)
                       .Include(x => x.FKCustomers)
                       .FirstOrDefault(x => x.FundingRequestId == id);
        }

        /// <summary>
        /// Gets the rercord for order.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>
        /// funding request for order
        /// </returns>
        public FundingRequests GetRercordForOrder(long orderId)
        {
            return this._db.Orders
                           .AsNoTracking()
                           .FirstOrDefault(x => x.OrdersId == orderId).FKFundingRequests;
        }

        /// <summary>
        /// Gets the rercord for order number.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <returns></returns>
        public FundingRequests GetRercordForOrderNumber(long orderNumber)
        {
            return this._db.Orders
                           .AsNoTracking()
                           .FirstOrDefault(x => x.OrderNumber == orderNumber).FKFundingRequests;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, FundingRequests updatedItem)
        {
            var item = this.Get(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.FKCustomersId = updatedItem.FKCustomersId;
            item.RequestDate = updatedItem.RequestDate;
            item.UpdateDate = updatedItem.UpdateDate;
            item.DateOfBirth = updatedItem.DateOfBirth;
            item.IdentificationType = updatedItem.IdentificationType;
            item.IdentificationNumber = updatedItem.IdentificationNumber;
            item.BankName = updatedItem.BankName;
            item.TotalAmountRequest = updatedItem.TotalAmountRequest;
            item.InitialFee = updatedItem.InitialFee;
            item.Installments = updatedItem.Installments;
            item.Profession = updatedItem.Profession;
            item.Status = updatedItem.Status;
            item.Notes = updatedItem.Notes;
            item.CityOfResidence = updatedItem.CityOfResidence;
            item.UpdatedFor = updatedItem.UpdatedFor;
            item.MonthlyIncome = updatedItem.MonthlyIncome;
            item.IndependentActivity = updatedItem.IndependentActivity;
            item.EconomicActivity = updatedItem.EconomicActivity;
            this._db.FundingRequests.Update(item);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Funding request for id
        /// </returns>
        private FundingRequests Get(long id)
        {
            return this._db.FundingRequests
                       .AsNoTracking()
                       .FirstOrDefault(x => x.FundingRequestId == id);
        }
    }
}
