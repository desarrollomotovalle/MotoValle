// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReportHelper.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Report Helper Facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.ReportHelper
{
    using System.Threading.Tasks;
 
    /// <summary>
    /// Report Helper Facade
    /// </summary>
    public interface IReportHelper
    {
        /// <summary>
        /// Gets the campaing leads.
        /// </summary>
        /// <returns>Campaing Leads report</returns>
        Task<byte[]> GetCampaingLeads();
    }
}
