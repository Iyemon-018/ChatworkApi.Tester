namespace ChatworkApi.Tester.Presentation.ComponentModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using Prism.Mvvm;

    /// <summary>
    /// 入力値を検証する機能を備えた<see cref="ViewModelBase" /> クラスです。
    /// </summary>
    public abstract class ValidatableViewModelBase : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly ErrorsContainer<string> _errorsContainer;

        private readonly object _lock = new object();

        protected ValidatableViewModelBase()
        {
            _errorsContainer = new ErrorsContainer<string>(OnErrorsChanged);
        }

        /// <summary>Gets a value that indicates whether the entity has validation errors.</summary>
        /// <returns>
        /// <see langword="true" /> if the entity currently has validation errors; otherwise, <see langword="false" />.
        /// </returns>
        public bool HasErrors => _errorsContainer.HasErrors;

        public bool IsValid => HasErrors;

        /// <summary>Gets the validation errors for a specified property or for the entire entity.</summary>
        /// <param name="propertyName">
        /// The name of the property to retrieve validation errors for; or <see langword="null" /> or
        /// <see cref="F:System.String.Empty" />, to retrieve entity-level errors.
        /// </param>
        /// <returns>The validation errors for the property or entity.</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsContainer.GetErrors(propertyName);
        }

        /// <summary>Occurs when the validation errors have changed for a property or for the entire entity.</summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnErrorsChanged(string propertyName)
        {
            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">
        /// Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.
        /// </param>
        /// <returns>
        /// True if the value was changed, false if the existing value matched the
        /// desired value.
        /// </returns>
        protected override bool SetProperty<T>(ref T                     storage
                                             , T                         value
                                             , [CallerMemberName] string propertyName = null)
        {
            var changed = base.SetProperty(ref storage, value, propertyName);
            if (changed) ValidateProperty(value, propertyName);

            return changed;
        }

        protected void ValidateProperty(object                    value
                                      , [CallerMemberName] string propertyName = null)
        {
            lock (_lock)
            {
                var context = new ValidationContext(this, null, null)
                              {
                                  MemberName = propertyName
                              };
                var validationResults = new List<ValidationResult>();
                if (Validator.TryValidateProperty(value, context, validationResults))
                {
                    _errorsContainer.ClearErrors(propertyName);
                }
                else
                {
                    var errors = validationResults.Select(x => x.ErrorMessage);
                    _errorsContainer.SetErrors(propertyName, errors);
                }
            }
        }

        protected void Validate()
        {
            lock (_lock)
            {
                var context           = new ValidationContext(this, null, null);
                var validationResults = new List<ValidationResult>();
                var valid             = Validator.TryValidateObject(this, context, validationResults, true);

                _errorsContainer.ClearErrors();

                if (!valid)
                {
                    var byPropertyNames = from result in validationResults
                                          from memberName in result.MemberNames
                                          group result by memberName
                                          into g
                                          select g;
                    foreach (var property in byPropertyNames)
                    {
                        _errorsContainer.SetErrors(property.Key, property.Select(x => x.ErrorMessage).ToArray());
                        OnErrorsChanged(property.Key);
                    }
                }

                OnValidate();
            }
        }

        protected virtual void OnValidate()
        {
        }

        protected void NotifyError<TProperty>(Expression<Func<TProperty>> propertyExpression
                                            , string                      message)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message));

            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            NotifyError(propertyName, message);
        }

        protected void NotifyError(string propertyName
                                 , string message)
        {
            var errors      = _errorsContainer.GetErrors();
            var containsKey = errors.ContainsKey(propertyName);
            if (containsKey)
            {
                errors[propertyName].Add(message);
            }
            else
            {
                errors.Add(propertyName, new List<string> {message});
            }

            OnErrorsChanged(propertyName);
        }

        protected void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }
    }
}