using Amazon.DynamoDBv2.DataModel;

namespace TestAmazon;

public class DynamoDb
{
    [DynamoDBTable("students")]
    public class Student
    {
        [DynamoDBHashKey("id")]
        public int? Id { get; set; }
        [DynamoDBProperty("first_name")]
        public string? FirstName { get; set; }
        [DynamoDBProperty("last_name")]
        public string? LastName { get; set; }
        [DynamoDBProperty("is_student")]
        public bool IsStudent { get; set; }
    }
}