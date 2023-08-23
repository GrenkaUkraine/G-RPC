using System;
using System.Collections.Generic;
using System.Diagnostics;
using DiscordRPC;

namespace G_RPC
{
    public class DiscordRPC
    {
        private DiscordRpcClient _client;

        public void StartRPC(
            string appId,
            string details,
            string state,
            string largeImageKey = null,
            string largeImageText = null,
            string smallImageKey = null,
            string smallImageText = null,
            int buttonsCount = 0,
            string button1Text = null,
            string button1Url = null,
            string button2Text = null,
            string button2Url = null
        )
        {
            DiscordRpcClient client = new DiscordRpcClient(appId);

            client.OnReady += (sender, e) =>
            {
                SetupButtons(
                    client,
                    buttonsCount,
                    button1Text,
                    button1Url,
                    button2Text,
                    button2Url
                );
                Debug.WriteLine("RPC connected");
            };

            client.Initialize();

            var presence = new RichPresence
            {
                Details = details,
                State = state,
                Assets = new Assets
                {
                    LargeImageKey = largeImageKey,
                    LargeImageText = largeImageText,
                    SmallImageKey = smallImageKey,
                    SmallImageText = smallImageText
                }
            };

            client.SetPresence(presence);
            _client = client;
        }

        public void CloseConnection()
        {
            _client.ClearPresence();
            _client.Deinitialize();
            _client = null;
        }

        private void SetupButtons(
            DiscordRpcClient client,
            int buttonsCount,
            string button1Text = null,
            string button1Url = null,
            string button2Text = null,
            string button2Url = null
        )
        {
            var buttons = new List<Button>();

            if (buttonsCount > 0 && !string.IsNullOrEmpty(button1Text) && !string.IsNullOrEmpty(button1Url))
            {
                buttons.Add(new Button
                {
                    Label = button1Text,
                    Url = button1Url
                });
            }

            if (buttonsCount > 1 && !string.IsNullOrEmpty(button2Text) && !string.IsNullOrEmpty(button2Url))
            {
                buttons.Add(new Button
                {
                    Label = button2Text,
                    Url = button2Url
                });
            }

            if (buttons.Count > 0)
            {
                client.SetPresence(new RichPresence
                {
                    Buttons = buttons.ToArray(),
                });
            }
        }

    }
}
