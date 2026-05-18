using System.Windows.Input;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Bookings;

namespace Bookify.Application.Bookings.RescheduleBookingCommand;

public record RescheduleBookingCommand(Guid Id , Guid ApartmentId , DateOnly StartDate , DateOnly EndDate) : ICommand<bool>;
