using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.Application.DTOs;

public class ProgressDto
{
    public Guid UserId { get; set; }

    public long QuestionId { get; set; }

    public bool IsCompleted { get; set; }
}
