using Bookify.Domain.Abstractions;
using Bookify.Domain.Users.Events;
using Bookify.Domain.Users.Records;

namespace Bookify.Domain.Users;

public sealed class User : Entity
{
    private User(Guid id , FirstName firstName , LastName lastName , Email email) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }

    
    // factory method
    public static User Create(FirstName firstName, LastName lastName, Email email)
    {
        User entity = new User(Guid.NewGuid() , firstName, lastName, email);
        entity.RaiseDomainEvent(new UserCreatedDomainEvent(entity.Id));
        return entity;
    }
}