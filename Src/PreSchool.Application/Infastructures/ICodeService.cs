namespace PreSchool.Application.Services
{
    public interface ICodeService
    {
        string AmountInWords(decimal number);
        string ConvertHoursToTotalDays(decimal hours);
        int Get4DigitsRandomNumber();
        string Get8Digits();
        string GetPaddedNumber(int id, int length = 3);
        string NumberToWords(int number);
    }
}