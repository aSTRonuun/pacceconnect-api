using Application.Utils.IDtoBase;
using Domain.CellDomain.Entities;

namespace Application.CellApplication.Dtos
{
    public class CellPlanDto : IDto
    {
        public CellPlanDto()
        {
            TitleIsValid = false;
            LocalIsValid = false;
            DateIsValid = false;
            DurationIsValid = false;
            ModeIsValid = false;
            SynopsisIsValid = false;
            JustificationIsValid = false;
            TargetAudienceIsValid = false;
            ToolsIsValid = false;
            ActivitiesIsValid = false;
            ResultIndicatorsIsValid = false;
            MeansOfVerificationIsValid = false;
        }
        public int Id { get; set; }
        public int CellId { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool TitleIsValid { get; set; }
        public string? TitleComment { get; set; } = string.Empty;
        public string Local { get; set; } = string.Empty;
        public bool LocalIsValid { get; set; } 
        public string? LocalComment { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool DateIsValid { get; set; }
        public string? DateComment { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public bool DurationIsValid { get; set; }
        public string? DurationComment { get; set; } = string.Empty;
        public string Mode { get; set; } = string.Empty;
        public bool ModeIsValid { get; set; }
        public string? ModeComment { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;
        public bool SynopsisIsValid { get; set; }
        public string? SynopsisComment { get; set; } = string.Empty;
        public string Justification { get; set; } = string.Empty;
        public bool JustificationIsValid { get; set; }
        public string? JustificationComment { get; set; } = string.Empty;
        public string TargetAudience { get; set; } = string.Empty;
        public bool TargetAudienceIsValid { get; set; }
        public string? TargetAudienceComment { get; set; } = string.Empty;
        public string Activities { get; set; } = string.Empty;
        public bool ActivitiesIsValid { get; set; }
        public string? ActivitiesComment { get; set; } = string.Empty;
        public string Tools { get; set; } = string.Empty;
        public bool ToolsIsValid { get; set; }
        public string? ToolsComment { get; set; } = string.Empty;
        public string ResultIndicators { get; set; } = string.Empty;
        public bool ResultIndicatorsIsValid { get; set; }
        public string? ResultIndicatorsComment { get; set; } = string.Empty;
        public string MeansOfVerification { get; set; } = string.Empty;
        public bool MeansOfVerificationIsValid { get; set; }
        public string? MeansOfVerificationComment { get; set; } = string.Empty;



        public static CellPlanDto MapToDto(CellPlan cellPlan)
        {
            return new CellPlanDto
            {
                Id = cellPlan.Id,
                Title = cellPlan.Title,
                TitleIsValid = cellPlan.TitleIsValid,
                TitleComment = cellPlan.TitleComment,
                Local = cellPlan.Local,
                LocalIsValid = cellPlan.LocalIsValid,
                LocalComment = cellPlan.LocalComment,
                Date = cellPlan.Date,
                DateIsValid = cellPlan.DateIsValid,
                DateComment = cellPlan.DateComment,
                Duration = cellPlan.Duration,
                DurationIsValid = cellPlan.DurationIsValid,
                DurationComment = cellPlan.DurationComment,
                Mode = cellPlan.Mode,
                ModeIsValid = cellPlan.ModeIsValid,
                ModeComment = cellPlan.ModeComment,
                Synopsis = cellPlan.Synopsis,
                SynopsisIsValid = cellPlan.SynopsisIsValid,
                SynopsisComment = cellPlan.SynopsisComment,
                Justification = cellPlan.Justification,
                JustificationIsValid = cellPlan.JustificationIsValid,
                JustificationComment = cellPlan.JustificationComment,
                TargetAudience = cellPlan.TargetAudience,
                TargetAudienceIsValid = cellPlan.TargetAudienceIsValid,
                TargetAudienceComment = cellPlan.TargetAudienceComment,
                Activities = cellPlan.Activities,
                ActivitiesIsValid = cellPlan.ActivitiesIsValid,
                ActivitiesComment = cellPlan.ActivitiesComment,
                Tools = cellPlan.Tools,
                ToolsIsValid = cellPlan.ToolsIsValid,
                ToolsComment = cellPlan.ToolsComment,
                ResultIndicators = cellPlan.ResultIndicators,
                ResultIndicatorsIsValid = cellPlan.ResultIndicatorsIsValid,
                ResultIndicatorsComment = cellPlan.ResultIndicatorsComment,
                MeansOfVerification = cellPlan.MeansOfVerification,
                MeansOfVerificationIsValid = cellPlan.MeansOfVerificationIsValid,
                MeansOfVerificationComment = cellPlan.MeansOfVerificationComment
            };
        }

        public static CellPlan MapToEntity(CellPlanDto cellPlanDto) {

            return new CellPlan
            {
                Id = cellPlanDto.Id,
                Title = cellPlanDto.Title,
                TitleIsValid = cellPlanDto.TitleIsValid,
                TitleComment = cellPlanDto.TitleComment,
                Local = cellPlanDto.Local,
                LocalIsValid = cellPlanDto.LocalIsValid,
                LocalComment = cellPlanDto.LocalComment,
                Date = cellPlanDto.Date,
                DateIsValid = cellPlanDto.DateIsValid,
                DateComment = cellPlanDto.DateComment,
                Duration = cellPlanDto.Duration,
                DurationIsValid = cellPlanDto.DurationIsValid,
                DurationComment = cellPlanDto.DurationComment,
                Mode = cellPlanDto.Mode,
                ModeIsValid = cellPlanDto.ModeIsValid,
                ModeComment = cellPlanDto.ModeComment,
                Synopsis = cellPlanDto.Synopsis,
                SynopsisIsValid = cellPlanDto.SynopsisIsValid,
                SynopsisComment = cellPlanDto.SynopsisComment,
                Justification = cellPlanDto.Justification,
                JustificationIsValid = cellPlanDto.JustificationIsValid,
                JustificationComment = cellPlanDto.JustificationComment,
                TargetAudience = cellPlanDto.TargetAudience,
                TargetAudienceIsValid = cellPlanDto.TargetAudienceIsValid,
                TargetAudienceComment = cellPlanDto.TargetAudienceComment,
                Activities = cellPlanDto.Activities,
                ActivitiesIsValid = cellPlanDto.ActivitiesIsValid,
                ActivitiesComment = cellPlanDto.ActivitiesComment,
                Tools = cellPlanDto.Tools,
                ToolsIsValid = cellPlanDto.ToolsIsValid,
                ToolsComment = cellPlanDto.ToolsComment,
                ResultIndicators = cellPlanDto.ResultIndicators,
                ResultIndicatorsIsValid = cellPlanDto.ResultIndicatorsIsValid,
                ResultIndicatorsComment = cellPlanDto.ResultIndicatorsComment,
                MeansOfVerification = cellPlanDto.MeansOfVerification,
                MeansOfVerificationIsValid = cellPlanDto.MeansOfVerificationIsValid,
                MeansOfVerificationComment = cellPlanDto.MeansOfVerificationComment
            };
        }
    }
}
