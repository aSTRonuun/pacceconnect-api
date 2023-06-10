using Application.Utils.IDtoBase;
using Domain.CellDomain.Entities;

namespace Application.CellApplication.Dtos
{
    public class CellPlanDto : IDto
    {
        public int Id { get; set; }
        public int CellId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? TitleComment { get; set; }
        public string Local { get; set; } = string.Empty;
        public string? LocalComment { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string? DateComment { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public string? DurationComment { get; set; } = string.Empty;
        public string Mode { get; set; } = string.Empty;
        public string? ModeComment { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;
        public string? SynopsisComment { get; set; } = string.Empty;
        public string Justification { get; set; } = string.Empty;
        public string? JustificationComment { get; set; } = string.Empty;
        public string TargetAudience { get; set; } = string.Empty;
        public string? TargetAudienceComment { get; set; } = string.Empty;
        public string Activities { get; set; } = string.Empty;
        public string? ActivitiesComment { get; set; } = string.Empty;
        public string Tools { get; set; } = string.Empty;
        public string? ToolsComment { get; set; } = string.Empty;
        public string ResultIndicators { get; set; } = string.Empty;
        public string? ResultIndicatorsComment { get; set; } = string.Empty;
        public string MeansOfVerification { get; set; } = string.Empty;
        public string? MeansOfVerificationComment { get; set; } = string.Empty;

        public static CellPlanDto MapToDto(CellPlan cellPlan)
        {
            return new CellPlanDto
            {
                Id = cellPlan.Id,
                Title = cellPlan.Title,
                TitleComment = cellPlan.TitleComment,
                Local = cellPlan.Local,
                LocalComment = cellPlan.LocalComment,
                Date = cellPlan.Date,
                DateComment = cellPlan.DateComment,
                Duration = cellPlan.Duration,
                DurationComment = cellPlan.DurationComment,
                Mode = cellPlan.Mode,
                ModeComment = cellPlan.ModeComment, 
                Synopsis = cellPlan.Synopsis,
                SynopsisComment = cellPlan.SynopsisComment,
                Justification = cellPlan.Justification,
                JustificationComment = cellPlan.JustificationComment,
                TargetAudience = cellPlan.TargetAudience,
                TargetAudienceComment = cellPlan.TargetAudienceComment,
                Activities = cellPlan.Activities,
                ActivitiesComment = cellPlan.ActivitiesComment,
                Tools = cellPlan.Tools,
                ToolsComment = cellPlan.ToolsComment,
                ResultIndicators = cellPlan.ResultIndicators,
                ResultIndicatorsComment = cellPlan.ResultIndicatorsComment,
                MeansOfVerification = cellPlan.MeansOfVerification,
                MeansOfVerificationComment = cellPlan.MeansOfVerificationComment
            };
        }

        public static CellPlan MapToEntity(CellPlanDto cellPlanDto) {

            return new CellPlan
            {
                Id = cellPlanDto.Id,
                Title = cellPlanDto.Title,
                TitleComment = cellPlanDto.TitleComment,
                Local = cellPlanDto.Local,
                LocalComment = cellPlanDto.LocalComment,
                Date = cellPlanDto.Date,
                DateComment = cellPlanDto.DateComment,
                Duration = cellPlanDto.Duration,
                DurationComment = cellPlanDto.DurationComment,
                Mode = cellPlanDto.Mode,
                ModeComment = cellPlanDto.ModeComment,
                Synopsis = cellPlanDto.Synopsis,
                SynopsisComment = cellPlanDto.SynopsisComment,
                Justification = cellPlanDto.Justification,
                JustificationComment = cellPlanDto.JustificationComment,
                TargetAudience = cellPlanDto.TargetAudience,
                TargetAudienceComment = cellPlanDto.TargetAudienceComment,
                Activities = cellPlanDto.Activities,
                ActivitiesComment = cellPlanDto.ActivitiesComment,
                Tools = cellPlanDto.Tools,
                ToolsComment = cellPlanDto.ToolsComment,
                ResultIndicators = cellPlanDto.ResultIndicators,
                ResultIndicatorsComment = cellPlanDto.ResultIndicatorsComment,
                MeansOfVerification = cellPlanDto.MeansOfVerification,
                MeansOfVerificationComment = cellPlanDto.MeansOfVerificationComment
            };
        }
    }
}
