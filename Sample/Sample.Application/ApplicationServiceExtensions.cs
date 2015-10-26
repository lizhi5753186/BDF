using System;
using System.Collections.Generic;
using AutoMapper;
using Bdf.Application.Services;
using Bdf.Domain.Entities;
using Bdf.Domain.Repositories;
using Bdf.Uow;

namespace Sample.Application
{
    public static class ApplicationServiceExtensions
    {
        public static bool IsEmptyGuidString(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return true;
            var guid = new Guid(s);
            return guid == Guid.Empty;
        }

        [UnitOfWork]
        public static TDtoList PerformCreateObjects<TDtoList, TDto, TEntity>(this ApplicationService applicationService,
          TDtoList dataTransferObjects,
          IRepository<TEntity, Guid> repository,
          Action<TDto> processDto = null,
          Action<TEntity> processEntity = null)
            where TDtoList : List<TDto>, new()
            where TEntity : class, IEntity<Guid>
        {
            if (dataTransferObjects == null)
                throw new ArgumentNullException("dataTransferObjects");
            if (repository == null)
                throw new ArgumentNullException("repository");
            var result = new TDtoList();
            if (dataTransferObjects.Count <= 0) return result;
            var ars = new List<TEntity>();

            foreach (var dto in dataTransferObjects)
            {
                if (processDto != null)
                    processDto(dto);
                var ar = Mapper.Map<TDto, TEntity>(dto);
                if (processEntity != null)
                    processEntity(ar);
                ars.Add(ar);
                repository.Insert(ar);
            }


            ars.ForEach(ar => result.Add(Mapper.Map<TEntity, TDto>(ar)));
            return result;
        }

        [UnitOfWork]
        public static TDtoList PerformUpdateObjects<TDtoList, TDataObject, TEntity>(this ApplicationService applicationService,
            TDtoList dataTransferObjects,
            IRepository<TEntity, Guid> repository,
            Func<TDataObject, string> idFunc,
            Action<TEntity, TDataObject> updateAction)
            where TDtoList : List<TDataObject>, new()
            where TEntity : class, IEntity<Guid>
        {
            if (dataTransferObjects == null)
                throw new ArgumentNullException("dataTransferObjects");
            if (repository == null)
                throw new ArgumentNullException("repository");
            if (idFunc == null)
                throw new ArgumentNullException("idFunc");
            if (updateAction == null)
                throw new ArgumentNullException("updateAction");
            TDtoList result = null;
            if (dataTransferObjects.Count <= 0) return null;
            result = new TDtoList();
            foreach (var dto in dataTransferObjects)
            {
                if (IsEmptyGuidString(idFunc(dto)))
                    throw new ArgumentNullException("Id");
                var id = new Guid(idFunc(dto));
                var ar = repository.Get(id);
                updateAction(ar, dto);
                repository.Update(ar);
                result.Add(Mapper.Map<TEntity, TDataObject>(ar));
            }
            return result;
        }

        [UnitOfWork]
        public static void PerformDeleteObjects<TEntity>(this ApplicationService applicationService,
            IList<Guid> ids, IRepository<TEntity, Guid> repository, Action<Guid> preDelete = null, Action<Guid> postDelete = null)
            where TEntity : class, IEntity<Guid>
        {
            if (ids == null)
                throw new ArgumentNullException("ids");
            if (repository == null)
                throw new ArgumentNullException("repository");
            foreach (var id in ids)
            {
                var guid = id;
                if (preDelete != null)
                    preDelete(guid);
                var ar = repository.Get(guid);
                repository.Delete(ar);
                if (postDelete != null)
                    postDelete(guid);
            }
        }
    }
}