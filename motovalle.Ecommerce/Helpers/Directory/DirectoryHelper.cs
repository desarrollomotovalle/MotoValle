// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryHelper.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Directory Helper
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.Directory
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Directory Helper
    /// </summary>
    internal class DirectoryHelper
    {
        /// <summary>
        /// The WWW root path
        /// </summary>
        private readonly string _wwwRootPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryHelper"/> class.
        /// </summary>
        /// <param name="wwwRootPath">The WWW root path.</param>
        public DirectoryHelper(string wwwRootPath)
        {
            this._wwwRootPath = wwwRootPath;
        }

        /// <summary>
        /// Creates the sub directory.
        /// </summary>
        /// <param name="subDirectoryList">The sub directory list.</param>
        public void CreateSubDirectory(List<string> subDirectoryList)
        {
            var subDirectoryCreated = string.Empty;
            for (int i = 0; i < subDirectoryList.Count; i++)
            {
                subDirectoryCreated = Path.Combine(subDirectoryCreated, subDirectoryList[i]);
                if (i == 0)
                {
                    var partialPath = Path.Combine(this._wwwRootPath, subDirectoryList[i]);
                    if (!Directory.Exists(partialPath))
                    {
                        Directory.CreateDirectory(partialPath);
                    }
                }
                else
                {
                    var partialPath = Path.Combine(this._wwwRootPath, subDirectoryCreated);
                    if (!Directory.Exists(partialPath))
                    {
                        Directory.CreateDirectory(partialPath);
                    }
                }
            }
        }
    }
}
