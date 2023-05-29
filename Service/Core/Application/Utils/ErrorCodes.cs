﻿namespace Application.Utils
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
        UNABLE_TO_ATHENTICATE_USER = 7,
        USER_INCORRECT_Credentials = 8,
        USER_LENGTH_IS_INVALID = 9,
        USER_PASSWORD_INCORRECT = 106,

        // Articulator related codes 100 to 199
        ARTICULATOR_NOT_FOUND = 100,
        ARTICULATOR_EMAIL_ALREADY_EXISTS = 101,
        ARTICULATOR_NAME_ALREADY_EXISTS = 102,
        ARTICULATOR_MISSING_REQUIRED_INFORMATION = 103,
        ARTICULATOR_INVALID_EMAIL = 104,
        ARTICULATOR_COULD_NOT_BE_STORAGE = 105,
        ARTICULATOR_COULD_NOT_BE_FOUND = 106,

        // Manager related codes 200 to 299
        MANAGER_NOT_FOUND = 200,
        MANAGER_COULD_NOT_BE_FOUND = 201,
        MANAGER_MISSING_REQUIRED_INFORMATION = 202,
        MANAGER_COULD_NOT_BE_STORAGE = 203,


        // Cell related codes 300 to 399
        CELL_COULD_NOT_BE_STORAGE = 300,
        CELLPLAN_COULD_NOT_BE_STORE = 301,
        CELL_NOT_FOUND = 302,
        CELL_MISSING_REQUIRED_INFORMATIONS = 303,
        CELL_MISSING_ARTICULATOR_INFORMATION = 304
    }
}
