using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPrep.Domain.Entities
{
    public class UserProgress
    {
        public long Id { get; set; }

        public Guid UserId { get; set; }

        public long QuestionId { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime LastVisited { get; set; }
    }
}
