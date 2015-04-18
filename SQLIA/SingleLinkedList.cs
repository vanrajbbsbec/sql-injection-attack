using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLIA
{

    public class SingleLinkedList
    {
 
        class Node
        {
            public int tokenIdentifier;
            public Boolean IsUserData;
            public Node Next;
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

        public SingleLinkedList()
        {
            size = 0;
            head = null;
        }


        public void AddAtEnd(int pTokenIdentifier)
        {
            size++;

            var node = new Node()
            {
                tokenIdentifier = pTokenIdentifier
            };

            if(head == null)
            {
                head = node;
            }
            else
            {
                current.Next = node;
            }
            current = node;
        }

        public void AddAtBegin(int pTokenIdentifier)
        {
            size++;

            head = new Node()
            {
                Next = head,
                tokenIdentifier = pTokenIdentifier
            };
        }


        public void ListNodes()
        {
            Node tempNode = head;

            while (tempNode != null)
            {
                Console.WriteLine(tempNode.tokenIdentifier);
                tempNode = tempNode.Next;
            }
        }

        public bool Delete(int Position)
        {
            if (Position == 1)
            {
                if (Position == size)
                {
                    head = null;
                    current = null;
                    return true;
                }
                else if (Position > size)
                {
                    return false;
                }
                else
                {
                    head = head.Next;
                    return true;
                }
            }

            if (Position > 1 && Position <= size)
            {
                Node tempNode = head;

                Node lastNode = null;
                int count = 0;

                while (tempNode != null)
                {
                    if (count == Position - 1)
                    {
                        lastNode.Next = tempNode.Next;
                        return true;
                    }
                    count++;

                    lastNode = tempNode;
                    tempNode = tempNode.Next;
                }
            }
            return false;
        }

    }
}
