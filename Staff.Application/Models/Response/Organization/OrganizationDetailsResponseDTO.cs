using Staff.Core.Entities.Organization;

namespace Staff.Application.Models.Response.Organization;

public class OrganizationDetailsResponseDto
{
    public long Id { get; set; }

    public string Name { get; set; }= string.Empty;

    public string Address { get; set; }= string.Empty;

    public string ContactNo { get; set; }= string.Empty;

    public string Email { get; set; } = string.Empty;

    public string ApiKey { get; set; } = string.Empty;
    
    public DateTime ExpireDate { get; set; }

    public OrganizationDetailsResponseDto MapToResponse(OrganizationDetails organizationDetails)
    {
        return new OrganizationDetailsResponseDto
        {
            Id = organizationDetails.Id,
            Name = organizationDetails.Name,
            Address = organizationDetails.Address,
            Email = organizationDetails.Email,
            ContactNo = organizationDetails.ContactNo,
            ApiKey = organizationDetails.ApiKey,
            ExpireDate = organizationDetails.ExpireDate
        };
    }

    public List<OrganizationDetailsResponseDto> MapToResponseList(List<OrganizationDetails> list)
    {
        return list.Select(MapToResponse).ToList();
    }
}