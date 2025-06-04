using System.ComponentModel.DataAnnotations;

namespace MongoCrudService.Dto
{
    public class InsertRecordDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Contact { get; set; }
        public double Salary { get; set; }
    }
}
