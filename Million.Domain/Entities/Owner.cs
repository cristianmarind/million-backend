using System;
using Million.Domain.Exceptions;

namespace Million.Domain.Entities;

public class Owner {
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string Photo { get; private set; }
    public DateTime Birthday { get; private set; }

    public Owner(string name, string address, string photo, DateTime birthday) {
        if (string.IsNullOrWhiteSpace(name))
            throw new PropertyNotFoundException("Owner name is required");

        if (string.IsNullOrWhiteSpace(address))
            throw new PropertyNotFoundException("Owner address is required");

        if (birthday == default)
            throw new PropertyNotFoundException("Owner birthday must be valid");

        if (string.IsNullOrWhiteSpace(photo)) {
            throw new PropertyNotFoundException("Owner photo is required");
        }
        else if (!Uri.IsWellFormedUriString(photo, UriKind.Absolute)) {
            throw new PropertyContentInvalidException("Owner photo must be a valid URL");
        }
        
        if (birthday > DateTime.Now)
            throw new PropertyContentInvalidException("Owner birthday cannot be in the future");

        if (birthday > DateTime.Now.AddYears(-18))
            throw new PropertyContentInvalidException("Owner must be at least 18 years old");

        Name = name;
        Address = address;
        Photo = photo;
        Birthday = birthday;
    }
}