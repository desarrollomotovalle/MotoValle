// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrdersRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Orders Repo implementations
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos
{
    using Ecommerce.DAL.EF;
    using Ecommerce.DAL.Repos.Interfaces;
    using Ecommerce.Models.Entities;
    using Ecommerce.Models.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Orders Repo implementations
    /// </summary>
    /// <seealso cref="Ecommerce.DAL.Repos.Interfaces.IOrdersRepo" />
    public class OrdersRepo : IOrdersRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public OrdersRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Create/Add Order record
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Order created</returns>
        public Orders CreateRecord(Orders item)
        {
            item.OrderNumber = this.GetNewOrderNumber();
            this._db.Orders.Add(item);
            this._db.SaveChanges();
            return item;
        }

        /// <summary>
        /// Gets the new order number.
        /// </summary>
        /// <returns>Next order number</returns>
        private int GetNewOrderNumber()
        {
            return this._db.Orders.Count() + 1;
        }

        /// <summary>
        /// Deletes Order record by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteRecord(long id)
        {
            Orders item = this.GetRecord(id);
            if (item != null)
            {
                this._db.Orders.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of orders</returns>
        public List<Orders> GetAllRecords()
        {
            return this._db.Orders
                       .AsNoTracking()
                       .Include(x => x.FkCustomers)
                       .ToList();
        }

        /// <summary>
        /// Gets the records for customer.
        /// </summary>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>List of orders for customer</returns>
        public List<Orders> GetRecordsForCustomer(long customersId)
        {
            return this._db.Orders
                       .AsNoTracking()
                       .Include(x => x.FkCustomers)
                       .Where(x => x.FkCustomersId == customersId)
                       .ToList();
        }

        /// <summary>
        /// Gets the Order record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Order by Id</returns>
        public Orders GetRecord(long id)
        {
            return this._db.Orders
                       .AsNoTracking()
                       .Include(x => x.FkCustomers)
                       .FirstOrDefault(c => c.OrdersId == id);
        }

        /// <summary>
        /// Gets the record for identifier and customer.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>Order for id and customer</returns>
        public Orders GetRecordForIdAndCustomer(long ordersId, long customersId)
        {
            return this._db.Orders
                       .AsNoTracking()
                       .Include(x => x.FkCustomers)
                       .FirstOrDefault(c => c.OrdersId == ordersId
                                         && c.FkCustomersId == customersId);
        }

        /// <summary>
        /// Gets the record for order number.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <returns>Order for number</returns>
        public Orders GetRecordForOrderNumber(long orderNumber)
        {
            return this._db.Orders
                       .AsNoTracking()
                       .Include(x => x.FkCustomers)
                       .FirstOrDefault(c => c.OrderNumber == orderNumber);
        }

        /// <summary>
        /// Gets the record for transaction identifier.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns>Order for transacction id</returns>
        public Orders GetRecordForTransactionId(string transactionId)
        {
            return this._db.Orders
                       .AsNoTracking()
                       .Include(x => x.FkCustomers)
                       .FirstOrDefault(c => c.TransactionId.Equals(transactionId, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Updates the order record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedRecord">The updated record.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, Orders updatedRecord)
        {
            ////Check Order exists
            Orders item = this.Get(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.FkCustomersId = updatedRecord.FkCustomersId;
            item.Status = updatedRecord.Status;
            item.PaymentMethod = updatedRecord.PaymentMethod;
            item.Notes = updatedRecord.Notes;
            item.InvoiceUrl = updatedRecord.InvoiceUrl;
            item.OrderDate = updatedRecord.OrderDate;
            item.OrderNumber = updatedRecord.OrderNumber;
            item.Subtotal = updatedRecord.Subtotal;
            item.Tax = updatedRecord.Tax;
            item.Shipping = updatedRecord.Shipping;
            item.Discount = updatedRecord.Discount;
            item.PhoneNumber = updatedRecord.PhoneNumber;
            item.AlternateNumber = updatedRecord.AlternateNumber;
            item.Country = updatedRecord.Country;
            item.BillToAddress = updatedRecord.BillToAddress;
            item.BillToCity = updatedRecord.BillToCity;
            item.BillToState = updatedRecord.BillToState;
            item.BillToZipcode = updatedRecord.BillToZipcode;
            item.ShipToAddress = updatedRecord.ShipToAddress;
            item.ShipToCity = updatedRecord.ShipToCity;
            item.ShipToState = updatedRecord.ShipToState;
            item.ShipToZipcode = updatedRecord.ShipToZipcode;
            item.Total = updatedRecord.Total;
            item.TransactionId = updatedRecord.TransactionId;
            item.PartialAmountPaid = updatedRecord.PartialAmountPaid;
            item.FKFundingRequestsId = updatedRecord.FKFundingRequestsId;
            item.IdentificationType = updatedRecord.IdentificationType;
            item.IdentificationNumber = updatedRecord.IdentificationNumber;
            item.FullName = updatedRecord.FullName;
            this._db.Orders.Update(item);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Gets the order with details and product information.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>
        /// Order details with product info
        /// </returns>
        public OrderWithDetailsAndProductInfo GetOrderWithDetailsAndProductInfo(long ordersId, long customersId)
        {
            var orderWithDetailsAndProductInfo = this._db.Orders
                                                     .AsNoTracking()
                                                     .Where(x => x.OrdersId == ordersId
                                                              && x.FkCustomersId == customersId)
                                                     .Select(x => new OrderWithDetailsAndProductInfo()
                                                     {
                                                         AlternateNumber = x.AlternateNumber,
                                                         BillToAddress = x.BillToAddress,
                                                         BillToCity = x.BillToCity,
                                                         BillToState = x.BillToState,
                                                         BillToZipCode = x.BillToZipcode,
                                                         Country = x.Country,
                                                         CustomerId = x.FkCustomersId,
                                                         Discount = x.Discount ?? 0,
                                                         FKFundingRequestId = x.FKFundingRequestsId ?? 0,
                                                         FullName = x.FullName,
                                                         IdentifiationNumber = x.IdentificationNumber,
                                                         IdentificationType = x.IdentificationType,
                                                         InvoiceURL = x.InvoiceUrl,
                                                         Notes = x.Notes,
                                                         OrderDate = x.OrderDate,
                                                         OrderNumber = x.OrderNumber,
                                                         PaymentMethod = x.PaymentMethod,
                                                         PhoneNumber = x.PhoneNumber,
                                                         PartialAmountPaid = x.PartialAmountPaid ?? 0,
                                                         ShipToAddress = x.ShipToAddress,
                                                         Shipping = x.Shipping ?? 0,
                                                         ShipToCity = x.ShipToCity,
                                                         ShipToState = x.ShipToState,
                                                         ShipToZipCode = x.ShipToZipcode,
                                                         Status = x.Status,
                                                         SubTotal = x.Subtotal ?? 0,
                                                         Tax = x.Tax,
                                                         Total = x.Total,
                                                         TransactionId = x.TransactionId,
                                                         OrderDetails = x.OrderDetails.Select(x => new OrderDetailWithProductInfo()
                                                         {
                                                             BaseWeight = x.FkProducts.BaseWeight ?? 0,
                                                             BodyStyleName = x.FkProducts.FkBodyStyles.BodyStyleName,
                                                             CategoryDescription = x.FkProducts.FkCategories.Description,
                                                             CategoryId = x.FkProducts.FkCategoriesId ?? 0,
                                                             CategoryName = x.FkProducts.FkCategories.CategoryName,
                                                             CurrentPrice = x.FkProducts.SalesPrice ?? 0,
                                                             Description = x.FkProducts.Description,
                                                             DriveTrainName = x.FkProducts.FkDriveTrains.DriveTrainName,
                                                             EngineTypeName = x.FkProducts.FkEngineTypes.EngineTypeName,
                                                             EpaClassName = x.FkProducts.FkEpaClasses.EpaClassName,
                                                             GasMileage = x.FkProducts.GasMileage,
                                                             HorsePower = x.FkProducts.Horsepower ?? 0,
                                                             IsFeatured = Convert.ToBoolean(x.FkProducts.IsFeatured),
                                                             LineItemTaxTotal = x.Tax,
                                                             LineItemTotal = x.ItemPrice,
                                                             LongDescription = x.FkProducts.LongDescription,
                                                             MakeDescription = x.FkProducts.FkMakes.Description,
                                                             MakeName = x.FkProducts.FkMakes.MakeName,
                                                             ModelName = x.FkProducts.FkModels.ModelName,
                                                             ModelNumber = x.FkProducts.FkModels.ModelsId.ToString(),
                                                             Msrp = x.FkProducts.Msrp ?? 0,
                                                             Notes = x.Notes,
                                                             OrderId = x.FkOrdersId,
                                                             OrderDetailsId = x.OrderDetailsId,
                                                             PassengerDoors = x.FkProducts.PassengerDoors ?? 0,
                                                             Passengers = x.FkProducts.Passengers ?? 0,
                                                             ProductId = x.FkProducts.ProductsId,
                                                             ProductImage = x.FKInventoryItems.CoverPictureUrl,
                                                             ProductImageLarge = string.Empty,
                                                             ProductImageThumb = string.Empty,
                                                             ProductName = x.FkProducts.ProductName,
                                                             Quantity = x.Quantity,
                                                             ShipDate = x.ShipDate,
                                                             Transmission = x.FkProducts.FkTransmissions.TransmissionName,
                                                             Trim = x.FkProducts.Trim,
                                                             Year = x.FkProducts.Year
                                                         }).ToList(),
                                                         HOrdersTaxes = x.HOrdersTaxes.ToList()
                                                     }).FirstOrDefault();

            return orderWithDetailsAndProductInfo;
        }

        /// <summary>
        /// Check existence by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>boolean control</returns>
        private bool RecordExists(long id)
        {
            return this._db.Orders.Any(c => c.OrdersId == id);
        }


        /// <summary>
        /// Gets the Order record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Order by Id</returns>
        public Orders Get(long id)
        {
            return this._db.Orders
                       .AsNoTracking()
                       .FirstOrDefault(c => c.OrdersId == id);
        }
    }
}
