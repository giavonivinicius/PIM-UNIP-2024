using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PimUrbanGreen.Models
{
    [Table("Usuarios")]
    public class UserModel
    {
        [Key]
        [Column("NomeUsuario")]
        public string NomeUsuario { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;
    }
}
