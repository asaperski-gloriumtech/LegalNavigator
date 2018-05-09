﻿using Access2Justice.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using Access2Justice.CosmosDbService.Models;
namespace Access2Justice.Repositories.Interface
{
    public interface ITopicRepository<TModel, in TPk>
    {    
            Task<IEnumerable<TModel>> GetTopicsFromCollectionAsync();
            Task<TModel> GetTopicsFromCollectionAsync(TPk id);         
        }
    }
