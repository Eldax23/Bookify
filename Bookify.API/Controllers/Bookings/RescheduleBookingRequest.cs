namespace Bookify.API.Controllers.Bookings;

public sealed record RescheduleBookingRequest(Guid BookingId , Guid ApartmentId , DateOnly StartDate, DateOnly EndDate);
