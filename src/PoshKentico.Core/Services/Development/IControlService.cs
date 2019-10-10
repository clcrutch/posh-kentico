using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Development
{
    public interface IControlService<TControl, TControlCategory>
    {
        #region Properties

        /// <summary>
        /// Gets a list of all of the <see cref="IControl{T}"/> provided by the CMS System.
        /// </summary>
        IEnumerable<IControl<TControl>> Controls { get; }

        /// <summary>
        /// Gets a list of all of the <see cref="IControlCategory{T}"/> provided by the CMS System.
        /// </summary>
        IEnumerable<IControlCategory<TControlCategory>> Categories { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the <see cref="IControlCategory{T}"/>.
        /// </summary>
        /// <param name="controlCategory">The <see cref="IControlCategory{T}"/> to create.</param>
        /// <returns>The newly created <see cref="IControlCategory{T}"/>.</returns>
        IControlCategory<TControlCategory> Create(IControlCategory<TControlCategory> controlCategory);

        /// <summary>
        /// Deletes the specified <see cref="IControlCategory{T}"/>.
        /// </summary>
        /// <param name="controlCategory">The <see cref="IControlCategory{T}"/> to delete.</param>
        void Delete(IControlCategory<TControlCategory> controlCategory);

        /// <summary>
        /// Deletes the specified <see cref="IControl{T}"/>.
        /// </summary>
        /// <param name="control">The <see cref="IControl{T}"/> to delete.</param>
        void Delete(IControl<TControl> control);

        /// <summary>
        /// Gets a list of <see cref="IControlCategory{T}"/> that have <paramref name="parentControlCategory"/> as the parent.
        /// </summary>
        /// <param name="parentControlCategory">The parent <see cref="IControlCategory{T}"/> to the desired web part categories.</param>
        /// <returns>A list of <see cref="IControlCategory{T}"/> that have <paramref name="parentControlCategory"/> as the parent.</returns>
        IEnumerable<IControlCategory<TControlCategory>> GetControlCategories(IControlCategory<TControlCategory> parentControlCategory);

        /// <summary>
        /// Gets the <see cref="IControlCategory{T}"/> which matches the supplied ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="IControlCategory{T}"/> to return.</param>
        /// <returns>The <see cref="IControlCategory{T}"/> which matches the ID, else null.</returns>
        IControlCategory<TControlCategory> GetControlCategory(int id);

        /// <summary>
        /// Gets a list of <see cref="IControl{T}"/> which have <paramref name="controlCategory"/> as their parent category.
        /// </summary>
        /// <param name="controlCategory">The <see cref="IControlCategory{T}"/> which is the category for the desired list of <see cref="IControlCategory{T}"/>.</param>
        /// <returns>A list of <see cref="IControl{T}"/> which are related to <paramref name="controlCategory"/>.</returns>
        IEnumerable<IControl<TControl>> GetControls(IControlCategory<TControlCategory> controlCategory);

        /// <summary>
        /// Updates the <see cref="IControlCategory{T}"/>.
        /// </summary>
        /// <param name="controlCategory">The <see cref="IControlCategory{T}"/> to update.</param>
        void Update(IControlCategory<TControlCategory> controlCategory);

        #endregion
    }
}
