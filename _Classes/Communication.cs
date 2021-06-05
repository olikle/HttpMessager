using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpMessager
{
    #region Constructor
    /// <summary>
    /// https://github.com/windows-toolkit/WindowsCommunityToolkit
    /// https://docs.microsoft.com/en-us/windows/uwp/design/shell/tiles-and-notifications/send-local-toast?tabs=uwp
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

    }
    #endregion

}
