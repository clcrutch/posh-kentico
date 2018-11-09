// <copyright file="SecurityPropertyEnum.cs" company="Chris Crutchfield">
// Copyright (C) 2017  Chris Crutchfield
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see &lt;http://www.gnu.org/licenses/&gt;.
// </copyright>

namespace PoshKentico.Core.Services.ContentManagement.MediaLibraries
{
    /// <summary>
    /// Types of the Security Option Properties for a Media Library.
    /// </summary>
    public enum SecurityPropertyEnum
    {
        /// <summary>
        /// Represents Access Property
        /// </summary>
        Access = 0,

        /// <summary>
        /// Represents FileCreate Property
        /// </summary>
        FileCreate = 1,

        /// <summary>
        /// Represents FolderCreate Property
        /// </summary>
        FolderCreate = 2,

        /// <summary>
        /// Represents FileDelete Property
        /// </summary>
        FileDelete = 3,

        /// <summary>
        /// Represents FolderDelete Property
        /// </summary>
        FolderDelete = 4,

        /// <summary>
        /// Represents FileModify Property
        /// </summary>
        FileModify = 5,

        /// <summary>
        /// Represents FolderModify Property
        /// </summary>
        FolderModify = 6,
    }
}
