using Domain.CellDomain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Domain.CellDomain.Entities
{
    public class CellPlan
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? TitleComment { get; set; } 
        public string Local { get; set; } = string.Empty;
        public string? LocalComment { get; set; }
        public DateTime Date { get; set; }
        public string? DateComment { get; set; }
        public TimeSpan Duration { get; set; }
        public string? DurationComment { get; set; }
        public string Mode { get; set; } = string.Empty;
        public string? ModeComment { get; set; }
        public string Synopsis { get; set; } = string.Empty;
        public string? SynopsisComment { get; set; }
        public string Justification { get; set; } = string.Empty;
        public string? JustificationComment { get; set; }
        public string TargetAudience { get; set; } = string.Empty;
        public string? TargetAudienceComment { get; set; }
        public string Activities { get; set; } = string.Empty;
        public string? ActivitiesComment { get; set; }
        public string Tools { get; set; } = string.Empty;
        public string? ToolsComment { get; set; }
        public string ResultIndicators { get; set; } = string.Empty;
        public string? ResultIndicatorsComment { get; set; }
        public string MeansOfVerification { get; set; } = string.Empty;
        public string? MeansOfVerificationComment { get; set; }

        private void ValidateStateCellPlan()
        {
            if (string.IsNullOrWhiteSpace(Title)            ||
                string.IsNullOrWhiteSpace(Local)            ||
                string.IsNullOrWhiteSpace(Mode)             ||
                string.IsNullOrWhiteSpace(Synopsis)         ||
                string.IsNullOrWhiteSpace(Justification)    ||
                string.IsNullOrWhiteSpace(TargetAudience)   ||
                string.IsNullOrWhiteSpace(Activities)       ||
                string.IsNullOrWhiteSpace(Tools)            ||
                string.IsNullOrWhiteSpace(ResultIndicators) ||
                string.IsNullOrWhiteSpace(MeansOfVerification))
            {
                throw new CellMissingRequiredInformationException();
            }
        }

        public bool IsValidate()
        {
            ValidateStateCellPlan();
            return true;
        }

        public bool PlanIsCompleted()
        {
            if (!string.IsNullOrEmpty(TitleComment)              ||
                !string.IsNullOrEmpty(LocalComment)              ||      
                !string.IsNullOrEmpty(ModeComment)               ||          
                !string.IsNullOrEmpty(SynopsisComment)           ||
                !string.IsNullOrEmpty(JustificationComment)      ||
                !string.IsNullOrEmpty(ToolsComment)              ||
                !string.IsNullOrEmpty(TargetAudienceComment)     ||
                !string.IsNullOrEmpty(ResultIndicatorsComment)   ||
                !string.IsNullOrEmpty(DateComment)               ||
                !string.IsNullOrEmpty(ActivitiesComment)         ||
                !string.IsNullOrEmpty(DurationComment)           ||
                !string.IsNullOrEmpty(MeansOfVerificationComment)) return false;
            return true;
        }
    }
}
