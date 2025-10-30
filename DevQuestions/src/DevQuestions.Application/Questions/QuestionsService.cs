using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DevQuestions.Application.Questions;

public class QuestionsService : IQuestionsService
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly ILogger<QuestionsService> _logger;
    private readonly IValidator<CreateQuestionDto> _validator;

    public QuestionsService(
        IQuestionsRepository questionsRepository, 
        ILogger<QuestionsService> logger,
        IValidator<CreateQuestionDto> validator)
    {
        _questionsRepository = questionsRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task <Guid> Create(CreateQuestionDto questionDto, CancellationToken cancellationToken)
    {
        // проверка валидности входных данных
        var validationResult = await _validator.ValidateAsync(questionDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        //  валидация бизнес-логики
        int openUserQuestionsCount = _questionsRepository.GetOpenUserQuestionsAsync(questionDto.UserId, cancellationToken).Result;
        if (openUserQuestionsCount > 3)
        {
            throw new Exception("Пользователь не может открыть больше 3 вопросов.");
        }
    // создание сущности Question
        var questionId = Guid.NewGuid();
        var question = new Question
           (questionId,
           questionDto.Title,
           questionDto.Text,
           questionDto.UserId,
           null,
           questionDto.TagIds);
        
        // сохранение сущности в БД
        await _questionsRepository.AddAsync(question, cancellationToken);
    
        // логирование 
        _logger.LogInformation("Created question with id {questionId}", questionId);
        return questionId;
    }
}