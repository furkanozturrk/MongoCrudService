using System.ComponentModel.DataAnnotations;

namespace MongoCrudService.Dto
{
    public class DeleteRecordByIdDto
    {
        [Required]
        public string Id { get; set; }
    }
}
