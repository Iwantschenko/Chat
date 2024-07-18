using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto
{
    public class ChatDto
    {
        public string Name { get; set; }
        public string Creator_Id { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}
