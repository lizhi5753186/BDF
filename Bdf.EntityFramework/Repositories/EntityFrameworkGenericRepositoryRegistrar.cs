﻿using System;
using Bdf.Dependency;
using Bdf.Domain.Entities;
using Bdf.EntityFramework.Extensions;
using Bdf.Reflection.Extensions;

namespace Bdf.EntityFramework.Repositories
{
    public static class EntityFrameworkGenericRepositoryRegistrar
    {
        public static void RegisterForDbContext(Type dbContextType, IIocManager iocManager)
        {
            var autoRepositoryAttr = dbContextType.GetSingleAttributeOrNull<AutoRepositoryTypesAttribute>() ??
                                     AutoRepositoryTypesAttribute.Default;

            foreach (var entityType in dbContextType.GetEntityTypes())
            {
                foreach (var interfaceType in entityType.GetInterfaces())
                {
                    if (!interfaceType.IsGenericType || interfaceType.GetGenericTypeDefinition() != typeof (IEntity<>))
                        continue;
                    var primaryKeyType = interfaceType.GenericTypeArguments[0];
                    if (primaryKeyType == typeof(int))
                    {
                        var genericRepositoryType = autoRepositoryAttr.RepositoryInterface.MakeGenericType(entityType);
                        if (!iocManager.IsRegistered(genericRepositoryType))
                        {
                            var implType = autoRepositoryAttr.RepositoryImplementation.GetGenericArguments().Length == 1
                                ? autoRepositoryAttr.RepositoryImplementation.MakeGenericType(entityType)
                                : autoRepositoryAttr.RepositoryImplementation.MakeGenericType(dbContextType, entityType);

                            iocManager.Register(
                                genericRepositoryType,
                                implType,
                                DependencyLifeStyle.Transient
                                );
                        }
                    }

                    var genericRepositoryTypeWithPrimaryKey = autoRepositoryAttr.RepositoryInterfaceWithPrimaryKey.MakeGenericType(entityType, primaryKeyType);
                    if (!iocManager.IsRegistered(genericRepositoryTypeWithPrimaryKey))
                    {
                        var implType = autoRepositoryAttr.RepositoryImplementationWithPrimaryKey.GetGenericArguments().Length == 2
                            ? autoRepositoryAttr.RepositoryImplementationWithPrimaryKey.MakeGenericType(entityType, primaryKeyType)
                            : autoRepositoryAttr.RepositoryImplementationWithPrimaryKey.MakeGenericType(dbContextType, entityType, primaryKeyType);

                        iocManager.Register(
                            genericRepositoryTypeWithPrimaryKey,
                            implType,
                            DependencyLifeStyle.Transient
                            );
                    }
                }
            }
        }
    }
}