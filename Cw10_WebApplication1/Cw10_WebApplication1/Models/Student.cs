using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cw10_WebApplication1.Models
{
    public partial class Student
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        [ForeignKey("IdEnrollment")]
        public int? IdEnrollment { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public string Salt { get; set; }

        public virtual Enrollment IdEnrollmentNavigation { get; set; }
    }
}
