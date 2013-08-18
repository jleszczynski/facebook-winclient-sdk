//-----------------------------------------------------------------------
// <copyright file="FacebookDialog.cs" company="The Outercurve Foundation">
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
using Facebook;

namespace Facebook.Client
{
    /// <summary>
    /// Facebook Dialog
    /// </summary>
    public class FacebookDialog
    {
        /// <summary>
        /// Present Dialog asyncronizely
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<WebDialogResult> PresentDialogAsync(string method, Dictionary<string, object> parameters)
        {
            var session = FacebookSessionCacheProvider.Current.GetSessionData();
            if (session != null)
            {
                FacebookClient client = new FacebookClient();
                client.AppId = session.AppId;

                if (parameters != null && !parameters.ContainsKey("redirect_uri"))
                {
                    parameters.Add("redirect_uri", "https://www.facebook.com/connect/login_success.html");
                }
                Uri startUri = client.GetDialogUrl(method, parameters);

                Uri endUri = new Uri("https://www.facebook.com/connect/login_success.html");

                var result = await WebDialogBroker.PresentAsync(WebDialogOptions.None, startUri, endUri);

                if (result.ResponseStatus == WebDialogStatus.ErrorHttp)
                {
                    throw new InvalidOperationException();
                }
                else if (result.ResponseStatus == WebDialogStatus.UserCancel)
                {
                    throw new InvalidOperationException();
                }
                return result;
            }
            else
                return null;
        }
    }
}
