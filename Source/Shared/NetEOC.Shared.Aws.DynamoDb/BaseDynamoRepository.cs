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

        private static AmazonDynamoDBClient client { get; set; }

        static BaseDynamoRepository()
        {
            var configSection = ApplicationConfiguration.Configuration.GetSection("DynamoDb");

            string awsAccessKeyId = configSection["awsAccessKeyId"];

            string awsSecretKey = configSection["awsSecretKey"];

            client = new AmazonDynamoDBClient(awsAccessKeyId, awsSecretKey, Amazon.RegionEndpoint.USEast1);
        }

        public virtual async Task<bool> Create(T obj)
        {
            if (obj.Id == Guid.Empty) obj.Id = Guid.NewGuid();

            Document document = ConvertObjectToDocument(obj);

            Table table = GetTable(TableName);

            Document result = await table.PutItemAsync(document);

            return true;
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

        public virtual async Task<bool> Update(T obj)
        {
            Table table = GetTable(TableName);

            Document document = ConvertObjectToDocument(obj);

            await table.UpdateItemAsync(document, obj.Id.ToString());

            return true;
        }

        private Table GetTable(string tableName)
        {
            return Table.LoadTable(client, tableName);
        }

        private string ConvertToJson(Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private T ConvertFromJson(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                Error = delegate (object sender, ErrorEventArgs args)
                {
                    args.ErrorContext.Handled = true;
                },
            });
        }

        private Document ConvertObjectToDocument(Object obj)
        {
            return Document.FromJson(ConvertToJson(obj));
        }

        private T ConvertDocumentToObject(Document document)
        {
            return ConvertFromJson(document.ToJson());
        }
    }
}
