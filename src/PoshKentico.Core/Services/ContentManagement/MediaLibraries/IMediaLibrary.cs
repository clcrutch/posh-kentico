using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.ContentManagement.MediaLibraries
{
    /// <summary>
    /// Represents a Media Library Object.
    /// </summary>
    public interface IMediaLibrary
    {
        /// <summary>
        /// Gets the display name for the media library.
        /// </summary>
        string LibraryDisplayName { get; }

        /// <summary>
        /// Gets the media library name.
        /// </summary>
        string LibraryName { get; }

        /// <summary>
        /// Gets the media library description.
        /// </summary>
        string LibraryDescription { get; }

        /// <summary>
        /// Gets the media library folder.
        /// </summary>
        string LibraryFolder { get; }

        /// <summary>
        /// Gets the site id for the media library.
        /// </summary>
        int LibrarySiteID { get; }
    }
}
