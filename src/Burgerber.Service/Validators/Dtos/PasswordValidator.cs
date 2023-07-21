namespace Burgerber.Service.Validators.Dtos;

public class PasswordValidator
{
    public static string Symbols { get; } = "!@#$%^&*()_+-={[}]|\\;:\"?><.,/~";

    public static (bool isValid, string Message) isStrongPassword(string password)
    {
        if (password.Length < 8) return (isValid: false, Message: "password uzunligi 8 tadan kam bo'lmasligi kerak");

        bool isUpperCaseExists=false;
        bool isLowerCaseExists=false;
        bool isNumberExists=false;
        bool isCharacterExists=false;

        foreach (var item in password)
        {
            if (char.IsUpper(item)) isUpperCaseExists = true;
            if(char.IsLower(item)) isLowerCaseExists = true;
            if(char.IsDigit(item)) isNumberExists = true;
            if(Symbols.Contains(item)) isCharacterExists = true;
        }

        if (isUpperCaseExists == false) return (isValid: false, Message: "Password da kamida 1ta katta harf bo'lishi shart");
        if (isLowerCaseExists == false) return (isValid: false, Message: "Password da kamida 1ta kichik harf bo'lishi shart");
        if (isNumberExists == false) return (isValid: false, Message: "Password da kamida 1ta raqam bo'lishi shart");
        if (isCharacterExists == false) return (isValid: false, Message: "Password da kamida 1ta belgi bo'lishi shart. Misol uchun (@#$%^<>&*)");

        return (isValid: true, "");
    }
}
