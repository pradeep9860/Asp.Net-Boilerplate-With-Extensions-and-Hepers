using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MongoDB.Bson;
using Mongos.MongoAppService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Extensions.MongoDB.Model
{
    [AutoMapFrom(typeof(MyTestDto))]
    public class MyTestModel : FullAuditedEntity<ObjectId> 
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
