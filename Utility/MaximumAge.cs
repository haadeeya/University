using System;
using System.ComponentModel.DataAnnotations;

namespace Utility
{
    public class MaximumAgeAttribute : ValidationAttribute
    {
        int _maximumAge;

        public MaximumAgeAttribute(int maximumAge)
        {
            _maximumAge = maximumAge;
        }

        public override bool IsValid(object value)
        {
            DateTime date;
            if (!DateTime.TryParse(value.ToString(), out date))return false;

            int year = int.Parse(DateTime.Parse(date.ToString()).Year.ToString());
            int currentyear = int.Parse(DateTime.Parse(DateTime.Now.ToString()).Year.ToString());
            if(currentyear - year > 70)return false;

            return true;
        }
    }
}
