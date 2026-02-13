namespace DAL.Entities;

public class Follower
{
    public int Id { get; set; }
    
    public int FollowerID { get; set; }

    public int FollowingID { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public User? FollowerProfile { get; set; }
    public User? FollowingProfile { get; set; }
}