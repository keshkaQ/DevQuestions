namespace DevQuestions.Domain.Reports;

public enum ReportStatus
{
    /// <summary>
    ///  Статусы, открыт
    /// </summary>
    Open,
    /// <summary>
    ///  Статус в работе
    /// </summary>
    InProgress, 
    /// <summary>
    /// Статус решен
    /// </summary>
    Resolved, 
    /// <summary>
    /// Статус отменен
    /// </summary>
    Dismissed
}