using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Configuration
{
    /// <summary>
    /// Service for providing access to a CMS sites.
    /// </summary>
    public interface ISiteService
    {
        #region Properties

        /// <summary>
        /// Gets a list of all of the <see cref="ISite"/> provided by the CMS System.
        /// </summary>
        IEnumerable<ISite> Sites { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the <see cref="ISite"/>.
        /// </summary>
        /// <param name="site">The <see cref="ISite"/> to create.</param>
        /// <returns>The newly created <see cref="ISite"/>.</returns>
        ISite Create(ISite site);

        /// <summary>
        /// Deletes the specified <see cref="ISite"/>.
        /// </summary>
        /// <param name="site">The <see cref="ISite"/> to delete.</param>
        void Delete(ISite site);

        /// <summary>
        /// Gets the <see cref="ISite"/> which matches the supplied ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="ISite"/> to return.</param>
        /// <returns>The <see cref="ISite"/> which matches the ID, else null.</returns>
        ISite GetSite(int id);

        /// <summary>
        /// Updates the <see cref="ISite"/>.
        /// </summary>
        /// <param name="site">The <see cref="ISite"/> to update.</param>
        void Update(ISite site);

        #endregion
    }
}
