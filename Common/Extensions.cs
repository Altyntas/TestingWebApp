namespace Common
{
    public static class Extensions
    {
        public static string GetDataType(this string data)
        {
            if (float.TryParse(data, out var floatType)) return "numeric(10,2)";
            if (Int32.TryParse(data, out var intType)) return "integer";
            if (DateTime.TryParse(data, out var dateTimeType)) return "timestamp";
            if (bool.TryParse(data, out var boolType)) return "boolean";
            if (Int64.TryParse(data, out var int64Type)) return "bigint";

            return "varchar(50)";
        }
    }
}
