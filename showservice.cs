public class ReferralTreeService
{
    private readonly ApplicationDbContext _context;

    public ReferralTreeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<BinaryTreeNodeDto> GetTreeAsync()
    {
        // Fetch the root node (where ParentId is null)
        var rootNode = await _context.BinaryTreeNodes
            .FirstOrDefaultAsync(node => node.ParentId == null);

        if (rootNode == null)
        {
            throw new Exception("No tree found.");
        }

        // Recursively build the tree
        return BuildTree(rootNode);
    }

    private BinaryTreeNodeDto BuildTree(BinaryTreeNode node)
    {
        if (node == null) return null;

        return new BinaryTreeNodeDto
        {
            Id = node.Id,
            UserId = node.UserId,
            LeftChild = BuildTree(GetNodeById(node.LeftChildId)),
            RightChild = BuildTree(GetNodeById(node.RightChildId))
        };
    }

    private BinaryTreeNode GetNodeById(int? nodeId)
    {
        if (nodeId == null) return null;

        return _context.BinaryTreeNodes.FirstOrDefault(n => n.Id == nodeId);
    }
}

// DTO for transferring tree data
public class BinaryTreeNodeDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public BinaryTreeNodeDto LeftChild { get; set; }
    public BinaryTreeNodeDto RightChild { get; set; }
}
