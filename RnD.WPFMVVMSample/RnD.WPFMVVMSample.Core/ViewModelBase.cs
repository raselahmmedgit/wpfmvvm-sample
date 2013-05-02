using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace RnD.WpfEfSample.Core
{

    public class ViewModelBase : DependencyObject, INotifyPropertyChanged, INotifyDataErrorInfo, IDataErrorInfo
    {
        protected int selectedIndex = 0; //used for list selected index
        protected DateTime minDate; //used for report page min date
        protected DateTime maxDate; //used for report page max date
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        IList<ValidationErrorInfo> error = new List<ValidationErrorInfo>();

        //Global Members for all ViewModel classes
        public long _currentUserID;
        public string _currentUser = null;

        public ViewModelBase()
        {
            this._currentUserID = 1;
            this._currentUser = "Rasel";
        }

        #region Implementation for INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Implementation for IDataErrorInfo

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Internal list of errors
        /// </summary>
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        /// <summary>
        /// Remove all errors for specified property name
        /// </summary>
        /// <param name="propertyName">Property whose errors are to be cleared</param>
        public void RemovePropertyErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
            }
        }

        /// <summary>
        /// Add an error via a property setter for the specified property
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <param name="error">Error message</param>
        public void AddPropertyError(string propertyName, string error)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors[propertyName].Add(error);
            }
            else
            {
                _errors.Add(propertyName, new List<string> { error });
            }
        }

        /// <summary>
        /// Indexer for evaluating whether a column (property)
        /// has errors.
        /// </summary>
        /// <param name="columnName">Column (property)</param>
        /// <returns>Error message(s)</returns>
        public virtual string this[string columnName]
        {
            get
            {
                var errors = new StringBuilder();
                var first = true;
                if (_errors.ContainsKey(columnName))
                {
                    foreach (var error in _errors[columnName])
                    {
                        if (first)
                        {
                            errors.Append(error);
                            first = false;
                        }
                        else
                        {
                            errors.AppendLine(error);
                        }
                    }
                }

                return errors.ToString();
            }
        }

        /// <summary>
        /// Clear all errors
        /// </summary>
        public void ClearPropertyErrors()
        {
            _errors.Clear();
        }

        #endregion

        public bool IsDesignedTime
        {
            get { return DesignerProperties.IsInDesignTool; }
        }

        private Dictionary<String, List<ValidationErrorInfo>> _errorslist = new Dictionary<string, List<ValidationErrorInfo>>();

        //protected void RemoveErrorFromProperty(string propertyName, int errorCode)
        //{
        //    //if (_errors.ContainsKey(propertyName))
        //    //{
        //    //    RemoveExistErrorsForProperty(propertyName, errorCode);
        //    //    NotifyErrorsChanged(propertyName);
        //    //}
        //}

        //private void RemoveExistErrorsForProperty(string propertyName, int errorCode)
        //{
        //    //if (_errors.ContainsKey(propertyName))
        //    //{
        //    //    var errorToRemove = _errors[propertyName].SingleOrDefault(error => error.ErrorCode == errorCode);

        //    //    if (errorToRemove != null)
        //    //    {
        //    //        _errors[propertyName].Remove(errorToRemove);
        //    //        if (_errors[propertyName].Count == 0) _errors.Remove(propertyName);
        //    //    }
        //    //}
        //}

        //protected void AddErrorsForProperty(string propertyName, ValidationErrorInfo errorInfo)
        //{
        //    RemoveErrorFromProperty(propertyName, errorInfo.ErrorCode);
        //    if (!_errors.ContainsKey(propertyName))
        //        _errors.Add(propertyName, new List<ValidationErrorInfo>());

        //    _errors[propertyName].Add(errorInfo);

        //    NotifyErrorsChanged(propertyName);
        //}

        protected void AddErrorsForProperty(string propertyName, IEnumerable<ValidationResult> validationResults)
        {
            RemoveErrorsForProperty(propertyName);

            if (!_errors.ContainsKey(propertyName))
                _errorslist.Add(propertyName, new List<ValidationErrorInfo>());


            ValidationErrorInfo objErrorInfo = null;
            foreach (var objValidation in validationResults)
            {
                objErrorInfo = new ValidationErrorInfo();
                objErrorInfo.ErrorMessage = objValidation.ErrorMessage;
                error.Add(objErrorInfo);
                _errorslist[propertyName].Add(objErrorInfo);
            }

            // FireErrorsChanged(propertyName);
            NotifyErrorsChanged(propertyName);


        }

        public void RemoveErrorsForProperty(string propertyName)
        {
            //if (_errors.ContainsKey(propertyName))
            //    _errors.Remove(propertyName);
        }

        protected bool ValidateProperty(object t, string propertyName, object value)
        {

            ValidationContext vctx = new ValidationContext(t, null, null)
            {
                MemberName = propertyName
            };
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateProperty(value, vctx, results);

            RemovePropertyErrors(propertyName);

            if (isValid == false)
                AddPropertyError(propertyName, results[0].ErrorMessage);
            return isValid;
        }

        public bool ValidateObject<T>(object t)
        {

            Type objectType = t.GetType();
            bool isValid = true;
            PropertyInfo[] properties = objectType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetCustomAttributes(typeof(ValidationAttribute), true).Any())
                {
                    object value = property.GetValue(t, null);
                    isValid = ValidateProperty(t, property.Name, value);
                }
            }

            if (!HasErrors)
            {
                if (objectType.GetCustomAttributes(typeof(ValidationAttribute), true).Any())
                {
                    var results = new List<ValidationResult>();
                    bool isvalid = Validator.TryValidateObject(t, new ValidationContext(t, null, null), results, true);
                    if (!isvalid)
                        //  ErrorsContainer.SetErrors("Email", results.Select(result => result.ErrorMessage).ToList());
                        NotifyErrorsChanged("Email");
                }
            }

            return isValid;
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (!_errors.ContainsKey(propertyName))
                return _errors.Values;

            return _errors[propertyName];
        }

        public bool HasErrors
        {
            get { return this._errors.Count > 0; }
        }

        protected void NotifyErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #region Validate Combobox Property
        /// <summary>
        /// This function used to validate combobox property
        /// </summary>
        /// <param name="obj">the object/entity which field validate for</param>
        /// <param name="propertyName">bind property name</param>
        /// <param name="message">Message which will show </param>
        /// <param name="valueProperty">validate against field</param>
        public void ValidateComboboxProperty(dynamic obj, string propertyName, string message, string valueProperty = "id")
        {
            if (obj != null)
            {
                // validate - even if no change    
                RemovePropertyErrors(propertyName);


                //Add Error according to property
                if (obj.GetType().GetProperty(valueProperty).GetValue(obj, null).ToString() == "0")
                {
                    AddPropertyError(propertyName, message);
                }
            }
        }

        #endregion

        #region Validate TextBox Property
        /// <summary>
        /// This function used to validate combobox property
        /// </summary>
        /// <param name="obj">the object/entity which field validate for</param>
        /// <param name="propertyName">bind property name</param>
        /// <param name="message">Message which will show </param>
        /// <param name="valueProperty">validate against field</param>
        public void ValidateTextBoxProperty(dynamic obj, string propertyName, string message, string valueProperty = "id")
        {
            if (obj != null)
            {
                // validate - even if no change    
                RemovePropertyErrors(propertyName);


                //Add Error according to property
                if (obj.ToString() == String.Empty)
                {
                    AddPropertyError(propertyName, message);
                }
            }
        }

        #endregion
    }

}
