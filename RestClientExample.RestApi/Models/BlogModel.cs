using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestClientExample.RestApi.Models;

[Table("Tbl_Blog")]
public class BlogModel
{
    [Key]
    public long BlogId { get; set; }
    public string BlogTitle { get; set; } = null!;
    public string BlogAuthor { get; set; } = null!;
    public string BlogContent { get; set; } = null!;
}
