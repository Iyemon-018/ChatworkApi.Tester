namespace ChatworkApi.Tester.Presentation.Components
{
    using System;
    using MaterialDesignThemes.Wpf;
    using Prism.Mvvm;

    public sealed class NotifyMessage : BindableBase
    {
        public SnackbarMessageQueue MessageQueue { get; }

        public NotifyMessage()
        {
            MessageQueue = new SnackbarMessageQueue();
        }

        public NotifyMessage(TimeSpan duration)
        {
            MessageQueue = new SnackbarMessageQueue(duration);
        }

        public void Notify(string message)
        {
            MessageQueue.Enqueue(message);
        }
    }
}