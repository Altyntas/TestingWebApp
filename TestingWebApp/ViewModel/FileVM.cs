﻿namespace TestingWebApp.ViewModel
{
    public class FileVM
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? FullPath { get; set; }
        public IFormFile? File { get; set; }
    }
}
