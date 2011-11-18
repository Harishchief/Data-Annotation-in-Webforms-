using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DataAnnotation
{
    public class ModelValidate
    {
        private List<string> _errors = new List<string>();

        public bool Validate<T>(T oModel)
        {
            Type model = oModel.GetType();
            string PropertyValue = string.Empty;
            foreach (PropertyInfo prop in model.GetProperties())
            {
                PropertyValue = GetPropertyValue(oModel, prop.Name);

                // Validation Type
                foreach (ValidationTypeAttribute attribute in prop.GetCustomAttributes(typeof(ValidationTypeAttribute), false))
                {
                    switch (attribute.Validation)
                    {
                        case ValidationType.Email:
                            if (PropertyValue != null)
                            {
                                if (!Regex.IsMatch(PropertyValue, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                                {
                                    _errors.Add(attribute.ErrorMessage == null ? string.Format(ErrorMessageFormat.Invalid, prop.Name) : attribute.ErrorMessage);
                                }
                            }
                            break;

                        case ValidationType.Required:
                            {
                                if (string.IsNullOrEmpty(PropertyValue))
                                {
                                    _errors.Add(attribute.ErrorMessage == null ? string.Format(ErrorMessageFormat.Required, prop.Name) : attribute.ErrorMessage);
                                }
                            }
                            break;

                        case ValidationType.Url:
                            if (PropertyValue != null)
                            {
                                if (!Regex.IsMatch(PropertyValue, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"))
                                {
                                    _errors.Add(attribute.ErrorMessage == null ? string.Format(ErrorMessageFormat.Invalid, prop.Name) : attribute.ErrorMessage);
                                }
                            }
                            break;

                        case ValidationType.MaxLength:
                            if (PropertyValue.Length > attribute.MaxLength)
                                _errors.Add(attribute.ErrorMessage == null ? string.Format(ErrorMessageFormat.Maxlength, prop.Name, attribute.MaxLength) : attribute.ErrorMessage);
                            break;

                        case ValidationType.Range:
                            if (Convert.ToInt64(PropertyValue) < attribute.MinRange || Convert.ToInt64(PropertyValue) > attribute.MaxRange)
                                _errors.Add(attribute.ErrorMessage == null ? string.Format(ErrorMessageFormat.Invalid, prop.Name) : attribute.ErrorMessage);

                            break;

                        case ValidationType.CustomExpression:
                            if (PropertyValue != null)
                            {
                                if (!Regex.IsMatch(PropertyValue, attribute.RegularExpression))
                                {
                                    _errors.Add(attribute.ErrorMessage == null ? string.Format(ErrorMessageFormat.Invalid, prop.Name) : attribute.ErrorMessage);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return !(_errors.Count > 0);
        }

        public string GetError(string seperator)
        {
            return string.Join(seperator, _errors.Select(x => x.ToString()).ToArray());
        }

        public bool IsColumnValidate(List<ColumnDefination> columns)
        {
            string template = string.Empty;
            foreach (ColumnDefination oColumnDefination in columns)
            {
                try
                {
                    switch (oColumnDefination.DataType)
                    {
                        case DataType.Integer:
                            template = ErrorMessageFormat.Numeric;
                            Convert.ToInt64(oColumnDefination.Value);
                            break;
                        case DataType.Varchar:
                            template = ErrorMessageFormat.AlphaNumeric;
                            Convert.ToString(oColumnDefination.Value);
                            break;
                        case DataType.DateTime:
                            template = ErrorMessageFormat.Invalid;
                            Convert.ToDateTime(oColumnDefination.Value);
                            break;
                        case DataType.Boolean:
                            template = ErrorMessageFormat.Boolean;
                            Convert.ToBoolean(oColumnDefination.Value);
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    _errors.Add(string.Format(template, oColumnDefination.Name));
                }
            }

            return !(_errors.Count > 0);
        }

        #region Helping Methods

        private string GetPropertyValue(object T, string propertyName)
        {
            string getPropertyValue = string.Empty;
            var pro = T.GetType().GetProperty(propertyName);
            if (pro != null)
            {
                getPropertyValue = pro.GetValue(T, null).ToString();
            }
            return getPropertyValue;
        }

        #endregion Helping Methods
    }
}