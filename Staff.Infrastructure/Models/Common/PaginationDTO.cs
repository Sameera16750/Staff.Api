using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Staff.Infrastructure.Models.Common;

[DataContract]
public class PaginationDto
{
    [DataMember]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Minimum page number is 1")]
    public int PageNumber { get; set; } = 1;

    [DataMember]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Minimum page size is 1")]
    public int PageSize { get; set; } = 10;

    [DataMember]
    public string Search { get; set; } = string.Empty;
}