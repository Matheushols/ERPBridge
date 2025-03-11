using System;
using System.ComponentModel.DataAnnotations;

namespace ERPBridge.DTOs
{
    public class CreatePayableTitleDto
    {
        [Required]
        public string OriginId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string SystemName { get; set; }

        public string? ExternalId { get; set; }

        [Required]
        public int ExternalRetry { get; set; }

        public string? ErrorExternalId { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public int StatusExternal { get; set; }

        [Required]
        public object TitleInformations { get; set; }
    }

    public class UpdatePayableTitleDto
    {
        public string? OriginId { get; set; }

        public int? UserId { get; set; }

        public string? SystemName { get; set; }

        public string? ExternalId { get; set; }

        public int? ExternalRetry { get; set; }

        public string? ErrorExternalId { get; set; }

        public DateTime? CreationDate { get; set; }

        public int? StatusExternal { get; set; }

        public object? TitleInformations { get; set; }
    }
}
