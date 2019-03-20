using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Application.Extensions.MongoDB.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mongos.MongoAppService.Dto
{
    [AutoMapTo(typeof(MyTestModel))]
    public class MyTestDto : FullAuditedEntityDto<string> 
    { 
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
