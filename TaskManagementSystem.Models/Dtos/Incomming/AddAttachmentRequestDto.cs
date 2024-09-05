namespace TaskManagementSystem.Models.Dtos.Incomming;

public class AddAttachmentRequestDto
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public int TaskId { get; set; }
}
