using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BookProject.Application.Models
{
    public class ArticleTestResponseJustName
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string MagazineName { get; set; }
        public string AuthorName { get; set; }
    }
}
