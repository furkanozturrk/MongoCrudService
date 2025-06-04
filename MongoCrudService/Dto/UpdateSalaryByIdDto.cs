using System.ComponentModel.DataAnnotations;

namespace MongoCrudService.Dto
{
    public class UpdateSalaryByIdDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public int Salary { get; set; }
    }
}
