using System;
using Xamarin.Forms;
using LibVLCSharp.Shared;
using LibVLCSharp.Forms.Shared;
using System.Diagnostics;

namespace LogTestlibVLC
{
    public partial class MainPage : ContentPage
    {
        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;

        public MainPage()
        {
            InitializeComponent();

            Core.Initialize();

            _libVLC = new LibVLC();
            _libVLC.Log += LibVLC_Log;

            _mediaPlayer = new MediaPlayer(_libVLC)
            {
                Media = new Media(_libVLC, "rtsp://viewer:prados123@192.168.1.82", FromType.FromLocation)
            };

            videoView.MediaPlayer = _mediaPlayer;
            _mediaPlayer.Play();

            // Update the status label when the media player starts playing
            _mediaPlayer.Playing += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    statusLabel.Text = "Status: Playing";
                });
            };

            // Update the status label when the media player stops
            _mediaPlayer.Stopped += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    statusLabel.Text = "Status: Stopped";
                });
            };
        }

        private void LibVLC_Log(object sender, LogEventArgs e)
        {
            Debug.WriteLine($"LibVLC Error: {e.Message}");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _mediaPlayer.Stop();
        }
    }
}
