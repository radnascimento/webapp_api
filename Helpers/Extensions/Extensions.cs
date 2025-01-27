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
                Name = dto.Name
            };
        }


        public static Study ToStudy(this StudyDto dto)
        {
            return new Study
            {
                IdStudy = dto.IdStudy,
                IdTopic = dto.IdTopic,
                Note = dto.Note,
                OperationDate = dto.OperationDate

            };
        }
    }
}
