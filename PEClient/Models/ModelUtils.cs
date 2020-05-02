using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class ModelUtils
    {
        public static string FormatExceptionMessage(Exception ex)
        {
            string msg = ex.Message;

            Exception innerException = ex.InnerException;
            while (innerException != null)
            {
                msg += ("\n" + ex.InnerException.Message);
                innerException = innerException.InnerException;
            }
            return msg;
        }
    }
}