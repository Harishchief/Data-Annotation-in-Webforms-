namespace DataAnnotation
{
    public enum ValidationType
    {
        Numeric = 1,
        Alphabetic,
        AlphaNumeric,
        Email,
        Url,
        Required,
        Range,
        MaxLength,
        CustomExpression
    }

    public enum DataType
    {
        Integer = 1,
        Varchar,
        DateTime,
        Boolean
    }
}