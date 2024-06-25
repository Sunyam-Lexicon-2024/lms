using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Core.Entities;

namespace LMS.Core.Models
{
    public class DocumentBaseModel
    {
        public string Name { get; set; } = default!;
        public string Url { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime UploadedAt { get; set; }
        public string UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
