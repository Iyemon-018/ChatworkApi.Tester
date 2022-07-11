namespace ChatworkApi.Tester.Views
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using Domain.Models;
    using Messages;
    using ViewModels;

    /// <summary>
    /// ChatRoomView.xaml の相互作用ロジック
    /// </summary>
    public partial class ChatRoomView : UserControl
    {
        public ChatRoomView()
        {
            InitializeComponent();
        }

        private ChatRoomViewModel _viewModel = null;

        private void ReplyMessageButton_OnClick(object          sender
                                              , RoutedEventArgs e)
        {
            if (!((sender as Button)?.DataContext is ChatMessage data)) return;

            var reply = new MessageBuilder().Reply
                                            .Add(data.Sender.Id, _viewModel.SelectedMyRoom.Id, data.Id)
                                            .Add($"{data.Sender.Name}さん")
                                            .Build();
            ChatMessageView.SetMessage(reply, true);
        }

        private void ChatRoomView_OnLoaded(object          sender
                                         , RoutedEventArgs e)
        {
            _viewModel = DataContext as ChatRoomViewModel;
        }

        private void QuoteMessageButton_OnClick(object          sender
                                              , RoutedEventArgs e)
        {
            if (!((sender as Button)?.DataContext is ChatMessage data)) return;

            var quote = new MessageBuilder().Quote.Add(data.Sender.Id, data.SendDateTime.Value, data.Body).Build();

            ChatMessageView.SetMessage(quote, true);
        }

        private void LinkMessageButton_OnClick(object          sender
                                             , RoutedEventArgs e)
        {
            if (!((sender as Button)?.DataContext is ChatMessage data)) return;

            var link = $"https://kcw.kddi.ne.jp/#!rid{_viewModel.SelectedMyRoom.Id}-{data.Id}";
            ChatMessageView.SetMessage(link, true);
        }
    }
}