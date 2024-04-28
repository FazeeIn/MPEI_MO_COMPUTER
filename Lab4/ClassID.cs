
namespace ClassID
{
    public class TreeNode
    {
        public string Name { get; set; }
        public int Data { get; set; }
        public TreeNode(string name, int data)
        {
            Name = name;
            Data = data;
        } 
        public TreeNode(string name)
        {
            Name = name;
        }
    }
}       

