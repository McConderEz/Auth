﻿using SharedKernel.Shared.Objects;

namespace SharedKernel.Shared.ValueObjects;

public class CreatedAt : ValueObject
{
    public DateTime Value { get; }
    
    private CreatedAt(){}

    private CreatedAt(DateTime value)
    {
        Value = value;
    }

    public static Result<CreatedAt> Create(DateTime value)
    {
        if (value > DateTime.Now)
            return Errors.Errors.General.ValueIsInvalid("created at");

        return new CreatedAt(value);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}