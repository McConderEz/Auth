using Core.DTOs.ValueObjects;

namespace Core.DTOs.Accounts;

public class UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? Photo { get; set; }
    public bool IsBanned { get; set; }
    public SocialNetworkDto[] SocialNetworks { get; set; } = [];
    public RoleDto[] Roles { get; set; } = [];
    public Guid? VolunteerAccountId { get; set; }
    public ParticipantAccountDto? ParticipantAccount { get; set; }
    public Guid? ParticipantAccountId { get; set; }
    public AdminProfileDto AdminProfile { get; set; }
    public Guid AdminProfileId { get; set; }
}
