using Api.Repository.Interface;
using Api.Repository;
using Api.Services;
using Api.Services.Interface;

namespace Api.Config
{
    public static class ServiceRegistration
    {
        public static void AddProjectServices(this IServiceCollection services)
        {
            // Register Repository Layer
            services.AddScoped<ITopicRepository, TopicRepository>();

            // Register Service Layer
            services.AddScoped<ITopicService, TopicService>();

            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddScoped<ILevelService, LevelService>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IStudyRepository, StudyRepository>();
            services.AddScoped<IStudyService, StudyService>();
            services.AddScoped<IApplicationConfigRepository, ApplicationConfigRepository>();
            services.AddScoped<IApplicationConfigService, ApplicationConfigService>();
            services.AddScoped<IStudyReviewService, StudyReviewService>();
            services.AddScoped<IStudyReviewRepository, StudyReviewRepository>();
            services.AddHostedService<ReviewCheckService>();
        }
    }
}

