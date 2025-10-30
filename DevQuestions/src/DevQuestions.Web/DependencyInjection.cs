using DevQuestions.Application;
using DevQuestions.Application.Questions;
using FluentValidation;

namespace DevQuestions.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(this IServiceCollection services)
    {
        return services.AddAWebDependencies().AddApplication();
    }

    private static IServiceCollection AddAWebDependencies(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddControllers();
        return services;
    }
}