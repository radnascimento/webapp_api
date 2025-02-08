using Api.Enums;
using Api.Models;
using Api.Models.Dtos;

namespace Api.Helpers.Extensions
{
    public static class Extensions
    {
        public static Material ToMaterial(this MaterialDto dto)
        {
            return new Material
            {
                IdTopic = dto.IdTopic,
                IdLevel = dto.IdLevel,
                Url = dto.Url,
                OperationDate = dto.OperationDate
            };
        }


        public static Level ToLevel(this LevelDto dto)
        {
            return new Level
            {
                Id = dto.Id,
                Description = dto.Description,
                Name = dto.Name
            };
        }

        public static Topic ToTopic(this TopicDto dto)
        {
            return new Topic
            {
                Id = dto.Id,
                Description = dto.Description,
                Name = dto.Name,
                IdUser = dto.IdUser,
                OperationDate = dto.OperationDate,
            };
        }

        public static Topic ToTopic(this EditTopicDto dto)
        {
            return new Topic
            {
                Id = dto.Id,
                Description = dto.Description,
                Name = dto.Name,
                IdUser = dto.IdUser,
                OperationDate = dto.OperationDate,
            };
        }


        public static Study ToStudy(this StudyDto dto)
        {
            return new Study
            {
                IdStudy = dto.IdStudy,
                IdTopic = dto.IdTopic,
                Note = dto.Note,
                OperationDate = dto.OperationDate,
                IdUser = dto.IdUser,
                IdStudyPC = (int)StudyEnum.Registered
            };
        }



        public static StudyReview ToStudy(this EditStudyReviewDto dto)
        {
            return new StudyReview
            {
                IdStudyReview = dto.IdStudyReview,
                IdStudy = dto.IdStudy,
                OperationDate = dto.OperationDate,
                IdStudyPC = dto.IdStudyPC,
            };
        }

        public static Study ToStudy(this EditStudyDto dto)
        {

            return new Study
            {
                IdStudy = dto.IdStudy,
                IdTopic = dto.IdTopic,
                Note = dto.Note,
                OperationDate = dto.OperationDate,
                IdUser = dto.IdUser,
                IdStudyPC = dto.IdStudyPC
            };
        }
        public static ApplicationConfig ToApplicationConfig(this ApplicationConfigDto dto)
        {
            return new ApplicationConfig
            {
                IdApplicationConfig = dto.IdApplicationConfig,
                Name = dto.Name,
                JsonContent = dto.JsonContent
            };
        }
    }
}
