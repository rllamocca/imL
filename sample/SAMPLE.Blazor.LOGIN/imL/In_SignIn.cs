using System.ComponentModel.DataAnnotations;

namespace SAMPLE.Blazor.LOGIN
{
    public class In_SignIn
    {
        [Required]
        public string? Username { set; get; }
        [Required]
        public string? PassWord { set; get; }
        public bool Remember { set; get; }
    }
}
