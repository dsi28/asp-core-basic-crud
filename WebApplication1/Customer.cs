using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public class Customer
    {
        public int Id { get; set; }
        //required makes the class property manditory, str length : max length 20
        [Required, StringLength(20)]
        public string Name { get; set; }
    }
}