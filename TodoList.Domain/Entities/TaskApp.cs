using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Entities;

namespace TodoList.Domain.Entities
{
    public class TaskApp : Entity
    {
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
