using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationHW1.Models
{
    [Table("FileTable")]
    public class FileTable
    {
        [Key]
        public int ID { get; set; }

        public byte[] UploadedFile { get; set; }


    }


}
