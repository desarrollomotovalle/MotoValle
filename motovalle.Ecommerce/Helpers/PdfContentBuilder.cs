// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PdfContentBuilder.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Pdf Content Builder
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers
{
    using global::Ecommerce.Models.Entities;
    using global::Ecommerce.Models.ViewModels;
    using Syncfusion.HtmlConverter;
    using Syncfusion.Pdf;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Pdf Content Builder
    /// </summary>
    internal class PdfContentBuilder
    {
        /// <summary>
        /// Gets the content of the manual invoice.
        /// </summary>
        /// <param name="manualInvoicePath">The manual invoice path.</param>
        /// <param name="email">The email.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="logoUrl">The logo URL.</param>
        /// <param name="orderDetailsWithProductInfo">The order details with product information.</param>
        /// <param name="date">The date.</param>
        /// <returns>
        /// Main content for manual invoice
        /// </returns>
        internal string GetManualInvoiceContent(string manualInvoicePath, string email, string phoneNumber, string fullName, string logoUrl, OrderWithDetailsAndProductInfo orderDetailsWithProductInfo, DateTime date)
        {
            var variableContent = this.GetInternalContent(orderDetailsWithProductInfo.OrderDetails);
            var taxesContent = this.GetTaxesContent(orderDetailsWithProductInfo.HOrdersTaxes);
            return new StreamReader(manualInvoicePath)
                .ReadToEnd()
                .Replace("${logoUrl}", logoUrl)
                .Replace("${orderNumber}", orderDetailsWithProductInfo.OrderNumber.ToString())
                .Replace("${orderDate}", orderDetailsWithProductInfo.OrderDate.ToLocalTime().ToString("dd/MM/yyyy"))
                .Replace("${dateTimeNow}", date.ToString("dd/MM/yyyy"))
                .Replace("${name}", fullName)
                .Replace("${email}", email)
                .Replace("${phoneNumber}", phoneNumber)
                .Replace("${paymentMethod}", orderDetailsWithProductInfo.PaymentMethod)
                .Replace("${paymentId}", string.IsNullOrEmpty(orderDetailsWithProductInfo.TransactionId) ? orderDetailsWithProductInfo.FKFundingRequestId.ToString() : orderDetailsWithProductInfo.TransactionId)
                .Replace("${notes}", string.Concat(orderDetailsWithProductInfo.Notes, " - ", "Estado de Pago: ", orderDetailsWithProductInfo.Status))
                .Replace("${subTotal}", (orderDetailsWithProductInfo.SubTotal ?? 0).ToString("C"))
                .Replace("${taxes}", taxesContent)
                .Replace("${shipping}", $"{orderDetailsWithProductInfo.Shipping ?? 0:C}")
                .Replace("${discount}", $"{orderDetailsWithProductInfo.Discount ?? 0:C}")
                .Replace("${total}", $"{orderDetailsWithProductInfo.Total ?? 0:C}")
                .Replace("${Date}", DateTime.Now.Year.ToString())
                .Replace("${renderBody}", variableContent);
        }

        /// <summary>
        /// Creates the PDF.
        /// </summary>
        /// <param name="webRootPath">The web root path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="contentTemplate">The content template.</param>
        /// <returns>Pdf creates path</returns>
        internal string CreatePDF(string webRootPath, string fileName, string contentTemplate)
        {
            var settings = new WebKitConverterSettings()
            {
                WebKitPath = Path.Combine(webRootPath, "QtBinariesWindows"),
                EnableRepeatTableFooter = true,
                EnableHyperLink = true,
                HtmlEncoding = Encoding.UTF8,
                PdfPageSize = PdfPageSize.Letter
            };

            var converter = new HtmlToPdfConverter() { ConverterSettings = settings };
            ////settings.PdfFooter = FooterHTMLtoPDF(converter);
            var document = converter.Convert(contentTemplate, "");
            var ms = new MemoryStream();
            document.Save(ms);
            document.Close();
            ms.Position = 0;
            var savePath = Path.Combine(webRootPath, "invoice_backup", fileName);
            using var fs = new FileStream(savePath, FileMode.Create);
            ms.CopyTo(fs);
            ms.Close();
            ms.Dispose();
            fs.Dispose();

            return savePath;
        }


        /// <summary>
        /// Gets the content of the internal.
        /// </summary>
        /// <param name="orderDetails">The order details.</param>
        /// <returns>variable content for manual invoice</returns>
        private string GetInternalContent(List<OrderDetailWithProductInfo> orderDetails)
        {
            var content = string.Empty;
            for (int i = 0; i < orderDetails.Count; i++)
            {
                if (i == orderDetails.Count - 1)
                {
                    content += @$"<tr class=""item last"">";
                }
                else
                {
                    content += @$"<tr class=""item"">";
                }

                content += @$"<td class=""record"">
                                { orderDetails[i].ProductName}
                            </td>
                            <td class=""quantity"">
                                {orderDetails[i].Quantity:F01}
                            </td>
                            <td class=""price"" colspan=""2"">
                                { orderDetails[i].LineItemTotal ?? 0:C}
                            </td>
                        </tr>";
            }

            return content;
        }

        /// <summary>
        /// Gets the content of the taxes.
        /// </summary>
        /// <param name="hOrdersTaxes">The h orders taxes.</param>
        /// <returns></returns>
        private string GetTaxesContent(List<HOrdersTaxes> hOrdersTaxes)
        {
            var content = string.Empty;
            foreach (var item in hOrdersTaxes)
            {
                content += @$"<tr class=""taxes"">
                <td></td>
                <td></td>
                <td>{item.TaxName}:</td>
                    <td class=""price"">
                        {item.TaxTotal:C}
                    </td>
                </tr>";
            }

            return content;
        }
    }
}
