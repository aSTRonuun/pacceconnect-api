using Domain.CellDomain.Exceptions;
using Domain.CellDomain.Ports;
using System.ComponentModel.DataAnnotations;

namespace Domain.CellDomain.Entities
{
    public class CellPlan
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool TitleIsValid { get; set; }
        public string? TitleComment { get; set; } 
        public string Local { get; set; } = string.Empty;
        public bool LocalIsValid { get; set; }
        public string? LocalComment { get; set; }
        public DateTime Date { get; set; }
        public bool DateIsValid { get; set; }
        public string? DateComment { get; set; }
        public TimeSpan Duration { get; set; }
        public bool DurationIsValid { get; set; }
        public string? DurationComment { get; set; }
        public string Mode { get; set; } = string.Empty;
        public bool ModeIsValid { get; set; }
        public string? ModeComment { get; set; }
        public string Synopsis { get; set; } = string.Empty;
        public bool SynopsisIsValid { get; set; }
        public string? SynopsisComment { get; set; }
        public string Justification { get; set; } = string.Empty;
        public bool JustificationIsValid { get; set; }
        public string? JustificationComment { get; set; }
        public string TargetAudience { get; set; } = string.Empty;
        public bool TargetAudienceIsValid { get; set; }
        public string? TargetAudienceComment { get; set; }
        public string Activities { get; set; } = string.Empty;
        public bool ActivitiesIsValid { get; set; }
        public string? ActivitiesComment { get; set; }
        public string Tools { get; set; } = string.Empty;
        public bool ToolsIsValid { get; set; }
        public string? ToolsComment { get; set; }
        public string ResultIndicators { get; set; } = string.Empty;
        public bool ResultIndicatorsIsValid { get; set; }
        public string? ResultIndicatorsComment { get; set; }
        public string MeansOfVerification { get; set; } = string.Empty;
        public bool MeansOfVerificationIsValid { get; set; }
        public string? MeansOfVerificationComment { get; set; }

        private void ValidateStateCellPlan()
        {
            if (string.IsNullOrWhiteSpace(Title) ||
                string.IsNullOrWhiteSpace(Local) ||
                string.IsNullOrWhiteSpace(Mode) ||
                string.IsNullOrWhiteSpace(Synopsis) ||
                string.IsNullOrWhiteSpace(Justification) ||
                string.IsNullOrWhiteSpace(TargetAudience) ||
                string.IsNullOrWhiteSpace(Activities) ||
                string.IsNullOrWhiteSpace(Tools) ||
                string.IsNullOrWhiteSpace(ResultIndicators) ||
                string.IsNullOrWhiteSpace(MeansOfVerification))
            {
                throw new MissingRequiredInformationException();
            }
        }

        public bool IsValidate()
        {
            ValidateStateCellPlan();
            return true;
        }

        public bool PlanIsCompleted()
        {
            if (TitleIsValid &&
                LocalIsValid &&
                ModeIsValid &&
                SynopsisIsValid &&
                DateIsValid &&
                DurationIsValid &&
                JustificationIsValid &&
                TargetAudienceIsValid &&
                ActivitiesIsValid &&
                ToolsIsValid &&
                ResultIndicatorsIsValid &&
                MeansOfVerificationIsValid) return true;
            return false;
                
        }
    }
}
