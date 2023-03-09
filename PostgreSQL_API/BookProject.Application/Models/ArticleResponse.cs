using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BookProject.Application.Models
{
    public class ArticleResponse
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int MagazineId { get; set; }
        public int AuthorId { get; set; }
    }
}
