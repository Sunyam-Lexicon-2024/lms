﻿using LMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Models
{
    public class CreateModuleBaseModel
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public int? ParentId { get; set; }
    }
}
 