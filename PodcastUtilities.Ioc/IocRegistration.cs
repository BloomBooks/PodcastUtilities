﻿using PodcastUtilities.Common;
using PodcastUtilities.Common.IO;

namespace PodcastUtilities.Ioc
{
	public static class IocRegistration
	{
		public static void RegisterFileServices(IIocContainer container)
		{
			container.Register<IDriveInfoProvider, SystemDriveInfoProvider>();
			container.Register<IDirectoryInfoProvider, SystemDirectoryInfoProvider>();
			container.Register<IFileUtilities, FileUtilities>();
			container.Register<IFileCopier, FileCopier>();
			container.Register<IFileFinder, FileFinder>();
			container.Register<IFileSorter, FileSorter>();
            container.Register<IUnwantedFileRemover, UnwantedFileRemover>();
        }

        public static void RegisterPlaylistServices(IIocContainer container)
        {
            container.Register<IPlaylistFactory, PlaylistFactory>();
        }

        public static void RegisterFeedServices(IIocContainer container)
        {
            container.Register<IPodcastFeedFactory, PodcastFeedFactory>();
            container.Register<IWebClientFactory, WebClientFactory>();
        }
    }
}
