using Domain.Models.SurveyModels;
using Infrastracture.Contracts.Parameters.SurveyRepositories;

namespace Infrastracture.Contracts.Specifications.SurveySpecification;

public class SurveyStatsSpecification : BaseSpecification<Survey>
{

    public SurveyStatsSpecification(SurveyStatsParameters surveyStats,long? AreaId = null):base(
        p => 
                //(p.AreaId.Equals(AreaId)) &&
                (p.CurrentStepId != null) &&
                (!surveyStats.PositionId.HasValue || p.PositionId.Equals(surveyStats.PositionId)) &&
                (!surveyStats.VTShId.HasValue || p.VTShId.Equals(surveyStats.VTShId)) &&
                (!surveyStats.ArmyRankId.HasValue || p.ArmyRankId.Equals(surveyStats.ArmyRankId)) &&
                (!surveyStats.EducationId.HasValue || p.EducationId.Equals(surveyStats.EducationId)) &&
                (!surveyStats.DateFrom.HasValue || p.CreatedAt > surveyStats.DateFrom) &&
                (!surveyStats.DateTo.HasValue || p.CreatedAt > surveyStats.DateTo)
             )
    {
        
    }
    
}