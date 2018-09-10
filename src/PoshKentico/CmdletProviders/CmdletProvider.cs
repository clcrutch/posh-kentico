using CMS.Base;
using PoshKentico.Core.Services.General;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Management.Automation;
using System.Management.Automation.Provider;

namespace PoshKentico.CmdletProviders
{
    public abstract class CmdletProvider : NavigationCmdletProvider, IPropertyCmdletProvider
    {
        protected abstract string ProviderName { get; }

        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

        #region Statics

        public static string GetDirectory(string path)
        {
            throw new NotImplementedException();
        }

        public static string GetName(string path)
        {
            throw new NotImplementedException();
        }

        public static string JoinPath(params string[] items)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IPropertyCmdletProvider

        public void GetProperty(string path, Collection<string> providerSpecificPickList)
        {
            throw new NotImplementedException();
        }

        public object GetPropertyDynamicParameters(string path, Collection<string> providerSpecificPickList)
        {
            throw new NotImplementedException();
        }

        public void SetProperty(string path, PSObject propertyValue)
        {
            throw new NotImplementedException();
        }

        public object SetPropertyDynamicParameters(string path, PSObject propertyValue)
        {
            throw new NotImplementedException();
        }

        public void ClearProperty(string path, Collection<string> propertyToClear)
        {
            throw new NotImplementedException();
        }

        public object ClearPropertyDynamicParameters(string path, Collection<string> propertyToClear)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <inheritdoc/>
        protected override bool IsValidPath(string path)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            throw new NotImplementedException();
        }

        protected override string NormalizeRelativePath(string path, string basePath)
        {
            throw new NotImplementedException();
        }

        protected virtual void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
