﻿namespace Balea
{
    public class RoleFilter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Enabled { get; set; }

        public string[] Mappings { get; set; }
        public string[] Subjects { get; set; }
    }
}
