using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using NetEOC.Shared.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Shared.Aws.DynamoDb
{
    public abstract class BaseDynamoRepository<T> where T : BaseDynamoModel, new()
    {
        public abstract string TableName { get; }

        static AmazonDynamoDBClient client { get; set; }

        static BaseDynamoRepository()
        {
            var configSection = ApplicationConfiguration.Configuration.GetSection("dynamodb");

            string awsAccessKeyId = configSection["awsAccessKeyId"];

            string awsSecretKey = configSection["awsSecretKey"];

            client = new AmazonDynamoDBClient(awsAccessKeyId, awsSecretKey, Amazon.RegionEndpoint.USEast1);
        }

        public virtual async Task<T> Create(T obj)
        {
            if (obj.Id == Guid.Empty) obj.Id = Guid.NewGuid();

            Document document = ConvertObjectToDocument(obj);

            Table table = GetTable(TableName);

            Document result = await table.PutItemAsync(document);

            return obj;
        }

        public virtual async Task<T> Get(Guid id)
        {
            Table table = GetTable(TableName);

            Document document = await table.GetItemAsync(id.ToString());

            if (document == null) return null;

            return ConvertDocumentToObject(document);
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            Table table = GetTable(TableName);

            Document document = await table.DeleteItemAsync(id.ToString());

            return true;
        }

        public virtual async Task<T> Update(T obj)
        {
            Table table = GetTable(TableName);

            Document document = ConvertObjectToDocument(obj);

            await table.UpdateItemAsync(document, obj.Id.ToString());

            return obj;
        }

        protected virtual async Task<T[]> GetByIndex(string index, string key, string value, int limit = int.MaxValue)
        {
            Table table = GetTable(TableName);

            QueryFilter filter = new QueryFilter(key, QueryOperator.Equal, value);

            QueryOperationConfig config = new QueryOperationConfig();

            config.Filter = filter;

            config.IndexName = index;

            config.Limit = limit;

            Search search = table.Query(config);

            List<Document> documents = new List<Document>();

            do
            {
                documents.AddRange(await search.GetNextSetAsync());
            } while (!search.IsDone);

            return !documents.Any() ? new T[0] : documents.Select(ConvertDocumentToObject).ToArray();
        }

        protected Table GetTable(string tableName)
        {
            return Table.LoadTable(client, tableName);
        }

        protected string ConvertToJson(Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        protected T ConvertFromJson(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                Error = delegate (object sender, ErrorEventArgs args)
                {
                    args.ErrorContext.Handled = true;
                },
            });
        }

        protected Document ConvertObjectToDocument(Object obj)
        {
            return Document.FromJson(ConvertToJson(obj));
        }

        protected T ConvertDocumentToObject(Document document)
        {
            return ConvertFromJson(document.ToJson());
        }
    }
}
