using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings;

public class BookingErrors
{
    public static Error NotFound = new Error("Booking.NotFound" , "Booking not found.");
    public static Error Overlap = new Error("Booking.Overlap" , "the current booking is overlapping with existent one.");
    public static Error NotReserved = new  Error("Booking.NotReserved" , "the current booking is not pending.");
    public static Error NotConfirmed = new  Error("Booking.NotConfirmed" , "the current booking is not confirmed.");
    public static Error AlreadyStarted = new  Error("Booking.AlreadyStarted" , "the current booking is already started.");
    public static Error NotPending = new  Error("Booking.NotPending" , "the current booking is not pending.");
    
}