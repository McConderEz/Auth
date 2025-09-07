using System.Text.RegularExpressions;
using SharedKernel.Shared.Objects;

namespace SharedKernel.Shared.ValueObjects;

public class FullName : ValueObject
{

    private static readonly Regex ValidationRegex = new Regex(
        @"^[\p{L}\p{M}\p{N}]{1,50}\z",
        RegexOptions.Singleline | RegexOptions.Compiled);

    public string FirstName { get; }
    public string SecondName { get; }
    public string? Patronymic { get; } 

    private FullName(){}
    private FullName(string firstName, string secondName, string? patronymic)
    {
        FirstName = firstName;
        SecondName = secondName;
        Patronymic = patronymic;
    }

    public void Deconstruct(out string firstName, out string secondName, out string? patronymic)
    {
        firstName = FirstName;
        secondName = SecondName;
        patronymic = Patronymic;
    }
    
    public static Result<FullName> Create(string firstName,string secondName, string? patronymic)
    {
        if (string.IsNullOrWhiteSpace(firstName) || !ValidationRegex.IsMatch(firstName))
            return Errors.Errors.General.ValueIsInvalid(firstName);

        if (string.IsNullOrWhiteSpace(secondName) || !ValidationRegex.IsMatch(secondName))
            return Errors.Errors.General.ValueIsInvalid(secondName);

        return new FullName(firstName, secondName, patronymic);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return SecondName;
        yield return Patronymic;
    }

    public override string ToString()
    {
        return $"{SecondName} {FirstName} {Patronymic}";
    }
}