// <copyright file="NewWebPartCategoryDynamicParameter.cs" company="Chris Crutchfield">
// Copyright (c) Chris Crutchfield. All rights reserved.
// </copyright>

using System.Management.Automation;

namespace PoshKentico.Navigation.DynamicParameters
{
    public class NewWebPartCategoryDynamicParameter
    {
        #region Properties

        [Parameter]
        public string DisplayName { get; set; }

        [Parameter]
        public string ImagePath { get; set; }

        #endregion

    }
}
