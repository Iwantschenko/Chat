using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Chat
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Creator_Id { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
