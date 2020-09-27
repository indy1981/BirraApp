namespace BirrasApp.Repositories.Models
{
    public class UserRequestToMeetUp
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int MeetupId { get; set; }

        public Meetup Meetup { get; set; }
    }
}
