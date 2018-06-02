using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalaryWebApp.Validators
{
    public class CustomPositiveValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                int outVal;
                if (int.TryParse(value.ToString(), out outVal))
                {
                    if (outVal >= 0)
                        return true;

                }
            }
            return false;
        }
    }
}