using Order.Domain.Commons.Shared.ValueObjects;

namespace Order.Application.Orders.Dtos;

public class AddressDto
{
    public AddressDto(string firstName, string lastName, string emailAddress, string addressLine, string city, string country, string state, string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        City = city;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }

    public static Address MapToDomain(AddressDto dto)
    {
        return Address.New(
            firstName: dto.FirstName,
            lastName: dto.LastName,
            emailAddress: dto.EmailAddress,
            addressLine: dto.AddressLine,
            city: dto.City,
            country: dto.Country,
            state: dto.State,
            zipCode: dto.ZipCode);
    }

    public static AddressDto MapToDto(Address address)
    {
        return new AddressDto(
            firstName: address.FirstName,
            lastName: address.LastName,
            emailAddress: address.EmailAddress,
            addressLine: address.AddressLine,
            city: address.City,
            country: address.Country,
            state: address.State,
            zipCode: address.ZipCode
        );
    }

    public string FirstName { get; }
    public string LastName { get; }

    public string EmailAddress { get; }
    public string AddressLine { get; } 

    public string City { get; } 
    public string Country { get; } 

    public string State { get; }

    public string ZipCode { get; }
}
