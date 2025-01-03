var list = new List<BinaryTreeNode> { referrer }; // Initialize with referrer.

while (list.Count > 0)
{
    var current = list[0]; // Access the first element.
    list.RemoveAt(0);      // Remove the first element (simulates Dequeue).

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

    list.Add(GetNodeById(current.LeftChildId)); // Add left child to the list.
    list.Add(GetNodeById(current.RightChildId)); // Add right child to the list.
}
