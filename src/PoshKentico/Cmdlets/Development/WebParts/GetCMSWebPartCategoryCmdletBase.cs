using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.WebParts
{
    [ExcludeFromCodeCoverage]
    public class GetCMSWebPartCategoryCmdletBase : MefCmdlet
    {
        #region Constants

        private const string CATEGORYNAME = "Category Name";
        private const string IDSETNAME = "ID";
        private const string PATH = "Path";
        private const string WEBPART = "Web Part";

        protected const string NONE = "None";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The category name or display name the webpart category.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = CATEGORYNAME)]
        [Alias("DisplayName", "Name")]
        public string CategoryName { get; set; }

        /// <summary>
        /// <para type="description">The path to get the web part category at.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PATH)]
        [Alias("Path")]
        public string CategoryPath { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the web part category to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = IDSETNAME)]
        public int[] ID { get; set; }

        /// <summary>
        /// <para type="description">Indiciates if the cmdlet should look recursively for web part categories.</para>
        /// </summary>
        [Parameter(ParameterSetName = CATEGORYNAME)]
        [Parameter(ParameterSetName = IDSETNAME)]
        [Parameter(ParameterSetName = PATH)]
        public virtual SwitchParameter Recurse { get; set; }

        /// <summary>
        /// <para type="description">Indicates if the CategoryName supplied is a regular expression.</para>
        /// </summary>
        [Parameter(ParameterSetName = CATEGORYNAME)]
        [Alias("Regex")]
        public SwitchParameter RegularExpression { get; set; }

        /// <summary>
        /// <para type="description">The webpart to get the web part category for.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = WEBPART)]
        public WebPartInfo WebPart { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this web part. Populated by MEF.
        /// </summary>
        [Import]
        public GetCMSWebPartCategoryBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IWebPartCategory> categories = null;

            switch (this.ParameterSetName)
            {
                case CATEGORYNAME:
                    categories = this.BusinessLayer.GetWebPartCategories(this.CategoryName, this.RegularExpression.ToBool(), this.Recurse.ToBool());
                    break;
                case IDSETNAME:
                    categories = this.BusinessLayer.GetWebPartCategories(this.ID, this.Recurse.ToBool());
                    break;
                case PATH:
                    categories = this.BusinessLayer.GetWebPartCategories(this.CategoryPath, this.Recurse.ToBool());
                    break;
                case WEBPART:
                    categories = new IWebPartCategory[]
                    {
                        this.BusinessLayer.GetWebPartCategory(this.WebPart.ActLike<IWebPart>()),
                    };
                    break;

                case NONE:
                    categories = this.BusinessLayer.GetWebPartCategories();
                    break;
            }

            foreach (var category in categories)
            {
                this.ActOnObject(category);
            }
        }

        protected virtual void ActOnObject(IWebPartCategory webPartCategory)
        {
            this.WriteObject(webPartCategory?.UndoActLike());
        }

        #endregion
    }
}
