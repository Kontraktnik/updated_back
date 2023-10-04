using Domain.Models.SurveyModels;
using Infrastracture.Contracts.Parameters.SurveyRepositories;

namespace Infrastracture.Contracts.Specifications.SurveySpecification;

public class SurveyForManagementSpecification : BaseSpecification<Survey>
{
    public SurveyForManagementSpecification(SurveyForManagementParameters parameters,List<long>SurveyId,bool isPaging = true)
        :base(
            p =>
                (p.CurrentStepId != null) &&
                (SurveyId.Contains(p.Id)) &&
                (!parameters.PositionId.HasValue || p.PositionId.Equals(parameters.PositionId)) &&
                (!parameters.VTShId.HasValue || p.VTShId.Equals(parameters.VTShId)) &&
                (!parameters.ArmyRankId.HasValue || p.ArmyRankId.Equals(parameters.ArmyRankId)) &&
                (!parameters.EducationId.HasValue || p.EducationId.Equals(parameters.EducationId)) &&
                (!parameters.DateFrom.HasValue || p.CreatedAt > parameters.DateFrom) &&
                (!parameters.DateTo.HasValue || p.CreatedAt > parameters.DateTo) &&
                (!parameters.Status.HasValue || p.Status.Equals(parameters.Status)) &&
                (!parameters.StepId.HasValue || p.CurrentStepId.Equals(parameters.StepId)) &&
                (string.IsNullOrEmpty(parameters.Search) || 
                 p.FullName.ToLower().Contains(parameters.Search.ToLower().Trim()) ||
                 p.IIN.ToLower().Contains(parameters.Search.ToLower().Trim()) ||
                 p.Email.ToLower().Contains(parameters.Search.ToLower().Trim()) ||
                 p.Phone.ToLower().Contains(parameters.Search.ToLower().Trim()) ||
                 p.City.ToLower().Contains(parameters.Search.ToLower().Trim()) ||
                 p.Region.ToLower().Contains(parameters.Search.ToLower().Trim()) ||
                 p.Street.ToLower().Contains(parameters.Search.ToLower().Trim()) ||
                 p.Appartment.ToLower().Contains(parameters.Search.ToLower().Trim()) ||
                 p.Home.ToLower().Contains(parameters.Search.ToLower().Trim())
                )
        )
    {
        AddInclude("User");
        //AddInclude("BirthArea");
        //AddInclude("Education");
        //AddInclude("ArmyRank");
        //AddInclude("VTSh");
        //AddInclude("Position");
       // AddInclude("Area");
        //AddInclude("Vacancy");
        AddInclude("StepGroup");
        AddInclude("CurrentStep");
        AddInclude("Profiles");
        //AddInclude("MedicalStatus");
       // AddInclude("SurveyDrivers.DriverLicense");
        //AddInclude("SurveyRelatives.Relative");
       // AddInclude("Profiles.ConfirmedUser");
        //AddInclude("Profiles.RequestedUser");
        AddInclude("SurveyExecutor.Director");
        AddInclude("SurveyExecutor.Executor");
        if (isPaging)
        {
            ApplyPaging(parameters.PageSize * (parameters.PageIndex - 1), parameters.PageSize);
        }
            
    }
}