using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Application.DTOs
{
    public class QuestionDto
    {
        public long Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Answer { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;
    }

}
