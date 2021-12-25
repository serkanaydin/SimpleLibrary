namespace SimpleLibrary.Core.Enum;

public enum DeactivateResult
{
    DeactivateSuccessful,
    ActiveUserDoesntExistWithUserId,
    SaveChangesFault,
    UnauthorizedDeactivation
}

public enum LoginResult
{
    SuccessfulLogin,
    NoUserWithTheUsername,
    WrongPassword,
    LockedOut,
    NotAllowed
}

public enum RegistrationResult
{   
    RegistrationSuccessful,
    UsernameAlreadyExist,
    RegistrationUnsuccessful
}
