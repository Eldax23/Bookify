using Bookify.Application.Exceptions;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure;

public class ApplicationDbContext : DbContext , IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly); // automatically applies all configurations
        
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            int result = await base.SaveChangesAsync(cancellationToken);
            await PublishDomainEventsAsync();
            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency Exception Occurred", ex);
        }

    }

    public async Task PublishDomainEventsAsync()
    {
        // for each entity get its domain events , and publish all in the end.

        List<IDomainEvent> domainEvents = ChangeTracker.Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                // get the domain events of the entity
                IReadOnlyList<IDomainEvent> domainEvents = entity.GetDomainEvents();
                
                // after getting the domain events , clear them 
                entity.ClearDomainEvents();
                
                return domainEvents;
            })
            .ToList();
        
        // no we have a list of all the domain events occured in all entites
        // we need to handle them

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }


}