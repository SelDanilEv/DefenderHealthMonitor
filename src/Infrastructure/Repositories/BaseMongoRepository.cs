﻿using System.Linq.Expressions;
using Defender.HealthMonitor.Application.Configuration.Options;
using Defender.HealthMonitor.Application.Enums;
using Defender.HealthMonitor.Application.Helpers;
using Defender.HealthMonitor.Domain.Entities;
using Defender.HealthMonitor.Infrastructure.Enums;
using MongoDB.Driver;

namespace Defender.HealthMonitor.Infrastructure.Repositories;

public abstract class BaseMongoRepository<Model> where Model : IBaseModel, new()
{
    private static MongoClient client;
    private static IMongoDatabase database;
    protected IMongoCollection<Model> _mongoCollection;

    protected BaseMongoRepository(MongoDbOption mongoOption)
    {
        mongoOption.ConnectionString =
            String.Format(
                mongoOption.ConnectionString,
                EnvVariableResolver.GetEnvironmentVariable(EnvVariable.MongoDBPassword));

        client ??= new MongoClient(mongoOption.ConnectionString);

        database ??= client.GetDatabase($"{mongoOption.Environment}_{mongoOption.AppName}");

        _mongoCollection = database.GetCollection<Model>(typeof(Model).Name);
    }

    protected virtual Task<IList<Model>> GetItemsAsync()
    {
        throw new NotImplementedException();
    }

    protected virtual Task<IList<Model>> GetItemsWithFilterAsync(FilterDefinition<Model> filter)
    {
        throw new NotImplementedException();
    }

    protected virtual Task<Model> GetItemAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    protected virtual Task<Model> AddItemAsync(Model newModel)
    {
        throw new NotImplementedException();
    }

    protected virtual Task<Model> UpdateItemAsync(Model updatedModel)
    {
        throw new NotImplementedException();
    }

    protected virtual Task RemoveItemAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    protected FilterDefinition<Model> CreateIdFilter(Guid id)
    {
        return Builders<Model>.Filter.Eq(s => s.Id, id);
    }

    protected FilterDefinition<Model> CreateEqualFilterDefinition<TField>(
        Expression<Func<Model, TField>> field, TField value)
    {
        return Builders<Model>.Filter.Eq(field, value);
    }

    protected FilterDefinition<Model> CreateFilterDefinition(
        Expression<Func<Model, bool>> expression)
    {
        return Builders<Model>.Filter.Where(expression);
    }

    protected FilterDefinition<Model> MergeFilters(
        MongoFilterOperator filterOperator = MongoFilterOperator.AND,
        params FilterDefinition<Model>[] filters)
    {
        switch (filterOperator)
        {
            case MongoFilterOperator.AND:
                return Builders<Model>.Filter.And(new List<FilterDefinition<Model>>(filters));
            case MongoFilterOperator.OR:
                return Builders<Model>.Filter.Or(new List<FilterDefinition<Model>>(filters));
        }

        throw new ArgumentException("Mongo filter operator is not supported");
    }
}
