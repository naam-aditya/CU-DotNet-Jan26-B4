using System;
using System.Collections.Generic;

namespace LibraryManagementApi.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public int? AuthorId { get; set; }

    public virtual Author? Author { get; set; }
}
