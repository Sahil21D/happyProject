[ApiController]
[Route("api/[controller]")]
public class ReferralTreeController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ReferralTreeController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetTree(string username)
    {
        // Find the referrer node by username.
        var referrerNode = await _context.BinaryTreeNodes
            .FirstOrDefaultAsync(n => n.UserId == _context.Users
                .Where(u => u.Username == username)
                .Select(u => u.Id)
                .FirstOrDefault());

        if (referrerNode == null)
        {
            return NotFound("Referrer not found.");
        }

        // Build the tree representation.
        var tree = BuildTree(referrerNode);

        return Ok(tree);
    }

    private TreeNodeDto BuildTree(BinaryTreeNode node)
    {
        if (node == null)
            return null;

        var user = _context.Users.FirstOrDefault(u => u.Id == node.UserId);

        // Recursive tree building.
        return new TreeNodeDto
        {
            Username = user?.Username,
            LeftChild = BuildTree(GetNodeById(node.LeftChildId)),
            RightChild = BuildTree(GetNodeById(node.RightChildId))
        };
    }

    private BinaryTreeNode GetNodeById(int? nodeId)
    {
        return _context.BinaryTreeNodes.FirstOrDefault(n => n.Id == nodeId);
    }
}

public class TreeNodeDto
{
    public string Username { get; set; }
    public TreeNodeDto LeftChild { get; set; }
    public TreeNodeDto RightChild { get; set; }
}
