using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertApi.Models;
using Amazon.DynamoDBv2.DataModel;


namespace AdvertApi.Services
{

    /// <summary>
    /// create a new data object in Db level with extra attribute than than teh actual advert model
    /// </summary>
    ///
    [DynamoDBTable("Adverts")]
    public class AdvertDbModel
    {
[DynamoDBHashKey]
        public string Id { get; set; }
        [DynamoDBProperty]
        public string Title { get; set; }
        [DynamoDBProperty]
        public string  Description { get; set; }
        [DynamoDBProperty]
        public double Price { get; set; }
        [DynamoDBProperty]
        public DateTime CreationDateTime { get; set; }
        [DynamoDBProperty]
        public AdvertStatus Status { get; set; }
    }
}
