namespace ChatworkApi.Tester.Views
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Messages;
    using Presentation.Models;

    /// <summary>
    /// ChatMessageView.xaml の相互作用ロジック
    /// </summary>
    public partial class ChatMessageView : UserControl
    {
        public ChatMessageView()
        {
            InitializeComponent();
        }

        private void InfoWithTitleButton_OnClick(object          sender
                                               , RoutedEventArgs e)
        {
            var messageBuilder = new MessageBuilder();
            SetMessage(messageBuilder.Information.Add(string.Empty).Build());
        }

        private void InfoButton_OnClick(object          sender
                                      , RoutedEventArgs e)
        {
            var messageBuilder = new MessageBuilder();
            SetMessage(messageBuilder.Information.Add(string.Empty).Build());
        }

        internal void SetMessage(string content
                              , bool   withNewLine = false)
        {
            var selectionStart          = MessageTextBox.SelectionStart;
            var newMessage              = MessageTextBox.Text.Insert(selectionStart, content);
            if (withNewLine) newMessage = $"{newMessage}{Environment.NewLine}";

            MessageTextBox.Text       = newMessage;
            MessageTextBox.CaretIndex = selectionStart + newMessage.Length;
        }

        private void MemberListBox_OnPreviewMouseLeftButtonUp(object               sender
                                                            , MouseButtonEventArgs e)
        {
            // SelectionChanged イベントだと連続で同じアカウントを選択できないのでこのイベントでToを設定する。
            var memberListBox = sender as ListBox;

            if (!(memberListBox?.SelectedItem is MessageTo messageTo)) return;

            SetMessage(messageTo.ToContent, true);
        }
    }
}