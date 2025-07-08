using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriYapıları
{
    public class BST
    {
        private BSTNode root;

        public void Insert(Item item)
        {
            root = InsertRec(root, item);
        }

        private BSTNode InsertRec(BSTNode node, Item item)
        {
            if (node == null)
                return new BSTNode(item);

            if (string.Compare(item.Name, node.Data.Name) < 0)
                node.Left = InsertRec(node.Left, item);
            else if (string.Compare(item.Name, node.Data.Name) > 0)
                node.Right = InsertRec(node.Right, item);

            return node;
        }

        public Item Search(string name)
        {
            return SearchRec(root, name.ToLower());
        }

        private Item SearchRec(BSTNode node, string name)
        {
            if (node == null) return null;

            if (name == node.Data.Name)
                return node.Data;

            if (string.Compare(name, node.Data.Name) < 0)
                return SearchRec(node.Left, name);
            else
                return SearchRec(node.Right, name);
        }
        public void Delete(string name)
        {
            root = DeleteRecursive(root, name);
        }

        private BSTNode DeleteRecursive(BSTNode node, string name)
        {
            if (node == null)
                return null;

            int compare = string.Compare(name, node.Data.Name, StringComparison.OrdinalIgnoreCase);

            if (compare < 0)
            {
                node.Left = DeleteRecursive(node.Left, name);
            }
            else if (compare > 0)
            {
                node.Right = DeleteRecursive(node.Right, name);
            }
            else
            {
                // Tek çocuk veya hiç çocuk yoksa
                if (node.Left == null)
                    return node.Right;
                if (node.Right == null)
                    return node.Left;

                // İki çocuğu varsa: sağ alt ağacın en küçük elemanını bul
                BSTNode minRight = FindMin(node.Right);
                node.Data = minRight.Data;
                node.Right = DeleteRecursive(node.Right, minRight.Data.Name);
            }

            return node;
        }

        private BSTNode FindMin(BSTNode node)
        {
            while (node.Left != null)
                node = node.Left;
            return node;
        }





    }
}
