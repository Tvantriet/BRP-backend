﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities
{
    public class DBEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
