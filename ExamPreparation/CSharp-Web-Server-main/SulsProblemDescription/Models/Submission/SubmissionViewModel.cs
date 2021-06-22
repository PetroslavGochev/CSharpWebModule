namespace SulsProblemDescription.Models.Submission
{
    using System;

    public class SubmissionViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public int AchievedResult { get; set; }

        public int MaxPoints { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
