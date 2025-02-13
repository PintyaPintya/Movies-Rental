namespace MoviesRental.Models;

public static class PasswordHasher
{
    public static string Encode(string plaintText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plaintText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string Decode(string encodedText)
    {
        var encodedBytes = System.Convert.FromBase64String(encodedText);
        return System.Text.Encoding.UTF8.GetString(encodedBytes);
    }
}
