using Staff.Core.Entities.Organization;

namespace Staff.Application.Models.Response.Organization;

public class OrganizationDetailsResponseDto
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string ContactNo { get; set; }

    public string Email { get; set; }

    public OrganizationDetailsResponseDto MapToResponse(OrganizationDetails organizationDetails)
    {
        return new OrganizationDetailsResponseDto
        {
            Id = organizationDetails.Id,
            Name = organizationDetails.Name,
            Address = organizationDetails.Address,
            Email = organizationDetails.Email,
            ContactNo = organizationDetails.ContactNo,
        };
    }
}