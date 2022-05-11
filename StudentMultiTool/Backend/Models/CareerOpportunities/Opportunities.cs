using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace StudentMultiTool.Backend.Models.CareerOpportunities
{
    public partial class Opportunities
    {
        [JsonProperty("LanguageCode")]
        public string LanguageCode { get; set; } = string.Empty;

        [JsonProperty("SearchParameters")]
        public SearchParameters? SearchParameters { get; set; }

        [JsonProperty("SearchResult")]
        public SearchResult? SearchResult { get; set; }
    }

    public partial class SearchParameters
    {
    }

    public partial class SearchResult
    {
        [JsonProperty("SearchResultCount")]
        public long SearchResultCount { get; set; }

        [JsonProperty("SearchResultCountAll")]
        public long SearchResultCountAll { get; set; }

        [JsonProperty("SearchResultItems")]
        public SearchResultItem[]? SearchResultItems { get; set; }

        [JsonProperty("UserArea")]
        public SearchResultUserArea? UserArea { get; set; }
    }

    public partial class SearchResultItem
    {
        [JsonProperty("MatchedObjectId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MatchedObjectId { get; set; }

        [JsonProperty("MatchedObjectDescriptor")]
        public MatchedObjectDescriptor? MatchedObjectDescriptor { get; set; }

        [JsonProperty("RelevanceRank")]
        public long RelevanceRank { get; set; }
    }

    public partial class MatchedObjectDescriptor
    {
        [JsonProperty("PositionID")]
        public string PositionId { get; set; } = string.Empty;

        [JsonProperty("PositionTitle")]
        public string PositionTitle { get; set; } = string.Empty;

        [JsonProperty("PositionURI")]
        public Uri? PositionUri { get; set; }

        [JsonProperty("ApplyURI")]
        public Uri[]? ApplyUri { get; set; }

        [JsonProperty("PositionLocationDisplay")]
        public string? PositionLocationDisplay { get; set; }

        [JsonProperty("PositionLocation")]
        public PositionLocation[]? PositionLocation { get; set; }

        [JsonProperty("OrganizationName")]
        public string OrganizationName { get; set; } = string.Empty;

        [JsonProperty("DepartmentName")]
        public string DepartmentName { get; set; } = string.Empty;

        [JsonProperty("JobCategory")]
        public JobCategory[]? JobCategory { get; set; }

        [JsonProperty("JobGrade")]
        public JobGrade[]? JobGrade { get; set; }

        [JsonProperty("PositionSchedule")]
        public JobCategory[]? PositionSchedule { get; set; }

        [JsonProperty("PositionOfferingType")]
        public JobCategory[]? PositionOfferingType { get; set; }

        [JsonProperty("QualificationSummary")]
        public string QualificationSummary { get; set; } = string.Empty;

        [JsonProperty("PositionRemuneration")]
        public PositionRemuneration[]? PositionRemuneration { get; set; }

        [JsonProperty("PositionStartDate")]
        public DateTimeOffset PositionStartDate { get; set; }

        [JsonProperty("PositionEndDate")]
        public DateTimeOffset PositionEndDate { get; set; }

        [JsonProperty("PublicationStartDate")]
        public DateTimeOffset PublicationStartDate { get; set; }

        [JsonProperty("ApplicationCloseDate")]
        public DateTimeOffset ApplicationCloseDate { get; set; }

        [JsonProperty("PositionFormattedDescription")]
        public PositionFormattedDescription[]? PositionFormattedDescription { get; set; }

        [JsonProperty("UserArea")]
        public MatchedObjectDescriptorUserArea? UserArea { get; set; }

        [JsonProperty("SubAgency", NullValueHandling = NullValueHandling.Ignore)]
        public string SubAgency { get; set; } = string.Empty;
    }

    public partial class JobCategory
    {
        [JsonProperty("Name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("Code")]
        public CodeUnion Code { get; set; }
    }

    public partial class JobGrade
    {
        [JsonProperty("Code")]
        public string Code { get; set; } = string.Empty;
    }

    public partial class PositionFormattedDescription
    {
        [JsonProperty("Label")]
        public Label Label { get; set; }

        [JsonProperty("LabelDescription")]
        public LabelDescription LabelDescription { get; set; }
    }

    public partial class PositionLocation
    {
        [JsonProperty("LocationName")]
        public string LocationName { get; set; } = string.Empty;

        [JsonProperty("CountryCode")]
        public CountryCode CountryCode { get; set; }

        [JsonProperty("CountrySubDivisionCode")]
        public string CountrySubDivisionCode { get; set; } = string.Empty;

        [JsonProperty("CityName")]
        public string CityName { get; set; } = string.Empty;

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }
    }

    public partial class PositionRemuneration
    {
        [JsonProperty("MinimumRange")]
        public string MinimumRange { get; set; } = string.Empty;

        [JsonProperty("MaximumRange")]
        public string MaximumRange { get; set; } = string.Empty;

        [JsonProperty("RateIntervalCode")]
        public RateIntervalCode RateIntervalCode { get; set; }
    }

    public partial class MatchedObjectDescriptorUserArea
    {
        [JsonProperty("Details")]
        public Details? Details { get; set; }

        [JsonProperty("IsRadialSearch")]
        public bool IsRadialSearch { get; set; }
    }

    public partial class Details
    {
        [JsonProperty("JobSummary")]
        public string JobSummary { get; set; } = string.Empty;

        [JsonProperty("WhoMayApply")]
        public JobCategory? WhoMayApply { get; set; }

        [JsonProperty("LowGrade")]
        public string LowGrade { get; set; } = string.Empty;

        [JsonProperty("HighGrade")]
        public string HighGrade { get; set; } = string.Empty;

        [JsonProperty("PromotionPotential")]
        public string PromotionPotential { get; set; } = string.Empty;

        [JsonProperty("OrganizationCodes")]
        public string OrganizationCodes { get; set; } = string.Empty;

        [JsonProperty("Relocation")]
        public DrugTestRequired Relocation { get; set; }

        [JsonProperty("HiringPath")]
        public string[]? HiringPath { get; set; }

        [JsonProperty("TotalOpenings", NullValueHandling = NullValueHandling.Ignore)]
        public string TotalOpenings { get; set; } = string.Empty;

        [JsonProperty("AppointmentExplanationText", NullValueHandling = NullValueHandling.Ignore)]
        public string AppointmentExplanationText { get; set; } = string.Empty;

        [JsonProperty("AgencyMarketingStatement")]
        public string AgencyMarketingStatement { get; set; } = string.Empty;

        [JsonProperty("TravelCode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TravelCode { get; set; }

        [JsonProperty("ApplyOnlineUrl", NullValueHandling = NullValueHandling.Ignore)]
        public Uri? ApplyOnlineUrl { get; set; }

        [JsonProperty("DetailStatusUrl", NullValueHandling = NullValueHandling.Ignore)]
        public Uri? DetailStatusUrl { get; set; }

        [JsonProperty("MajorDuties")]
        public string[]? MajorDuties { get; set; }

        [JsonProperty("Education")]
        public string Education { get; set; } = string.Empty;

        [JsonProperty("Requirements")]
        public string Requirements { get; set; } = string.Empty;

        [JsonProperty("Evaluations")]
        public string Evaluations { get; set; } = string.Empty;

        [JsonProperty("HowToApply")]
        public string HowToApply { get; set; } = string.Empty;

        [JsonProperty("WhatToExpectNext")]
        public string WhatToExpectNext { get; set; } = string.Empty;

        [JsonProperty("RequiredDocuments")]
        public string RequiredDocuments { get; set; } = string.Empty;

        [JsonProperty("Benefits")]
        public string Benefits { get; set; } = string.Empty;

        [JsonProperty("OtherInformation")]
        public string OtherInformation { get; set; } = string.Empty;

        [JsonProperty("KeyRequirements")]
        public string[]? KeyRequirements { get; set; }

        [JsonProperty("WithinArea")]
        public DrugTestRequired WithinArea { get; set; }

        [JsonProperty("CommuteDistance")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long CommuteDistance { get; set; }

        [JsonProperty("ServiceType")]
        public string ServiceType { get; set; } = string.Empty;

        [JsonProperty("AnnouncementClosingType")]
        public string AnnouncementClosingType { get; set; } = string.Empty;

        [JsonProperty("AgencyContactEmail")]
        public string AgencyContactEmail { get; set; } = string.Empty;

        [JsonProperty("AgencyContactPhone", NullValueHandling = NullValueHandling.Ignore)]
        public string AgencyContactPhone { get; set; } = string.Empty;

        [JsonProperty("SecurityClearance")]
        public string SecurityClearance { get; set; } = string.Empty;

        [JsonProperty("DrugTestRequired")]
        public DrugTestRequired DrugTestRequired { get; set; }

        [JsonProperty("AdjudicationType")]
        public AdjudicationType[]? AdjudicationType { get; set; }

        [JsonProperty("TeleworkEligible")]
        public bool TeleworkEligible { get; set; }

        [JsonProperty("RemoteIndicator")]
        public bool RemoteIndicator { get; set; }

        [JsonProperty("SubAgencyName", NullValueHandling = NullValueHandling.Ignore)]
        public string SubAgencyName { get; set; } = string.Empty;

        [JsonProperty("BenefitsUrl", NullValueHandling = NullValueHandling.Ignore)]
        public Uri? BenefitsUrl { get; set; }

        [JsonProperty("BenefitsDisplayDefaultText", NullValueHandling = NullValueHandling.Ignore)]
        public bool? BenefitsDisplayDefaultText { get; set; }

        [JsonProperty("AnnouncementClosingTypeOption", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? AnnouncementClosingTypeOption { get; set; }

        [JsonProperty("PositionSensitivitiy", NullValueHandling = NullValueHandling.Ignore)]
        public string PositionSensitivitiy { get; set; } = string.Empty;

        [JsonProperty("AgencyContactWebsite", NullValueHandling = NullValueHandling.Ignore)]
        public Uri? AgencyContactWebsite { get; set; }

        [JsonProperty("PromotionPotentialAdditionalText", NullValueHandling = NullValueHandling.Ignore)]
        public string PromotionPotentialAdditionalText { get; set; } = string.Empty;

        [JsonProperty("SecondAnnouncementUrl", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? SecondAnnouncementUrl { get; set; }

        [JsonProperty("MCOTags", NullValueHandling = NullValueHandling.Ignore)]
        public string[]? McoTags { get; set; }
    }

    public partial class SearchResultUserArea
    {
        [JsonProperty("NumberOfPages")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long NumberOfPages { get; set; }

        [JsonProperty("IsRadialSearch")]
        public bool IsRadialSearch { get; set; }
    }

    public enum CodeEnum { Empty, The0343, The0661, The0854 };

    public enum Label { DynamicTeaser };

    public enum LabelDescription { HitHighlightingForKeywordSearches };

    public enum CountryCode { UnitedStates };

    public enum RateIntervalCode { PerYear };

    public enum AdjudicationType { Credentialing, NationalSecurity, SuitabilityFitness };

    public enum DrugTestRequired { False, True };

    public partial struct CodeUnion
    {
        public CodeEnum? Enum;
        public long? Integer;

        public static implicit operator CodeUnion(CodeEnum Enum) => new CodeUnion { Enum = Enum };
        public static implicit operator CodeUnion(long Integer) => new CodeUnion { Integer = Integer };
    }

    public partial class Opportunities
    {
        public static Opportunities FromJson(string json) => JsonConvert.DeserializeObject<Opportunities>(json, StudentMultiTool.Backend.Models.CareerOpportunities.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Opportunities self) => JsonConvert.SerializeObject(self, StudentMultiTool.Backend.Models.CareerOpportunities.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                CodeUnionConverter.Singleton,
                CodeEnumConverter.Singleton,
                LabelConverter.Singleton,
                LabelDescriptionConverter.Singleton,
                CountryCodeConverter.Singleton,
                RateIntervalCodeConverter.Singleton,
                AdjudicationTypeConverter.Singleton,
                DrugTestRequiredConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class CodeUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(CodeUnion) || t == typeof(CodeUnion?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    switch (stringValue)
                    {
                        case "":
                            return new CodeUnion { Enum = CodeEnum.Empty };
                        case "0343":
                            return new CodeUnion { Enum = CodeEnum.The0343 };
                        case "0661":
                            return new CodeUnion { Enum = CodeEnum.The0661 };
                        case "0854":
                            return new CodeUnion { Enum = CodeEnum.The0854 };
                    }
                    long l;
                    if (Int64.TryParse(stringValue, out l))
                    {
                        return new CodeUnion { Integer = l };
                    }
                    break;
            }
            throw new Exception("Cannot unmarshal type CodeUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (CodeUnion)untypedValue;
            if (value.Enum != null)
            {
                switch (value.Enum)
                {
                    case CodeEnum.Empty:
                        serializer.Serialize(writer, "");
                        return;
                    case CodeEnum.The0343:
                        serializer.Serialize(writer, "0343");
                        return;
                    case CodeEnum.The0661:
                        serializer.Serialize(writer, "0661");
                        return;
                    case CodeEnum.The0854:
                        serializer.Serialize(writer, "0854");
                        return;
                }
            }
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value.ToString());
                return;
            }
            throw new Exception("Cannot marshal type CodeUnion");
        }

        public static readonly CodeUnionConverter Singleton = new CodeUnionConverter();
    }

    internal class CodeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(CodeEnum) || t == typeof(CodeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return CodeEnum.Empty;
                case "0343":
                    return CodeEnum.The0343;
                case "0661":
                    return CodeEnum.The0661;
                case "0854":
                    return CodeEnum.The0854;
            }
            throw new Exception("Cannot unmarshal type CodeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (CodeEnum)untypedValue;
            switch (value)
            {
                case CodeEnum.Empty:
                    serializer.Serialize(writer, "");
                    return;
                case CodeEnum.The0343:
                    serializer.Serialize(writer, "0343");
                    return;
                case CodeEnum.The0661:
                    serializer.Serialize(writer, "0661");
                    return;
                case CodeEnum.The0854:
                    serializer.Serialize(writer, "0854");
                    return;
            }
            throw new Exception("Cannot marshal type CodeEnum");
        }

        public static readonly CodeEnumConverter Singleton = new CodeEnumConverter();
    }

    internal class LabelConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Label) || t == typeof(Label?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Dynamic Teaser")
            {
                return Label.DynamicTeaser;
            }
            throw new Exception("Cannot unmarshal type Label");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Label)untypedValue;
            if (value == Label.DynamicTeaser)
            {
                serializer.Serialize(writer, "Dynamic Teaser");
                return;
            }
            throw new Exception("Cannot marshal type Label");
        }

        public static readonly LabelConverter Singleton = new LabelConverter();
    }

    internal class LabelDescriptionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(LabelDescription) || t == typeof(LabelDescription?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Hit highlighting for keyword searches.")
            {
                return LabelDescription.HitHighlightingForKeywordSearches;
            }
            throw new Exception("Cannot unmarshal type LabelDescription");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (LabelDescription)untypedValue;
            if (value == LabelDescription.HitHighlightingForKeywordSearches)
            {
                serializer.Serialize(writer, "Hit highlighting for keyword searches.");
                return;
            }
            throw new Exception("Cannot marshal type LabelDescription");
        }

        public static readonly LabelDescriptionConverter Singleton = new LabelDescriptionConverter();
    }

    internal class CountryCodeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(CountryCode) || t == typeof(CountryCode?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "United States")
            {
                return CountryCode.UnitedStates;
            }
            throw new Exception("Cannot unmarshal type CountryCode");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (CountryCode)untypedValue;
            if (value == CountryCode.UnitedStates)
            {
                serializer.Serialize(writer, "United States");
                return;
            }
            throw new Exception("Cannot marshal type CountryCode");
        }

        public static readonly CountryCodeConverter Singleton = new CountryCodeConverter();
    }

    internal class RateIntervalCodeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(RateIntervalCode) || t == typeof(RateIntervalCode?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Per Year")
            {
                return RateIntervalCode.PerYear;
            }
            throw new Exception("Cannot unmarshal type RateIntervalCode");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (RateIntervalCode)untypedValue;
            if (value == RateIntervalCode.PerYear)
            {
                serializer.Serialize(writer, "Per Year");
                return;
            }
            throw new Exception("Cannot marshal type RateIntervalCode");
        }

        public static readonly RateIntervalCodeConverter Singleton = new RateIntervalCodeConverter();
    }

    internal class AdjudicationTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(AdjudicationType) || t == typeof(AdjudicationType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Credentialing":
                    return AdjudicationType.Credentialing;
                case "National security":
                    return AdjudicationType.NationalSecurity;
                case "Suitability/Fitness":
                    return AdjudicationType.SuitabilityFitness;
            }
            throw new Exception("Cannot unmarshal type AdjudicationType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (AdjudicationType)untypedValue;
            switch (value)
            {
                case AdjudicationType.Credentialing:
                    serializer.Serialize(writer, "Credentialing");
                    return;
                case AdjudicationType.NationalSecurity:
                    serializer.Serialize(writer, "National security");
                    return;
                case AdjudicationType.SuitabilityFitness:
                    serializer.Serialize(writer, "Suitability/Fitness");
                    return;
            }
            throw new Exception("Cannot marshal type AdjudicationType");
        }

        public static readonly AdjudicationTypeConverter Singleton = new AdjudicationTypeConverter();
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class DrugTestRequiredConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DrugTestRequired) || t == typeof(DrugTestRequired?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "False":
                    return DrugTestRequired.False;
                case "True":
                    return DrugTestRequired.True;
            }
            throw new Exception("Cannot unmarshal type DrugTestRequired");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (DrugTestRequired)untypedValue;
            switch (value)
            {
                case DrugTestRequired.False:
                    serializer.Serialize(writer, "False");
                    return;
                case DrugTestRequired.True:
                    serializer.Serialize(writer, "True");
                    return;
            }
            throw new Exception("Cannot marshal type DrugTestRequired");
        }

        public static readonly DrugTestRequiredConverter Singleton = new DrugTestRequiredConverter();
    }
}
