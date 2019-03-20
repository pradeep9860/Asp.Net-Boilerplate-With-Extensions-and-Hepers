using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto; 
using Application.Extensions.MongoDB.Model;
using Application.Extensions.MongoDB.MongoDb.ServiceBase;
using Application.Extensions.MongoDB.Repositories;
using Application.Extensions.RealTimeNotification.Publisher;
using MongoDB.Bson; 
using Mongos.MongoAppService.Dto;

namespace Mongos.MongoAppService
{
    public class MongoAppService  : AsyncCrudMongoAppService<MyTestModel, MyTestDto, ObjectId, PagedResultRequestDto, MyTestDto, MyTestDto>, IMongoAppService
    {
        private readonly IMongoRepository<MyTestModel, ObjectId> _repository;
        private readonly PublisherService _notificationPublisherService;

        public MongoAppService(IMongoRepository<MyTestModel, ObjectId> repository, PublisherService notificationPublisherService) 
            : base(repository)
        {
            _repository = repository;
            _notificationPublisherService = notificationPublisherService;
        }

        public async Task Test(string message)
        {
           await _notificationPublisherService.Publish_SentFrendshipRequest("Test User", message, new Abp.UserIdentifier(null, userId: (long)AbpSession.UserId));
        }
         
        //public async Task<PagedResultDto<MyTestDto>> GetAll(PagedResultRequestDto input)
        //{
        //    try
        //    {
        //        var res = _repository.GetAll();
        //        var result =  res.Select(x => new MyTestDto {
        //            Address = x.Address,
        //            Id = x.Id.ToString(),
        //            Name = x.Name
        //        }).ToList();
        //        return new PagedResultDto<MyTestDto>(result.Count, result);
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw;
        //    }

        //}

        //public async Task<MyTestDto> Create(MyTestDto input)
        //{
        //    try
        //    {
        //        var res = _repository.InsertAsync(new MyTestModel { 
        //            Name = input.Name,
        //            Address = input.Address
        //        });
        //        input.Id = res.Result.Id.ToString();
        //        return input;
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw;
        //    }

        //}

        //public async Task<MyTestDto> Update(MyTestDto input)
        //{
        //    try
        //    {
        //        var res = _repository.UpdateAsync(new MyTestModel
        //        {
        //            Id = new ObjectId(input.Id),
        //            Name = input.Name,
        //            Address = input.Address
        //        });

        //        return input;
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw;
        //    }

        //}
    }
}
