using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class PayableTitle
    {
        [Key]
        public int Id { get; set; }
        public string originId { get; set; }

        public int userId { get; set; }

        public string systemName { get; set; }

        public string? externalId { get; set; }
        public int externalRetry { get; set; }

        public string? errorExternalId { get; set; }

        public DateTime creationDate { get; set; }

        public int statusExternal {  get; set; }

        public object titleInformations { get; set; }

    }
}
