//-----------------------------------------------------------------------
// <copyright file="FacebookWebDialog.cs" company="The Outercurve Foundation">
//    Copyright (c) 2011, The Outercurve Foundation. 
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//      http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>
// <author>Nathan Totten (ntotten.com) and Prabir Shrestha (prabir.me)</author>
// <website>https://github.com/facebook-csharp-sdk/facbook-winclient-sdk</website>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class FacebookWebDialog : FacebookDialog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<WebDialogResult> PresentFeedDialogAsync()
        {
            return await FacebookDialog.PresentDialogAsync("feed", null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<WebDialogResult> PresentFriendsDialogAsync()
        {
            return await FacebookDialog.PresentDialogAsync("friends", null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<WebDialogResult> PresentPayDialogAsync()
        {
            return await FacebookDialog.PresentDialogAsync("pay", null);
        }

        /// <summary>
        /// Present Request Dialog asyncronizely
        /// </summary>
        /// <returns></returns>
        public static async Task<WebDialogResult> PresentRequestDialogAsync(string title, string message, Dictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }

            parameters.Add("title", title);
            parameters.Add("message", message);
            return await FacebookDialog.PresentDialogAsync("apprequests", parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<WebDialogResult> PresentSendDialogAsync()
        {
            return await FacebookDialog.PresentDialogAsync("send", null);
        }
    }
}
