using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Chat> chats { get; set; }
    }
}
