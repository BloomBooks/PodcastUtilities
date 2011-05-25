﻿using System;
using System.Collections.Generic;
using System.Xml;

namespace PodcastUtilities.Common
{
	/// <summary>
	/// this object represents the xml control file
	/// </summary>
    public class ControlFile : IControlFile
	{
        private const string DefaultSortField = "Name";
        private const string DefaultSortDirection = "ascending";
        
        private readonly XmlDocument _xmlDocument;

		private List<PodcastInfo> _podcasts;

		/// <summary>
		/// create the object and read the control file from the specified filename
		/// </summary>
		/// <param name="filename">pathname to the control file xml</param>
        public ControlFile(string filename)
		{
			_xmlDocument = new XmlDocument();

			_xmlDocument.Load(filename);

			ReadPodcasts();
		}

        /// <summary>
        /// only used for unit testing
        /// </summary>
        public ControlFile(XmlDocument document)
		{
		    _xmlDocument = document;

            ReadPodcasts();
		}

		/// <summary>
		/// pathname to the root folder to copy from when synchronising
		/// </summary>
        public string SourceRoot
		{
			get { return GetNodeText("podcasts/global/sourceRoot"); }
		}
		
		/// <summary>
		/// pathname to the destination root folder
		/// </summary>
        public string DestinationRoot
		{
			get { return GetNodeText("podcasts/global/destinationRoot"); }
		}

		/// <summary>
		/// filename and extension for the generated playlist
		/// </summary>
        public string PlaylistFilename
		{
			get { return GetNodeText("podcasts/global/playlistFilename"); }
		}

        /// <summary>
        /// the format for the generated playlist
        /// </summary>
        public PlaylistFormat PlaylistFormat
        {
            get
            {
                string format = GetNodeText("podcasts/global/playlistFormat").ToLower();
                switch (format)
                {
                    case "wpl":
                        return PlaylistFormat.WPL;
                    case "asx":
                        return PlaylistFormat.ASX;
                }
                throw new IndexOutOfRangeException(string.Format("{0} is not a valid value for the playlist format",format));
            }
        }

        /// <summary>
        /// free space in MB to leave on the destination device
        /// </summary>
        public long FreeSpaceToLeaveOnDestination
        {
            get
            {
                try
                {
                    return Convert.ToInt64(GetNodeText("podcasts/global/freeSpaceToLeaveOnDestinationMB"));
                }
                catch
                {
					return 0;
                }
            }
        }

		/// <summary>
		/// the configuration for the individual podcasts
		/// </summary>
        public IList<PodcastInfo> Podcasts
		{
			get { return _podcasts; }
		}

		/// <summary>
		/// the field we are using to sort the podcasts on
		/// </summary>
        public string SortField
        {
            get
            {
				return GetNodeTextOrDefault("podcasts/global/sortfield", DefaultSortField);
            }
        }

        /// <summary>
        /// direction to sort in
        /// </summary>
        public string SortDirection
        {
            get
            {
            	return GetNodeTextOrDefault("podcasts/global/sortdirection", DefaultSortDirection);
            }
        }

        private PodcastFeedFormat ReadFeedFormat(string format)
        {
            switch (format.ToLower())
            {
                case "rss":
                    return PodcastFeedFormat.RSS;
                case "atom":
                    return PodcastFeedFormat.ATOM;
            }
            throw new IndexOutOfRangeException(string.Format("{0} is not a valid value for the feed format", format));
        }

        private FeedInfo ReadFeedInfo(XmlNode feedNode)
        {
            if (feedNode == null)
            {
                return null;
            }
            return new FeedInfo()
                       {
                           Address = new Uri(GetNodeText(feedNode, "url")),
                           Format = ReadFeedFormat(GetNodeText(feedNode, "format"))
                       };
        }

		private void ReadPodcasts()
		{
			_podcasts = new List<PodcastInfo>();

			var podcastNodes = _xmlDocument.SelectNodes("podcasts/podcast");

			if (podcastNodes != null)
			{
				foreach (XmlNode podcastNode in podcastNodes)
				{
					var podcastInfo = new PodcastInfo
					{
                        Feed = ReadFeedInfo(podcastNode.SelectSingleNode("feed")),
                        Folder = GetNodeText(podcastNode, "folder"),
						Pattern = GetNodeText(podcastNode, "pattern"),
						MaximumNumberOfFiles = Convert.ToInt32(GetNodeTextOrDefault(podcastNode, "number", "-1")),
						SortField = GetNodeTextOrDefault(podcastNode, "sortfield", SortField),
						AscendingSort = !(GetNodeTextOrDefault(podcastNode, "sortdirection", SortDirection).ToLower().StartsWith("desc"))
					};

					_podcasts.Add(podcastInfo);
				}
			}
		}

		private string GetNodeText(string xpath)
		{
			return GetNodeText(_xmlDocument, xpath);
		}

		private string GetNodeTextOrDefault(string xpath, string defaultText)
		{
			return GetNodeTextOrDefault(_xmlDocument, xpath, defaultText);
		}

		private static string GetNodeText(XmlNode root, string xpath)
		{
			XmlNode n = root.SelectSingleNode(xpath);
			if (n == null)
			{
				throw new System.Exception("GetNodeText : Node path '" + xpath + "' not found");
			}
			return n.InnerText;
		}

		private static string GetNodeTextOrDefault(XmlNode root, string xpath, string defaultText)
		{
			XmlNode n = root.SelectSingleNode(xpath);

			return ((n != null) ? n.InnerText : defaultText);
		}
	}
}
