using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }


        [Column(TypeName = "varcher(50)")]
        public string Name { get; set; }

        [Column (TypeName = "varchar(50)")]
        public string Gender { get; set; }

        [Column(TypeName ="varchar 200")]
        public string Department { get; set; }


    }
}
