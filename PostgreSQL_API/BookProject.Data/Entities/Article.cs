using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookProject.Data.Entities
{
    public class Article
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int MagazineId { get; set; }
        public Magazine Magazine { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
    }
}
