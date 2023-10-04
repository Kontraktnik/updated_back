using Domain.Models.ProfileModels;

namespace Infrastracture.Contracts.Specifications.ProfileFileSpecification;

public class ProfileFileManagementSpecification : BaseSpecification<ProfileFile>
{
    public ProfileFileManagementSpecification(long ProfileId,long UserId) : base(p=>p.ProfileId.Equals(ProfileId) && p.UserId.Equals(UserId))
    {
        AddInclude("User");
    }
}