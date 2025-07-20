﻿namespace ColdCash.Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSystem { get; set; }
    }
}