﻿#region License
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
using PortableDeviceApiLib;

namespace PodcastUtilities.PortableDevices
{
    /// <summary>
    /// create a portable MTP device
    /// </summary>
    internal class PortableDeviceFactory : IPortableDeviceFactory
    {
        /// <summary>
        /// create from unique ID, this will attach PodcastUtilities as a client to the specified device
        /// </summary>
        /// <param name="deviceId">ID</param>
        /// <returns>the device</returns>
        public IPortableDevice Create(string deviceId)
        {
            var deviceValues = (IPortableDeviceValues)new PortableDeviceTypesLib.PortableDeviceValuesClass();

            deviceValues.SetStringValue(ref PortableDevicePropertyKeys.WPD_CLIENT_NAME, "PodcastUtilities.PortableDevices");
            deviceValues.SetUnsignedIntegerValue(ref PortableDevicePropertyKeys.WPD_CLIENT_MAJOR_VERSION, 1);
            deviceValues.SetUnsignedIntegerValue(ref PortableDevicePropertyKeys.WPD_CLIENT_MINOR_VERSION, 0);
            deviceValues.SetUnsignedIntegerValue(ref PortableDevicePropertyKeys.WPD_CLIENT_REVISION, 1);

            var device = new PortableDeviceClass();

            device.Open(deviceId, deviceValues);

            return device;
        }
    }
}