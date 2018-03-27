using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace technical.test.Model
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [Display(Name = "Usuario")]
        public string Username { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]        
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [MinLength(10)]        
        public string PasswordConfirmation { set; get; }

        [Required(ErrorMessage = "El rol es requerido")]
        [Display(Name = "Rol del usuario")]
        public Role Role { get; set; }

        [JsonIgnore]
        public virtual UserData UserData { get; set; }

        [JsonIgnore]
        public virtual Session Session { get; set; }
                

        [Display(Name = "Activo")]
        public bool Enabled { get; set; }

        [JsonIgnore]
        public DateTime Created { get; set; }
    }

    [Table("UserData")]
    public class UserData
    {
        public int Id { get; set; }

        [InverseProperty("Id")]
        [ForeignKey("Id")]
        public virtual User User { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.EmailAddress,ErrorMessage ="El campo no tiene el formato de email")]
        public String Email { get; set; }
    }

    [Table("Sessions")]
    public class Session
    {
        public int Id { get; set; }

        [InverseProperty("Id")]
        [ForeignKey("Id")]
        public virtual User User { get; set; }

        public string Token { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Unspecified
    }
}
