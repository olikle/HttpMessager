using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpMessager
{
    #region Constructor
    /// <summary>
    /// https://github.com/windows-toolkit/WindowsCommunityToolkit
    /// https://docs.microsoft.com/en-us/windows/uwp/design/shell/tiles-and-notifications/send-local-toast?tabs=desktop
    /// </summary>
    class Communication
    {
        /// <summary>
        /// Toasts the notification.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="type">The type.</param>
        public static void ToastNotification(string title, string input, int type)
        {
            //    TargetFramework has to be set in project file to:
            //    <TargetFramework>net6.0-windows10.0.17763.0</TargetFramework>
            //
            //


            // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
            ToastContentBuilder toastContentBuilder = new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813);
            //    .AddText("Andrew sent you a picture")
            if (!string.IsNullOrEmpty(title))
                toastContentBuilder.AddText(title);
            if (!string.IsNullOrEmpty(input))
                toastContentBuilder.AddText(input);
            toastContentBuilder.Show();
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="callUrl">The call URL.</param>
        /// <returns></returns>
        public static async Task<string> SendMessageAsync(string callUrl, string json)
        {
            var handler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(handler);

            var bodyContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage callResult = null;
            callResult = await httpClient.PostAsync(callUrl, bodyContent);
            var resultContent = await callResult.Content.ReadAsStringAsync();
            if (callResult.IsSuccessStatusCode)
            {
                return resultContent;
            }
            else
            {
                return $"Error: {resultContent} {callResult.StatusCode}";
            }
        }

    }
    #endregion

}
