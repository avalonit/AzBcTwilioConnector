using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace com.businesscentral
{

    public partial class SalesOrders
    {
        [JsonProperty("@odata.context")]
        public Uri OdataContext { get; set; }

        [JsonProperty("value")]
        public List<SalesOrder> Value { get; set; }
    }

    public partial class SalesOrder
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("externalDocumentNumber")]
        public string ExternalDocumentNumber { get; set; }

        [JsonProperty("orderDate")]
        public DateTime OrderDate { get; set; }

        [JsonProperty("postingDate")]
        public DateTime PostingDate { get; set; }

        [JsonProperty("customerId")]
        public Guid CustomerId { get; set; }

        [JsonProperty("contactId")]
        public string ContactId { get; set; }

        [JsonProperty("customerNumber")]
        public string CustomerNumber { get; set; }

        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        [JsonProperty("billToName")]
        public string BillToName { get; set; }

        [JsonProperty("billToCustomerId")]
        public Guid BillToCustomerId { get; set; }

        [JsonProperty("billToCustomerNumber")]
        public string BillToCustomerNumber { get; set; }

        [JsonProperty("shipToName")]
        public string ShipToName { get; set; }

        [JsonProperty("shipToContact")]
        public string ShipToContact { get; set; }

        [JsonProperty("currencyId")]
        public Guid CurrencyId { get; set; }

        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty("pricesIncludeTax")]
        public bool PricesIncludeTax { get; set; }

        [JsonProperty("paymentTermsId")]
        public Guid PaymentTermsId { get; set; }

        [JsonProperty("shipmentMethodId")]
        public Guid ShipmentMethodId { get; set; }

        [JsonProperty("salesperson")]
        public string Salesperson { get; set; }

        [JsonProperty("partialShipping")]
        public bool PartialShipping { get; set; }

        [JsonProperty("requestedDeliveryDate")]
        public DateTime RequestedDeliveryDate { get; set; }

        [JsonProperty("discountAmount")]
        public long DiscountAmount { get; set; }

        [JsonProperty("discountAppliedBeforeTax")]
        public bool DiscountAppliedBeforeTax { get; set; }

        [JsonProperty("totalAmountExcludingTax")]
        public long TotalAmountExcludingTax { get; set; }

        [JsonProperty("totalTaxAmount")]
        public long TotalTaxAmount { get; set; }

        [JsonProperty("totalAmountIncludingTax")]
        public long TotalAmountIncludingTax { get; set; }

        [JsonProperty("fullyShipped")]
        public bool FullyShipped { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("lastModifiedDateTime")]
        public DateTime LastModifiedDateTime { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("sellingPostalAddress")]
        public IngPostalAddress SellingPostalAddress { get; set; }

        [JsonProperty("billingPostalAddress")]
        public IngPostalAddress BillingPostalAddress { get; set; }

        [JsonProperty("shippingPostalAddress")]
        public IngPostalAddress ShippingPostalAddress { get; set; }
    }

    public partial class IngPostalAddress
    {
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("countryLetterCode")]
        public string CountryLetterCode { get; set; }

        [JsonProperty("postalCode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PostalCode { get; set; }
    }

    public partial class SalesOrders
    {
        public static SalesOrders FromJson(string json) => JsonConvert.DeserializeObject<SalesOrders>(json);
    }

    public static class Serialize
    {
        public static string ToJson(this SalesOrders self) => JsonConvert.SerializeObject(self);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
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
}
