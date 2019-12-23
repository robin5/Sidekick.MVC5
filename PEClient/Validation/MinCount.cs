using PEClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PEClient.Validation
{
    public class MinCountAttribute : ValidationAttribute
    {
        private string _errorMessage = null;
        private int _minCount = int.MinValue;
        public MinCountAttribute(int minCount, string errorMessage = null)
        {
            _minCount = minCount;
            _errorMessage = errorMessage ?? base.ErrorMessage;
        }
        public override bool IsValid(object value)
        {
            IEnumerable<object> vals;

            // Validate an array of strings
            if ((null != (vals = value as IEnumerable<object>)) &&
                (vals.Count() >= _minCount))
            {
                return true;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return _errorMessage ?? $"{name} is empty";
        }
    }
}