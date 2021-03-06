#region License
// FreeBSD License
// Copyright (c) 2010 - 2013, Andrew Trevarrow and Derek Wilson
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
// 
// Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
// 
// Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED 
// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
// POSSIBILITY OF SUCH DAMAGE.
#endregion
using System;
using System.Collections.Generic;

namespace PodcastUtilities.PortableDevices
{
    /// <summary>
    /// an object on a device
    /// </summary>
    public interface IDeviceObject
    {
        /// <summary>
        /// unique id
        /// </summary>
        string Id { get; }
        /// <summary>
        /// readable name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// free space
        /// Only relevant for storage objects - so maybe shouldn't be here...
        /// </summary>
        long AvailableFreeSpace { get; }

        /// <summary>
        /// Modify time of the object
        /// </summary>
        DateTime ModifyTime { get; }

        /// <summary>
        /// gets all the folder objects
        /// </summary>
        /// <param name="pattern">pattern to match</param>
        /// <returns>folder objects</returns>
        IEnumerable<IDeviceObject> GetFolders(string pattern);
        
        /// <summary>
        /// gets all the file objects
        /// </summary>
        /// <param name="pattern">pattern to match</param>
        /// <returns>file objects</returns>
        IEnumerable<IDeviceObject> GetFiles(string pattern);
    }
}