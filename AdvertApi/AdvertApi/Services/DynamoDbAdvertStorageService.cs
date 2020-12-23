using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertApi.Models;
using AutoMapper;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon;

namespace AdvertApi.Services
{
    public class DynamoDbAdvertStorageService : IAdvertStorageServicecs

    {
        private readonly IMapper _mapper;
        public DynamoDbAdvertStorageService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<string> Add(AdvertModel model)
        {
            var dbModel = _mapper.Map<AdvertDbModel>(model);
            dbModel.Id = Guid.NewGuid().ToString();
            dbModel.CreationDateTime = DateTime.UtcNow;
            dbModel.Status = AdvertStatus.pending;
            AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
            // This client will access the US East 1 region.
            clientConfig.RegionEndpoint = RegionEndpoint.USEast2;
            using (var client = new AmazonDynamoDBClient(clientConfig))
            {
                using (var context = new DynamoDBContext(client))
                {
                    await context.SaveAsync(dbModel);
                }

            }
            return dbModel.Id;
        }

        public async Task Confirm(ConfirmAdvertModel model)
        {
            AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
            // This client will access the US East 1 region.
            clientConfig.RegionEndpoint = RegionEndpoint.USEast2;
            //AmazonDynamoDBClient client = new AmazonDynamoDBClient(clientConfig);

            using (var client = new AmazonDynamoDBClient(clientConfig))
            {
                using (var context = new DynamoDBContext(client))
                {
                    AdvertDbModel record = await context.LoadAsync<AdvertDbModel>(model.Id);
                
                        
                        if (record == null)
                    {
                        throw new KeyNotFoundException($"a RECORD WITH ID {model.Id} NOT EXISTS IN DB");

                    }

                    if (model.Status == AdvertStatus.Active)
                    {
                        record.Status= AdvertStatus.Active;
                        await context.SaveAsync(record);
                    }
                    else
                    {
                        await context.DeleteAsync(record);
                    }


                    }

                }

            }

        }

    }


