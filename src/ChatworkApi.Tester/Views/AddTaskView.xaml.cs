namespace ChatworkApi.Tester.Views
{
    using System.Windows.Controls;
    using System.Windows.Data;
    using Presentation.Models;

    /// <summary>
    /// AddTaskView.xaml の相互作用ロジック
    /// </summary>
    public partial class AddTaskView : UserControl
    {
        public AddTaskView()
        {
            InitializeComponent();
        }

        private void SelectedAccounts_OnFilter(object          sender
                                             , FilterEventArgs e)
        {
            if (!(e.Item is SelectableAccount account))
            {
                e.Accepted = false;
            }
            else
            {
                e.Accepted = account.Selected;
            }
        }
    }
}