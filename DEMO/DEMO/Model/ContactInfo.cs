using System;
using DataAnnotation;

namespace DEMO.Model
{
    public class ContactInfo
    {
        [ValidationType(Validation = ValidationType.Required, ErrorMessage = "First Name is required")]
        [ValidationType(Validation = ValidationType.MaxLength, MaxLength = 50)]
        public string FirstName { get; set; }

        [ValidationType(Validation = ValidationType.MaxLength, MaxLength = 50)]
        public string LastName { get; set; }

        [ValidationType(Validation = ValidationType.Required)]
        [ValidationType(Validation = ValidationType.Range, MinRange = 1000000000, MaxRange = 9999999999)]
        [ValidationType(Validation = ValidationType.MaxLength, MaxLength = 10)]
        public Int64 Mobile { get; set; }

        [ValidationType(Validation = ValidationType.Required)]
        [ValidationType(Validation = ValidationType.MaxLength, MaxLength = 40)]
        [ValidationType(Validation = ValidationType.Email)]
        public string Email { get; set; }

        [ValidationType(Validation = ValidationType.Required)]
        [ValidationType(Validation = ValidationType.CustomExpression, RegularExpression = "([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.][0-9]{4}")]
        public DateTime Birthday { get; set; }

        [ValidationType(Validation = ValidationType.Required)]
        [ValidationType(Validation = ValidationType.Url)]
        public string URL { get; set; }
    }
}