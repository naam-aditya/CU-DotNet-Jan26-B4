namespace LibraryManagementApi.Dtos
{
    public class UpdateBookDto
    {
        public int BookId { get; set; }
        public string? Title { get; set; }
        public int AuthorId { get; set; }
    }
}