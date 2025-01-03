public class BinaryTreeNode
{
    public int Id { get; set; } // Primary Key
    public int? ParentId { get; set; } // Foreign Key referencing another BinaryTreeNode
    public int? LeftChildId { get; set; } // Foreign Key referencing another BinaryTreeNode
    public int? RightChildId { get; set; } // Foreign Key referencing another BinaryTreeNode
    public int UserId { get; set; } // Foreign Key referencing the User table
}

public class User
{
    public int Id { get; set; } // Primary Key
    public string UserName { get; set; } // Username (used as referral code)
    public string Email { get; set; }
    public string PasswordHash { get; set; } // Encrypted password storage
}
