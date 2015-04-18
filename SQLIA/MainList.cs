using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLIA
{
    struct node
    {
        MainList pre;
        MainList next;
        SingleLinkedList pointer;
        int HitCount;
    }

    public class MainList
    {
        class Node
        {
            public Node pre;
            public Node next;
            public SingleLinkedList pointer;
            public int HitCount;
        }

        private int size;
        public int Count
        {
            get
            {
                return size;
            }
        }

        /// <summary>
        /// The head of the list.
        /// </summary>
        private Node head;

        /// <summary>
        /// The current node, used to avoid adding nodes before the head
        /// </summary>
        private Node current;

        public MainList()
        {
            size = 0;
            head = null;
        }


        public void AddAtEnd(SingleLinkedList pObject)
        {
            size++;

            var node = new Node()
            {
                pointer = pObject
            };

            if (head == null)
            {
                head = node;
                node.pre = null;
                node.next = null;
            }
            else
            {
                node.pre = current;
                node.next = null;
                current.next = node;
            }
            current = node;
        }

        public void ListNodes()
        {
            Node tempNode = head;
            while (tempNode != null)
            {
                Console.Write(" -->{0}\tHitCount: {1}",tempNode.pointer.GetType().Name, tempNode.HitCount);
                tempNode = tempNode.next;
            }
        }
    }
}
