﻿namespace MVC_PustokPlus.Models;

public class Blog
{
    public int Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastUpdatedAt { get; set;} = DateTime.Now;

    public int AuthorId { get; set; }
    public Author? Author { get; set; }
}