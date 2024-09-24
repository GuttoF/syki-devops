namespace Syki.Back.Auth;

/// <summary>
/// Políticas de acesso aos endpoints da API.
/// </summary>
public static class BackPolicy
{
    public const string SkipUserRegister = nameof(SkipUserRegister);
    public const string CrossLogin = nameof(CrossLogin);
}
