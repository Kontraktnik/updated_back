namespace Infrastracture.Contracts.Parameters.SurveyRepositories;

public class SurveyStatsParameters
{
   public DateTime? DateFrom { get; set; }
   public DateTime? DateTo { get; set; }
   
   public long? ExecutorId { get; set; }

   public long? EducationId { get; set; }
   
   public long? ArmyRankId { get; set; }
   
   public long? VTShId { get; set; }
   
   public long? PositionId { get; set; }
   
   


}