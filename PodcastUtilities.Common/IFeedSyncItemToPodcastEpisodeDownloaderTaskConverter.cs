using System;
using System.Collections.Generic;

namespace PodcastUtilities.Common
{
    /// <summary>
    /// converts a number of IFeedSyncItem to IPodcastEpisodeDownloader tasks
    /// </summary>
    public interface IFeedSyncItemToPodcastEpisodeDownloaderTaskConverter
    {
        /// <summary>
        /// converts a number of IFeedSyncItem to IPodcastEpisodeDownloader tasks
        /// </summary>
        /// <param name="downloadItems">the items to be downloaded</param>
        /// <param name="statusUpdate">the update mechanism for the download - can be null</param>
        /// <returns>an array of tasks suitable to be run in a task pool</returns>
        IPodcastEpisodeDownloader[] ConvertItemsToTasks(List<IFeedSyncItem> downloadItems, EventHandler<StatusUpdateEventArgs> statusUpdate);
    }
}