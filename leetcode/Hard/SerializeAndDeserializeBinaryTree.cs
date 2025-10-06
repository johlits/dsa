namespace LeetCode.SerializeAndDeserializeBinaryTree
{
    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }

    public class Codec
    {
        public string serialize(TreeNode root)
        {
            if (root == null)
            {
                return "null";
            }
            return root.val + "," + serialize(root.left) + "," + serialize(root.right);
        }

        public TreeNode deserialize(string data)
        {
            var nodes = new Queue<string>(data.Split(','));
            return BuildTree(nodes);
        }

        private TreeNode BuildTree(Queue<string> nodes)
        {
            string val = nodes.Dequeue();
            if (val == "null") return null;

            var node = new TreeNode(int.Parse(val));
            node.left = BuildTree(nodes);
            node.right = BuildTree(nodes);
            return node;
        }
    }
}