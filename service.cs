public class ReferralService
{
    private readonly ApplicationDbContext _context;

    public ReferralService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task RegisterUser(string username, string referralCode)
    {
        var referrer = await _context.BinaryTreeNodes
            .FirstOrDefaultAsync(n => n.UserId == _context.Users
                .Where(u => u.Username == referralCode)
                .Select(u => u.Id)
                .FirstOrDefault());

        if (referrer == null)
        {
            throw new Exception("Invalid referral code.");
        }

        var newUser = new User { Username = username };
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        var newNode = new BinaryTreeNode { UserId = newUser.Id };

        if (referrer.LeftChildId == null)
        {
            referrer.LeftChildId = newNode.Id;
        }
        else if (referrer.RightChildId == null)
        {
            referrer.RightChildId = newNode.Id;
        }
        else
        {
            var queue = new Queue<BinaryTreeNode>();
            queue.Enqueue(referrer);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current.LeftChildId == null)
                {
                    current.LeftChildId = newNode.Id;
                    newNode.ParentId = current.Id;
                    break;
                }
                else if (current.RightChildId == null)
                {
                    current.RightChildId = newNode.Id;
                    newNode.ParentId = current.Id;
                    break;
                }

                queue.Enqueue(GetNodeById(current.LeftChildId));
                queue.Enqueue(GetNodeById(current.RightChildId));
            }
        }

        _context.BinaryTreeNodes.Add(newNode);
        await _context.SaveChangesAsync();
    }

    private BinaryTreeNode GetNodeById(int? nodeId)
    {
        return _context.BinaryTreeNodes.FirstOrDefault(n => n.Id == nodeId);
    }
}
