namespace ChatworkApi.Tester.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Data;
    using Domain.Models;
    using Presentation.ComponentModels;

    public sealed class RoomDataViewModel : ValidatableViewModelBase
    {
        public RoomDataViewModel()
        {
            Members = new ObservableCollection<RoomMember>();
            BindingOperations.EnableCollectionSynchronization(Members, new object());
        }

        /// <summary>
        /// 概要
        /// </summary>
        private string _description;

        /// <summary>
        /// 概要を取得します。
        /// </summary>
        public string Description
        {
            get => _description;
            private set => SetProperty(ref _description, value);
        }

        public ObservableCollection<RoomMember> Members { get; }
        
        public void Update(string description)
        {
            Description = description;
        }

        public void SetMembers(IEnumerable<RoomMember> members)
        {
            Members.Clear();
            Members.AddRange(members);
        }
    }
}