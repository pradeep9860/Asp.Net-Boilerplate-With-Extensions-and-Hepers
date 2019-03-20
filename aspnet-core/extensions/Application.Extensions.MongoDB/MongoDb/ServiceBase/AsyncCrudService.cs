using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Linq;
using Application.Extensions.MongoDB.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions.MongoDB.MongoDb.ServiceBase
{
    public abstract class AsyncCrudMongoAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput> : ApplicationService, ITransientDependency
        where TEntity : class, IEntity<TPrimaryKey>
       // where TEntityDto : IEntityDto<TPrimaryKey>
        //where TCreateInput : IEntityDto<TPrimaryKey>
       // where TUpdateInput : IEntityDto<TPrimaryKey>
    {
        private readonly IMongoRepository<TEntity, TPrimaryKey> _repository;
        protected AsyncCrudMongoAppService(IMongoRepository<TEntity, TPrimaryKey> repository) {
            _repository = repository;
        }

        public async virtual Task<ListResultDto<TEntityDto>> GetAll()
        {
            var result = await _repository.GetAllListAsync(); 
            return new ListResultDto<TEntityDto>(result.Select(x => result.MapTo<TEntityDto>()).ToList());
        }

        public async virtual Task<PagedResultDto<TEntityDto>> GetAllPaged(PagedResultRequestDto input) {
            var result = await _repository.GetAll().ToListAsync();
            return new PagedResultDto<TEntityDto>(result.Count, result.Select(x => result.MapTo<TEntityDto>()).ToList());
        }

        public async virtual Task<TEntityDto> GetEntityByIdAsync(TPrimaryKey id)
        {
            var result = await _repository.GetAsync(id);
            return result.MapTo<TEntityDto>(); 
        }

        public async virtual Task<TEntityDto> Create(TCreateInput input)
        {
            await _repository.InsertAsync(input.MapTo<TEntity>()); 
            return input.MapTo<TEntityDto>();
        }

        public async virtual Task<TEntityDto> Update(TUpdateInput input)
        {
             await _repository.UpdateAsync(input.MapTo<TEntity>());
            return input.MapTo<TEntityDto>();
        }

        public async virtual Task Delete(TPrimaryKey id)
        {
            await _repository.DeleteAsync(id);
        }
    }
        

    public interface IAsyncCrudMongoAppService<TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput, in TUpdateInput>  
        where TEntityDto : class 
    {
        Task<ListResultDto<TEntityDto>> GetAll();
        Task<PagedResultDto<TEntityDto>> GetAllPaged(PagedResultRequestDto input);
        Task<TEntityDto> GetEntityByIdAsync(TPrimaryKey id);
        Task<TEntityDto> Create(TCreateInput input);
        Task<TEntityDto> Update(TUpdateInput input);
        Task Delete(TPrimaryKey id);
    }
}
