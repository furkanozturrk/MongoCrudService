using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoCrudService.Dto;
using MongoCrudService.Model;

namespace MongoCrudService.Data
{
    public class CrudOperationDL : ICrudOperationDL
    {
        private readonly IMongoCollection<UserDetails> _mongoCollection;
        private readonly IMapper _mapper;
        public CrudOperationDL(IMongoClient client, IOptions<DatabaseSettings> settings, IMapper mapper)
        {
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _mongoCollection = database.GetCollection<UserDetails>("UserDetails");
            _mapper = mapper;
        }
        public async Task<ResponseBase<bool>> InsertRecord(InsertRecordDto request)
        {
            try
            {
                var insertRecord = _mapper.Map<UserDetails>(request);
                insertRecord.CreatedDate = DateTime.Now.ToString();
                insertRecord.UpdatedDate = string.Empty;

                await _mongoCollection.InsertOneAsync(insertRecord);
            }
            catch (Exception ex)
            {
                return new ResponseBase<bool>(false, ex.Message);
            }
            return new ResponseBase<bool>(true, "Başarılı Bir Şekilde Kayıt Yapıldı.", true);
        }
        public async Task<ResponseBase<List<InsertRecordDto>>> GetAllRecords()
        {
            try
            {
                var insertRecordList = await _mongoCollection.Find(x => true).ToListAsync();

                if (insertRecordList.Count == 0)
                {
                    return new ResponseBase<List<InsertRecordDto>>(true, "Data Bulunamadı.");
                }
                return new ResponseBase<List<InsertRecordDto>>(true, "Başarılı Bir Şekilde Kayıt Getirildi.", _mapper.Map<List<InsertRecordDto>>(insertRecordList));
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<InsertRecordDto>>(false, ex.Message);
            }
        }
        public async Task<ResponseBase<InsertRecordDto>> GetRecordById(string id)
        {
            try
            {
                var insertRecord = await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (insertRecord == null)
                {
                    return new ResponseBase<InsertRecordDto>(false, "Data Bulunamadı.");
                }

                return new ResponseBase<InsertRecordDto>(true, "Başarılı Bir Şekilde Kayıt Getirildi.", _mapper.Map<InsertRecordDto>(insertRecord));
            }
            catch (Exception ex)
            {
                return new ResponseBase<InsertRecordDto>(false, ex.Message);
            }
        }
        public async Task<ResponseBase<List<InsertRecordDto>>> GetRecordByName(string name)
        {
            try
            {
                var insertRecord = await _mongoCollection.Find(x => (x.FirstName.ToLower() == name.ToLower()) || (x.LastName.ToLower() == name.ToLower())).ToListAsync();

                if (insertRecord.Count == 0)
                {
                    return new ResponseBase<List<InsertRecordDto>>(true, "Data Bulunamadı.");
                }
                return new ResponseBase<List<InsertRecordDto>>(true, "Başarılı Bir Şekilde Kayıt Getirildi.", _mapper.Map<List<InsertRecordDto>>(insertRecord));
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<InsertRecordDto>>(false, ex.Message);
            }
        }
        public async Task<ResponseBase<bool>> UpdateRecordById(UserDetails request)
        {
            try
            {
                var existingRecord = await _mongoCollection.Find(x => x.Id == request.Id).FirstOrDefaultAsync();
                if (existingRecord == null)
                {
                    return new ResponseBase<bool>(false, "Kayıt bulunamadı.");
                }

                request.UpdatedDate = DateTime.Now.ToString();

                var result = await _mongoCollection.ReplaceOneAsync(x => x.Id == request.Id, request);

                if (result.IsAcknowledged)
                {
                    return new ResponseBase<bool>(true, "Güncelleme başarıyla gerçekleştirildi.", true);
                }
                else
                {
                    return new ResponseBase<bool>(false, "Güncelleme yapılamadı.");
                }
            }
            catch (Exception ex)
            {
                return new ResponseBase<bool>(false, ex.Message);
            }
        }
        public async Task<ResponseBase<bool>> UpdateSalaryById(UpdateSalaryByIdDto updateSalaryByIdDto)
        {
            try
            {
                var update = Builders<UserDetails>.Update
                                .Set(x => x.Salary, updateSalaryByIdDto.Salary)
                                .Set(y => y.UpdatedDate, DateTime.Now.ToString());

                var result = await _mongoCollection.UpdateOneAsync(x => x.Id == updateSalaryByIdDto.Id, update);

                if (!result.IsAcknowledged)
                {
                    return new ResponseBase<bool>(true, "Güncelleme Gerçekleştirilemedi.");
                }
                return new ResponseBase<bool>(true, "Güncelleme başarıyla gerçekleştirildi.", true);
            }
            catch (Exception ex)
            {
                return new ResponseBase<bool>(false, ex.Message);
            }
        }
        public async Task<ResponseBase<bool>> DeleteRecordById(DeleteRecordByIdDto deleteRecordByIdDto)
        {
            try
            {
                var result = await _mongoCollection.DeleteOneAsync(x => x.Id == deleteRecordByIdDto.Id);

                if (!result.IsAcknowledged)
                {
                    return new ResponseBase<bool>(true, "Silme Gerçekleştirilemedi.");

                }
                return new ResponseBase<bool>(true, "Başarılı Bir Şekilde Kayıt Silindi.");
            }
            catch (Exception ex)
            {
                return new ResponseBase<bool>(false, ex.Message);
            }
        }
        public async Task<ResponseBase<bool>> DeleteAllRecord()
        {
            try
            {
                var result = await _mongoCollection.DeleteManyAsync(x => true);

                if (!result.IsAcknowledged)
                {
                    return new ResponseBase<bool>(false, "Silme Gerçekleştirilemedi.");
                }
                return new ResponseBase<bool>(true, "Başarılı Bir Şekilde Kayıtlar Silindi.");
            }
            catch (Exception ex)
            {
                return new ResponseBase<bool>(false, ex.Message);
            }
        }
    }
}