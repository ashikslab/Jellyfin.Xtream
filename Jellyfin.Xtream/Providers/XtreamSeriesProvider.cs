// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jellyfin.Xtream.Client;
using Jellyfin.Xtream.Client.Models;
using Jellyfin.Xtream.Service;
using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Xtream.Providers;

/// <summary>
/// The Xtream Codes Series metadata provider.
/// </summary>
/// <param name="logger">Instance of the <see cref="ILogger"/> interface.</param>
public class XtreamSeriesProvider(ILogger<SeriesChannel> logger) : ICustomMetadataProvider<MediaBrowser.Controller.Entities.TV.Episode>, IPreRefreshProvider
{
    /// <summary>
    /// The name of the provider.
    /// </summary>
    public const string ProviderName = "XtreamSeriesProvider";

    /// <inheritdoc/>
    public string Name => ProviderName;

    /// <inheritdoc/>
    public Task<ItemUpdateType> FetchAsync(MediaBrowser.Controller.Entities.TV.Episode item, MetadataRefreshOptions options, CancellationToken cancellationToken)
    {
        string? idStr = item.GetProviderId(ProviderName);
        if (idStr is not null)
        {
            logger.LogDebug("Getting metadata for episode {Id}", idStr);
            int id = int.Parse(idStr, CultureInfo.InvariantCulture);

            // Note: The Xtream API doesn't have a direct "get episode by ID" endpoint
            // The episode info is retrieved as part of the series info
            // For now, we'll just ensure the provider ID is set for proper tracking
            // Additional metadata enrichment could be added here if needed

            return Task.FromResult(ItemUpdateType.MetadataImport);
        }

        return Task.FromResult(ItemUpdateType.None);
    }
}
