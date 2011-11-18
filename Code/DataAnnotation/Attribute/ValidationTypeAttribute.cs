using System;

namespace DataAnnotation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ValidationTypeAttribute : System.Attribute
    {
        public ValidationTypeAttribute()
        {
        }

        public ValidationType Validation { get; set; }

        public string ErrorMessage { get; set; }

        public Int64 MaxLength { get; set; }

        public Int64 MinRange { get; set; }

        public Int64 MaxRange { get; set; }

        public string RegularExpression { get; set; }
    }
}