﻿using GeneralStoreAPI.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models.TransactionModels
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof Id)]
        public Customer 
    }
}