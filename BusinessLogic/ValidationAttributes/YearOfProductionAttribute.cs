using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ValidationAttributes
{
    public class YearOfProductionAttribute : ValidationAttribute
    {
        public int MinYear { get; }
        public int MaxYear { get; }

        public YearOfProductionAttribute(int minYear)
        {
            MinYear = minYear;
            MaxYear = DateTime.Now.Year; 
            ErrorMessage = $"Year of production must be between {minYear} and {MaxYear}.";
        }

        public override bool IsValid(object value)
        {
            if (value is int year)
            {
                return year >= MinYear && year <= MaxYear;
            }
            return false;
        }
    }
}
