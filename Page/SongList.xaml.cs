﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VTuberMusic.Modules;
using VTuberMusic.Tools;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace VTuberMusic.Page
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SongList
    {
        public string songListId;
        public Song[] songs;

        public SongList()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            songListId = (string)e.Parameter;
            if (songListId != null)
            {
                Log.WriteLine("[GUI]跳转到歌单: " + songListId, Level.Info);
                SongListList[] songListList = SongListList.GetSongListList("Id", songListId, 1, 1, "Id", "dasc");
                songs = Modules.SongList.GetSongListSong(songListId);
                BitmapImage bitmapImage = new BitmapImage(new Uri(songListList[0].CoverImg));
                SongListName.Text = songListList[0].Name;
                SongListCreator.Text = songListList[0].CreatorRealName;
                SongListCreatorImage.BadgeText = songListList[0].CreatorRealName;
                SongListImage.Source = (bitmapImage);
            }
            else
            {
                Log.WriteLine("[GUI]跳转到歌单: 本地我喜欢歌单", Level.Info);
            }
        }

        private void SongListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.ItemIndex % 2 == 0)
            {
                args.ItemContainer.Background = new SolidColorBrush(Colors.WhiteSmoke);
            }
            else
            {
                args.ItemContainer.Background = new SolidColorBrush(Colors.White);
            }
        }
    }
}
