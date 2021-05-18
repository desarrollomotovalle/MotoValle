// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportHelper.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Report Helper Implementations
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.ReportHelper
{
    using global::Ecommerce.DAL.EF;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using motovalle.Ecommerce.Models;
    using OfficeOpenXml;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Report Helper Implementations
    /// </summary>
    /// <seealso cref="motovalle.Ecommerce.Helpers.ReportHelper.IReportHelper" />
    public class ReportHelper : IReportHelper
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ecommerceContext _context;

        /// <summary>
        /// The logo URL
        /// </summary>
        private readonly string _logoUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportHelper" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="environment">The environment.</param>
        public ReportHelper(ecommerceContext context, IWebHostEnvironment environment)
        {
            this._context = context;
            this._logoUrl = Path.Combine(environment.WebRootPath, "media", "brands", "motovalle-logo.png");
        }

        /// <summary>
        /// Gets the campaing leads.
        /// </summary>
        /// <returns>
        /// Campaing Leads report
        /// </returns>
        public async Task<byte[]> GetCampaingLeads()
        {
            var records = await this._context.CampaignLeads
                                             .OrderBy(x => x.CampaignLeadsId)
                                             .ToListAsync();


            using ExcelPackage excelPackage = new ExcelPackage();
            var row = 14;
            var col = 1;
            var index = 1;

            ////Creates new sheet
            excelPackage.Workbook.Worksheets.Add("TiendaMotovalle-1");
            var sheet = excelPackage.Workbook.Worksheets["TiendaMotovalle-1"];

            ////Insert 13 rows in blank
            sheet.InsertRow(1, 13);
            ////Import images and set on puntal positions
            sheet.Drawings.AddPicture("logo", new FileInfo(this._logoUrl)).SetPosition(0, 0);

            ////Set predefine width
            sheet.Column(2).Width = 18;
            sheet.Column(3).Width = 25;
            sheet.Column(4).Width = 40;
            sheet.Column(5).Width = 40;
            sheet.Column(6).Width = 40;
            sheet.Column(7).Width = 40;
            sheet.Column(8).Width = 40;
            sheet.Column(9).Width = 40;

            //Set headers
            sheet.Cells[row, col++].Value = "#";
            sheet.Cells[row, col].Style.Font.Bold = true;
            sheet.Cells[row, col++].Value = "Fecha creación";
            sheet.Cells[row, col].Style.Font.Bold = true;
            sheet.Cells[row, col++].Value = "Camapaña";
            sheet.Cells[row, col].Style.Font.Bold = true;
            sheet.Cells[row, col++].Value = "Nombre";
            sheet.Cells[row, col].Style.Font.Bold = true;
            sheet.Cells[row, col++].Value = "Correo";
            sheet.Cells[row, col].Style.Font.Bold = true;
            sheet.Cells[row, col++].Value = "Número de contacto";
            sheet.Cells[row, col].Style.Font.Bold = true;
            sheet.Cells[row, col++].Value = "Remarks";
            sheet.Cells[row, col].Style.Font.Bold = true;
            sheet.Cells[row, col++].Value = "Landing Page";
            sheet.Cells[row, col].Style.Font.Bold = true;
            sheet.Cells[row, col++].Value = "Correo envíado a";
            sheet.Cells[row, col].Style.Font.Bold = true;
            sheet.Cells[row, col++].Value = "Estado";
            sheet.Cells[row, col].Style.Font.Bold = true;

            ////Loop to set values from list data
            foreach (var record in records)
            {
                var campaing = EnumExtensions.GetDisplayName((Campaings)Enum.ToObject(typeof(Campaings), record.Campaign));
                var status = EnumExtensions.GetDisplayName((Status)Enum.ToObject(typeof(Status), record.Status));

                row++;
                col = 1;
                sheet.Cells[row, col++].Value = index;
                sheet.Cells[row, col++].Value = record.CreatedOn.ToString("MM-dd-yyyy HH:mm:ss");
                sheet.Cells[row, col++].Value = campaing;
                sheet.Cells[row, col++].Value = record.Name;
                sheet.Cells[row, col++].Value = record.Email;
                sheet.Cells[row, col++].Value = record.PhoneNumber;
                sheet.Cells[row, col++].Value = record.Remarks;
                sheet.Cells[row, col++].Value = record.InfoFrom;
                sheet.Cells[row, col++].Value = record.EmailSentTo;
                sheet.Cells[row, col++].Value = status;
                index++;
            }

            return excelPackage.GetAsByteArray();
        }
    }
}
