using MongoCrudService.Dto;
using MongoCrudService.Model;

namespace MongoCrudService.Data
{
    public interface ICrudOperationDL
    {
        public Task<ResponseBase<bool>> InsertRecord(InsertRecordDto insertRecordDto);
        public Task<ResponseBase<List<InsertRecordDto>>> GetAllRecords();
        public Task<ResponseBase<InsertRecordDto>> GetRecordById(string id);
        public Task<ResponseBase<List<InsertRecordDto>>> GetRecordByName(string name);
        public Task<ResponseBase<bool>> UpdateRecordById(UserDetails request);
        public Task<ResponseBase<bool>> UpdateSalaryById(UpdateSalaryByIdDto updateSalaryByIdDto);
        public Task<ResponseBase<bool>> DeleteRecordById(DeleteRecordByIdDto deleteRecordByIdDto);
        public Task<ResponseBase<bool>> DeleteAllRecord();
    }
}
