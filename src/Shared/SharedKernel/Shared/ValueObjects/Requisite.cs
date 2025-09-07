using System.Text.Json.Serialization;
using SharedKernel.Shared.Objects;

namespace SharedKernel.Shared.ValueObjects;

public class Requisite : ValueObject
{
    public string Title { get; } 
    public string Description { get; }
    
    private Requisite(){}
    
    [JsonConstructor]
    private Requisite(string title, string description)
    {
        Title = title;
        Description = description;
    }
    

    public static Result<Requisite> Create(string title, string description)
    {
        if(string.IsNullOrWhiteSpace(title) || title.Length > Constraints.Constraints.MAX_VALUE_LENGTH)
        {
            return Errors.Errors.General.ValueIsRequired(title);
        }
        
        if(string.IsNullOrWhiteSpace(description) || title.Length > Constraints.Constraints.MAX_VALUE_LENGTH)
        {
            return Errors.Errors.General.ValueIsRequired(description);
        }

        return new Requisite(title, description);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Title;
        yield return Description;
    }
}