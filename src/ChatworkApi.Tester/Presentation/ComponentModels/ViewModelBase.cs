namespace ChatworkApi.Tester.Presentation.ComponentModels
{
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Drawing;
    using Prism.Mvvm;

    public abstract class ViewModelBase : BindableBase
    {
        /// <summary>Raises this object's PropertyChanged event.</summary>
        /// <param name="args">The PropertyChangedEventArgs</param>
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            OnPropertyChangedCore(args.PropertyName);
        }

        /// <summary>
        /// プロパティが変更された際に呼ばれます。
        /// </summary>
        /// <param name="propertyName">変更されたプロパティの名称</param>
        protected virtual void OnPropertyChangedCore(string propertyName)
        {
            
        }
    }
}