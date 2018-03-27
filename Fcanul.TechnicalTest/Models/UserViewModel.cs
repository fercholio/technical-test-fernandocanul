using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using technical.test.Model;

namespace Fcanul.TechnicalTest.Models
{
    public class UserViewModel
    {

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
        [MinLength(7)]
        public string Username { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [MinLength(10)]
        [Required(ErrorMessage = "La contraseña es requerida")]
        [RegularExpression("^.*(?=.{10,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Mínimo 10 caracteres, un numero, una mayuscula y minuscula mínimo")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [MinLength(10)]
        [Required(ErrorMessage = "La confirmación es requerida")]
        [RegularExpression("^.*(?=.{10,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Mínimo 10 caracteres, un numero, una mayuscula y minuscula mínimo")]
        [Display(Name = "Confirmación Contraseña")]
        [Compare("Password", ErrorMessage = "La confirmación de la contraseña no coincide")]
        public string PasswordConfirmation { set; get; }

        [Required(ErrorMessage = "El rol es requerido")]
        [Display(Name = "Rol del usuario")]
        public Role Role { get; set; }


        [Display(Name = "Activo")]
        public bool Enabled { get; set; }


        public DateTime Created { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "El campo no tiene el formato de email")]
        public String Email { get; set; }

    }
}