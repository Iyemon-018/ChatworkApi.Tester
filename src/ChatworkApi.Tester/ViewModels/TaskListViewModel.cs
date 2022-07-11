namespace ChatworkApi.Tester.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using System.Windows.Input;
    using Domain.Interactors.Requests;
    using Domain.UseCases;
    using Domain.UseCases.Requests;
    using Presentation;
    using Presentation.ComponentModels;
    using Presentation.Models;
    using Prism.Commands;
    using TaskStatus = Domain.TaskStatus;

    public sealed class TaskListViewModel : ViewModelBase, IViewLoadedHolder
    {
        private readonly IGetMyTasksUseCase _getMyTasksUseCase;

        private readonly IWorkTaskStateChangeUseCase _workTaskStateChangeUseCase;

        /// <summary>
        /// 選択されたタスクの状態
        /// </summary>
        private TaskStatus _selectedStatus;

        public TaskListViewModel()
        {
        }

        public TaskListViewModel(IGetMyTasksUseCase          getMyTasksUseCase
                               , IWorkTaskStateChangeUseCase workTaskStateChangeUseCase)
        {
            _getMyTasksUseCase          = getMyTasksUseCase;
            _workTaskStateChangeUseCase = workTaskStateChangeUseCase;

            _selectedStatus = TaskStatus.InProgress;
            WorkTasks       = new ObservableCollection<Domain.Models.WorkTask>();
            BindingOperations.EnableCollectionSynchronization(WorkTasks, new object());

            WorkTasksView   = CollectionViewSource.GetDefaultView(WorkTasks) as CollectionView;
            if (WorkTasksView != null)
            {
                WorkTasksView.Filter = OnWorkTasksFilter;
                WorkTasksView.SortDescriptions.Add(new SortDescription(nameof(Domain.Models.WorkTask.Limit), ListSortDirection.Ascending));
            }

            LoadedCommand       = new DelegateCommand(ExecuteLoadedCommand);
            CompleteTaskCommand = new DelegateCommand<Domain.Models.WorkTask>(ExecuteCompleteTaskCommand);
            IncompleteTaskCommand = new DelegateCommand<Domain.Models.WorkTask>(ExecuteIncompleteTaskCommand);
            StatusItems = new ObservableCollection<CollectionViewItem<TaskStatus>>
                          {
                              new CollectionViewItem<TaskStatus> {DisplayName = "未完了", Item = TaskStatus.InProgress}
                            , new CollectionViewItem<TaskStatus> {DisplayName = "完了", Item  = TaskStatus.Done}
                          };
        }

        /// <summary>
        /// 選択中のタスク
        /// </summary>
		private Domain.Models.WorkTask _selectedWorkTask;

        /// <summary>
        /// 選択中のタスクを設定、または取得します。
        /// </summary>
        public Domain.Models.WorkTask SelectedWorkTask
        {
            get => _selectedWorkTask;
            set => SetProperty(ref _selectedWorkTask, value);
        }

        public ObservableCollection<Domain.Models.WorkTask> WorkTasks { get; }

        public CollectionView WorkTasksView { get; }

        public ICommand LoadedCommand { get; }

        public ObservableCollection<CollectionViewItem<TaskStatus>> StatusItems { get; }

        /// <summary>
        /// 選択されたタスクの状態を設定、または取得します。
        /// </summary>
        public TaskStatus SelectedStatus
        {
            get => _selectedStatus;
            set => SetProperty(ref _selectedStatus, value);
        }

        /// <summary>
        /// タスクを完了状態にするためのコマンドを取得します。
        /// </summary>
        public ICommand CompleteTaskCommand { get; }

        /// <summary>
        /// タスクを未完了状態にするためのコマンドを取得します。
        /// </summary>
        public ICommand IncompleteTaskCommand { get; }

        private async void ExecuteCompleteTaskCommand(Domain.Models.WorkTask parameter)
        {
            if (parameter == null) return;

            var request  = WorkTaskStateChangeRequest.ToComplete(parameter.Room.Id, parameter.Id);

            await ChangeTaskStatusAsync(request);
        }

        private async void ExecuteIncompleteTaskCommand(Domain.Models.WorkTask parameter)
        {
            if (parameter == null) return;

            var request = WorkTaskStateChangeRequest.ToInProgress(parameter.Room.Id, parameter.Id);

            await ChangeTaskStatusAsync(request);
        }

        private async Task ChangeTaskStatusAsync(IWorkTaskStateChangeRequest request)
        {
            var response = await _workTaskStateChangeUseCase.Execute(request);
            var changedTask = WorkTasks.FirstOrDefault(x => x.Id == response.StateChangedTaskId);
            if (changedTask != null) WorkTasks.Remove(changedTask);
        }

        /// <summary>
        /// プロパティが変更された際に呼ばれます。
        /// </summary>
        /// <param name="propertyName">変更されたプロパティの名称</param>
        protected override void OnPropertyChangedCore(string propertyName)
        {
            base.OnPropertyChangedCore(propertyName);

            switch (propertyName)
            {
                case nameof(SelectedStatus):
                    OnSelectedStatusChanged(SelectedStatus);
                    break;
            }
        }

        private void OnSelectedStatusChanged(TaskStatus newValue)
        {
            WorkTasksView.Refresh();
        }

        private bool OnWorkTasksFilter(object item)
        {
            if (item is Domain.Models.WorkTask workTask) return workTask.Status == SelectedStatus;

            return false;
        }

        private async void ExecuteLoadedCommand()
        {
            var response        = await _getMyTasksUseCase.Execute(GetMyTasksRequest.InProgressAll()).ConfigureAwait(false);
            var inProgressTasks = response.Tasks.ToArray();

            response = await _getMyTasksUseCase.Execute(GetMyTasksRequest.DoneAll()).ConfigureAwait(false);
            var doneTasks = response.Tasks.ToArray();

            WorkTasks.Clear();
            WorkTasks.AddRange(inProgressTasks);
            WorkTasks.AddRange(doneTasks);

            await SetWorkTaskImages(inProgressTasks).ConfigureAwait(false);
            await SetWorkTaskImages(doneTasks).ConfigureAwait(false);
        }

        private async Task SetWorkTaskImages(IEnumerable<Domain.Models.WorkTask> workTasks)
        {
            await BitmapImageMapping.SetAccountIconToImageProperties(workTasks.Select(x => x.Assigned).ToArray()
                                                                   , x => x.AvatarImage
                                                                   , x => x.Id
                                                                   , x => x.AvatarUrl)
                                    .ConfigureAwait(false);

            await BitmapImageMapping.SetRoomIconToImageProperties(workTasks.Select(x => x.Room).ToArray()
                                                                , x => x.IconImage
                                                                , x => x.Id
                                                                , x => x.IconPath)
                                    .ConfigureAwait(false);
        }
    }
}