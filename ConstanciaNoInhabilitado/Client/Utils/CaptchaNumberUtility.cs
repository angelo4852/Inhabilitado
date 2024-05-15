namespace ConstanciaNoInhabilitado.Client.Utils;

public abstract class CaptchaNumberUtility
{
    private static readonly Random Random = new Random();
    public static string GetCaptchaNumber(int length)
    {
        const string numbers = "0123456789";
        char[] captchaChars = new char[length];
        for (int i = 0; i < length; i++)
        {
            captchaChars[i] = numbers[Random.Next(numbers.Length)];
        }
        return new string(captchaChars);
    }
}