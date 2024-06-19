namespace LMS.Core.Entities;

public class Document
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Description { get; set; }
    public DateTime UploadedAt {get;set;}
    public int UserId {get;set;}
    public User User {get;set;}
}