namespace FeelTags.WebApi.Dal
{
    public static class SqlFunctions
    {        
        public static double BiasRandomByDate(DateTime dateTime) => throw new NotImplementedException("This method is for use with Entity Framework Core only and has no in-memory implementation.");
        /*
         CREATE FUNCTION `BiasRandomByDate`(dateTime DATETIME)
RETURNS DOUBLE
DETERMINISTIC
BEGIN
    DECLARE result DOUBLE;
    SET result = RAND() / (DATEDIFF(CURDATE(), dateTime) + 1);
    RETURN result;
END
         */
    }
}
