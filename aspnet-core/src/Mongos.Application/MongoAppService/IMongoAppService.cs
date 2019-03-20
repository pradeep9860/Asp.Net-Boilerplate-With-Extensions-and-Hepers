using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Application.Extensions.MongoDB.Model;
using Application.Extensions.MongoDB.MongoDb.ServiceBase;
using MongoDB.Bson;
using Mongos.MongoAppService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mongos.MongoAppService
{
    public interface IMongoAppService  : IAsyncCrudMongoAppService<MyTestDto, ObjectId, PagedResultRequestDto, MyTestDto, MyTestDto>
    {
    }
}
