using Microsoft.AspNetCore.Mvc;
using MongoCrudService.Data;
using MongoCrudService.Dto;
using MongoCrudService.Model;

namespace MongoCrudService.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CrudOperationController : ControllerBase
    {
        private readonly ICrudOperationDL _crudOperationDL;
        public CrudOperationController(ICrudOperationDL crudOperationDL)
        {
            _crudOperationDL = crudOperationDL;
        }
        [HttpPost]
        public async Task<IActionResult> InsertRecord(InsertRecordDto ınsertRecordDto)
        {
            var res = await _crudOperationDL.InsertRecord(ınsertRecordDto);

            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRecord()
        {
            var res = await _crudOperationDL.GetAllRecords();

            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetRecordById([FromQuery] string Id)
        {
            var res = await _crudOperationDL.GetRecordById(Id);

            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetRecordByName([FromQuery] string Name)
        {
            var res = await _crudOperationDL.GetRecordByName(Name);

            return Ok(res);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRecordById(UserDetails request)
        {
            var res = await _crudOperationDL.UpdateRecordById(request);

            return Ok(res);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSalaryById(UpdateSalaryByIdDto updateSalaryByIdDto)
        {
            var res = await _crudOperationDL.UpdateSalaryById(updateSalaryByIdDto);

            return Ok(res);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRecordById(DeleteRecordByIdDto deleteRecordByIdDto)
        {
            var res = await _crudOperationDL.DeleteRecordById(deleteRecordByIdDto);

            return Ok(res);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAllRecord()
        {
            var res = await _crudOperationDL.DeleteAllRecord();

            return Ok(res);
        }
    }
}
