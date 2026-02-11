namespace DAL.Entities;

public class Follower
{
    public int Id { get; set; }
    
    public int FollowerID { get; set; }

    public int FollowingID { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public Profile? FollowerProfile { get; set; }
    public Profile? FollowingProfile { get; set; }
}