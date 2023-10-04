using Application.DTO.User;
using Domain.Models.SurveyModels;
using Infrastracture.Helpers;

namespace Infrastracture.Contracts.Specifications.SurveySpecification;

public class SurveySpecification : BaseSpecification<Survey>
{
    
    public SurveySpecification(long Id,UserDTO userDto) : base(p=>p.Id.Equals(Id))
    {
        AddInclude("User");
        AddInclude("BirthArea");
        AddInclude("Education");
        AddInclude("ArmyRank");
        AddInclude("VTSh");
        AddInclude("Position");
        AddInclude("Area");
        AddInclude("Vacancy");
        AddInclude("StepGroup");
        AddInclude("CurrentStep");
        AddInclude("MedicalStatus");
        AddInclude("SurveyDrivers.DriverLicense");
        AddInclude("SurveyRelatives.Relative");
        AddInclude("Profiles");
        if (userDto.RoleId.Equals(AppConstant.DirectorRoleId) ||
            userDto.RoleId.Equals(AppConstant.ExecutorRoleId)
           )
        {
            AddInclude("Profiles.ConfirmedUser");
            AddInclude("Profiles.StepGroup");
            AddInclude("Profiles.Step");
            AddInclude("Profiles.RequestedUser");
            AddInclude("Profiles.ProfileFiles.User");
        }

        if (userDto.RoleId.Equals(AppConstant.UserRoleId))
        {
            AddInclude("Profiles.StepGroup");
            AddInclude("Profiles.Step");
            AddInclude("Profiles.ProfileFiles");
        }
        if (userDto.RoleId.Equals(AppConstant.KNBRoleId))
        {
            AddInclude("Profiles.StepGroup");
            AddInclude("Profiles.Step");
            AddInclude("Profiles.ProfileFiles");
            AddInclude("Profiles.ConfirmedUser");
            AddInclude("Profiles.ProfileFiles.User");
        }
        if (userDto.RoleId.Equals(AppConstant.MEDRoleId))
        {
            AddInclude("Profiles.StepGroup");
            AddInclude("Profiles.Step");
            AddInclude("Profiles.ProfileFiles");
            AddInclude("Profiles.ConfirmedUser");
            AddInclude("Profiles.ProfileFiles.User");
        }
        
    }
    
    public SurveySpecification(UserDTO userDto,int PageIndex,int PageSize,bool isPaging = true) : base(p=>p.UserId.Equals(userDto.Id))
    {
        AddInclude("User");
        AddInclude("BirthArea");
        AddInclude("Education");
        AddInclude("ArmyRank");
        AddInclude("VTSh");
        AddInclude("Position");
        AddInclude("Area");
        AddInclude("Vacancy");
        AddInclude("StepGroup");
        AddInclude("CurrentStep");
        AddInclude("MedicalStatus");
        AddInclude("SurveyDrivers.DriverLicense");
        AddInclude("SurveyRelatives.Relative");
        AddInclude("Profiles.Step");
        AddInclude("Profiles.StepGroup");
        AddInclude("Profiles.ConfirmedUser");
        AddInclude("Profiles.RequestedUser");
        AddInclude("Profiles.ProfileFiles.User");
        if (isPaging)
        {
            ApplyPaging(PageSize * (PageIndex - 1), PageSize);
        }
        AddOrderByDescending(p=> p.UpdatedAt);



    }
    
    
    
    
    
    
    
}