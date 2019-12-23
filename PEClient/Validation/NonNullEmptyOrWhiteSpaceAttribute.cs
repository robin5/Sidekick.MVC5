using PEClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PEClient.Validation
{
    public class NonNullEmptyOrWhiteSpaceAttribute:ValidationAttribute
    {
        private string _errorMessage = null;
        public NonNullEmptyOrWhiteSpaceAttribute(string errorMessage = null)
        {
            _errorMessage = errorMessage;
        }

        public override bool IsValid(object value)
        {
            string val;
            IEnumerable<string> vals;

            // Validate a string
            if (null != (val = value as string))
            {
                return !string.IsNullOrWhiteSpace(val);
            } 
            // Validate an array of strings
            else if (null != (vals = value as IEnumerable<string>))
            {
                foreach (string val2 in vals)
                {
                    if (string.IsNullOrWhiteSpace(val2)) { return false; }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return _errorMessage ?? $"{name} contains a blank entry";
        }
    }
}