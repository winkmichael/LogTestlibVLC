using System;
using Xamarin.Forms;
using LibVLCSharp.Shared;
using LibVLCSharp.Forms.Shared;

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
            _mediaPlayer = new MediaPlayer(_libVLC);

            var media = new Media(_libVLC, "rtsp://viewer:prados123@192.168.1.82", FromType.FromLocation);
            _mediaPlayer.Play(media);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _mediaPlayer.Stop();
        }
    }
}
