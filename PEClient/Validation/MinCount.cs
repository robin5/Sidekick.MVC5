using PEClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PEClient.Validation
{
    public class MinCountAttribute : ValidationAttribute
    {
        private string _errorMessage = null;
        private int _minCount = int.MinValue;
        public MinCountAttribute(int minCount, string ErrorMessage = null)
        {
            _minCount = minCount;
            _errorMessage = ErrorMessage ?? base.ErrorMessage;
        }
        public override bool IsValid(object value)
        {
            IEnumerable<int> iVals = value as IEnumerable<int>;
            IEnumerable<string> sVals = value as IEnumerable<string>;
            IEnumerable<object> oVals = value as IEnumerable<object>;
            IEnumerable<SelectListItem> sliVals = value as IEnumerable<SelectListItem>;

            // Validate an array of strings
            if ((null != iVals) && (iVals.Count() >= _minCount)) { return true; }
            else if ((null != sVals) && (sVals.Count() >= _minCount)) { return true; }
            else if ((null != oVals) && (oVals.Count() >= _minCount)) { return true; }
            else if ((null != sliVals) && (sliVals.Count() >= _minCount)) { return true; }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return _errorMessage ?? $"{name} is empty";
        }
    }
}