using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Entity
    {
        public Guid Id { get; set; }
    }

    public class AggregateRoot : Entity
    {
        // Additional domain logic here
    }

    public class ValueObject
    {
        // Immutable value object pattern implementation
    }

    public class ApiResponse
    {
        public string? ApiVersion { get; set; }
        public string? DocumentationUrl { get; set; }
        public string? FriendlyNotice { get; set; }
        public int JobCount { get; set; }
        public string? XRayHash { get; set; }
        public string? ClientKey { get; set; }
        public DateTime LastUpdate { get; set; }
        public List<Job>? Jobs { get; set; }
    }

    [DataContract]
    public class Job: Entity
    {
        [DataMember(Name = "id")]
        public int JobId { get; set; }
        [DataMember]
        public required string Url { get; set; }
        [DataMember]
        public required string JobSlug { get; set; }
        [DataMember]
        public required string JobTitle { get; set; }
        [DataMember]
        public required string CompanyName { get; set; }
        [DataMember]
        public required string CompanyLogo { get; set; }
        [DataMember]
        public required List<string> JobIndustry { get; set; }
        [DataMember]
        public required List<string> JobType { get; set; }
        [DataMember]
        public required string JobGeo { get; set; }
        [DataMember]
        public required string JobLevel { get; set; }
        [DataMember]
        public required string JobExcerpt { get; set; }
        [DataMember]
        public required string JobDescription { get; set; }
        [DataMember]
        public DateTime PubDate { get; set; }
        [DataMember]
        public decimal AnnualSalaryMin { get; set; }
        [DataMember]
        public decimal AnnualSalaryMax { get; set; }
        [DataMember]
        public string? SalaryCurrency { get; set; }
    }
}