using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

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
        public class Node
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
                Console.Write(" -->{0}\tHitCount: {1}", tempNode.pointer.GetType().Name, tempNode.HitCount);
                tempNode = tempNode.next;
            }
        }


        public Boolean Compare(ArrayList pArrUserQuery)
        {
            Boolean IsUserQueryMatching = false;
            Node tempNode = this.head;

            while (tempNode != null)
            {
                SingleLinkedList objSingleList = tempNode.pointer;
                IsUserQueryMatching = objSingleList.CompareSingleList(pArrUserQuery);
                if (IsUserQueryMatching)
                {
                    /// <summary>
                    /// Increase the Hit count
                    /// </summary>
                    tempNode.HitCount++;

                    /// <summary>
                    /// Move The Current Node in Double Linked List to correct position i.e. one position UP
                    /// </summary>
                    MoveNode(tempNode);
                    break;
                }
                tempNode = tempNode.next;
            }
            return IsUserQueryMatching;
        }

        public void MoveNode(Node pInCorrectPositionNode)
        {
            Node tempNode = this.head;
            while (tempNode != null)
            {
                if (tempNode.pointer.Equals(pInCorrectPositionNode.pointer))
                {
                    /// <summary>
                    /// Check IF the current node is First node
                    /// </summary>
                    if (pInCorrectPositionNode.pre == null)
                    {
                        //Do nothing
                        break;
                    }
                    /// <summary>
                    /// Check IF the current node is Last node
                    /// </summary>
                    else if (pInCorrectPositionNode.next == null)
                    {
                        Node loc = pInCorrectPositionNode.pre;

                        /// <summary>
                        /// Check IF double linked list has only two nodes
                        /// </summary>
                        if (this.head == loc)
                        {
                            /// <summary>
                            /// Hitcount should be Equal or less from previous node to swap
                            /// </summary>
                            if (loc.HitCount <= pInCorrectPositionNode.HitCount)
                            {
                                this.head = loc.next;
                                loc.next = null;
                                loc.pre = head;
                                pInCorrectPositionNode.next = loc;
                                pInCorrectPositionNode.pre = null;
                            }
                        }
                        else
                        {
                            if (loc.HitCount <= pInCorrectPositionNode.HitCount)
                            {
                                loc.pre.next = pInCorrectPositionNode;
                                pInCorrectPositionNode.pre = loc.pre;
                                pInCorrectPositionNode.next = loc;
                                loc.pre = pInCorrectPositionNode;
                                loc.next = null;
                                break;
                            }
                        }
                    }
                    /// <summary>
                    /// Check IF the current node is Second node
                    /// </summary>
                    else if(pInCorrectPositionNode.pre.pre == null)
                    {
                        //Head of linked List need to be assigned pointer to Current node After swapping
                    }
                }
                tempNode = tempNode.next;
            }
        }
    }
}
