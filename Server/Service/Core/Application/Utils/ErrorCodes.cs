namespace Application.Utils
{
    public enum ErrorCodes
    {
        // User realated codes 1 to 99
        USER_NOT_FOUND = 1,
        USER_EMAIL_ALREADY_EXISTS = 2,
        USER_NAME_ALREADY_EXISTS = 3,
        USER_MISSING_REQUIRED_INFORMATION = 4,
        USER_INVALID_EMAIL = 5,
        USER_COULD_NOT_BE_STORAGE = 6,

        // Articulator related codes 100 to 199
        ARTICULATOR_NOT_FOUND = 100,
        ARTICULATOR_EMAIL_ALREADY_EXISTS = 101,
        ARTICULATOR_NAME_ALREADY_EXISTS = 102,
        ARTICULATOR_MISSING_REQUIRED_INFORMATION = 103,
        ARTICULATOR_INVALID_EMAIL = 104,
        ARTICULATOR_COULD_NOT_BE_STORAGE = 105,
    }
}
