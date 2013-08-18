// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace Facebook.Client
{
    internal enum WebDialogOptions
    {
        None,
        SilentMode
    }

    internal enum WebDialogStatus
    {
        Success = 0,

        UserCancel = 1,

        ErrorHttp = 2
    }

    internal sealed class WebDialogResult
    {
        public string ResponseData { get; private set; }

        public WebDialogStatus ResponseStatus { get; private set; }

        public uint ResponseErrorDetail { get; private set; }

        public WebDialogResult(string data, WebDialogStatus status, uint error)
        {
            ResponseData = data;
            ResponseStatus = status;
            ResponseErrorDetail = error;
        }
    }

    internal sealed class WebDialogBroker
    {
        private static string responseData = "";
        private static uint responseErrorDetail = 0;
        private static WebDialogStatus responseStatus = WebDialogStatus.UserCancel;
        private static AutoResetEvent dialogFinishedEvent = new AutoResetEvent(false);

        static public bool DialogInProgress { get; private set; }
        static public Uri StartUri { get; private set; }
        static public Uri EndUri { get; private set; }

        /// <summary>
        /// Mimics the WebAuthenticationBroker's AuthenticateAsync method.
        /// </summary>
        public static Task<WebDialogResult> PresentAsync(WebDialogOptions options, Uri startUri, Uri endUri)
        {
            if (options != WebDialogOptions.None)
            {
                throw new NotImplementedException("This method does not support dialog options other than 'None'.");
            }

            PhoneApplicationFrame rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;

            if (rootFrame == null)
            {
                throw new InvalidOperationException();
            }

            WebDialogBroker.StartUri = startUri;
            WebDialogBroker.EndUri = endUri;
            WebDialogBroker.DialogInProgress = true;

            // Navigate to the login page.
            rootFrame.Navigate(new Uri("/Facebook.Client;component/FacebookDialogPage.xaml", UriKind.Relative));

            Task<WebDialogResult> task = Task<WebDialogResult>.Factory.StartNew(() =>
            {
                dialogFinishedEvent.WaitOne();
                return new WebDialogResult(responseData, responseStatus, responseErrorDetail);
            });

            return task;
        }

        public static void OnDialogFinished(string data, WebDialogStatus status, uint error)
        {
            WebDialogBroker.responseData = data;
            WebDialogBroker.responseStatus = status;
            WebDialogBroker.responseErrorDetail = error;

            WebDialogBroker.DialogInProgress = false;

            // Signal the waiting task that the authentication operation has finished.
            dialogFinishedEvent.Set();
        }
    }
}
