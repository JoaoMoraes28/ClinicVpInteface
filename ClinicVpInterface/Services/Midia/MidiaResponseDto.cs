using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClinicVpInterface.Services.Midia
{
    public class MidiaResponseDto
    {

        [JsonPropertyName("asset_id")]
        public string AssetId { get; set; }

        [JsonPropertyName("public_id")]
        public string PublicId { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }

        [JsonPropertyName("version")]
        public long Version { get; set; }

        [JsonPropertyName("resource_type")]
        public string ResourceType { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("bytes")]
        public long Bytes { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("backup")]
        public bool Backup { get; set; }

        [JsonPropertyName("asset_folder")]
        public string AssetFolder { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("secure_url")]
        public string SecureUrl { get; set; }

        [JsonPropertyName("next_cursor")]
        public string NextCursor { get; set; }

        [JsonPropertyName("derived")]
        public List<DerivedImageResponse> Derived { get; set; }
    }

    public class DerivedImageResponse
    {
        [JsonPropertyName("transformation")]
        public string Transformation { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }

        [JsonPropertyName("bytes")]
        public long Bytes { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("secure_url")]
        public string SecureUrl { get; set; }
    }

}
