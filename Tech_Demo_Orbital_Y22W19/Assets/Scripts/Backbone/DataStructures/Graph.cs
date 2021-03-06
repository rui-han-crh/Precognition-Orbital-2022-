using Algorithms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DataStructures
{
    public abstract class Graph<T> : IGraphable<T>
    {
        public class GraphNode : IDirectable<GraphNode>, ICollection<GraphNode>, IConnectable<GraphNode>
        {
            private T data;

            public T Data => data;

            public int Count => OutgoingNodes.Count;

            public bool IsReadOnly => false;

            private HashSet<GraphNode> incomingNodes = new HashSet<GraphNode>();
            private HashSet<GraphNode> outgoingNodes = new HashSet<GraphNode>();

            public HashSet<GraphNode> IncomingNodes => incomingNodes;
            public HashSet<GraphNode> OutgoingNodes => outgoingNodes;

            public GraphNode(T data)
            {
                this.data = data;
            }

            public void Add(GraphNode item)
            {
                AddOutgoing(item);
            }

            public void Clear()
            {
                incomingNodes.Clear();
                outgoingNodes.Clear();
            }

            public bool Contains(GraphNode item)
            {
                return outgoingNodes.Contains(item);
            }

            public IEnumerable<GraphNode> GetConnected()
            {
                return OutgoingNodes;
            }

            public void CopyTo(GraphNode[] array, int arrayIndex)
            {
                GraphNode[] childrenArray = outgoingNodes.ToArray();
                childrenArray.CopyTo(array, arrayIndex);
            }

            public bool Remove(GraphNode item)
            {
                return RemoveOutgoing(item);
            }

            public IEnumerator<GraphNode> GetEnumerator()
            {
                return incomingNodes.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public bool Connect(GraphNode other)
            {
                return this.AddOutgoing(other) && other.AddIncoming(this);
            }

            public bool Disconnect(GraphNode other)
            {
                return this.RemoveOutgoing(other) && other.RemoveIncoming(this);
            }

            public bool IsConnectedWith(GraphNode other)
            {
                return this.Contains(other);
            }

            public bool AddIncoming(GraphNode incoming)
            {
                return incomingNodes.Add(incoming);
            }

            public bool RemoveIncoming(GraphNode incoming)
            {
                return incomingNodes.Remove(incoming);
            }

            public bool AddOutgoing(GraphNode outgoing)
            {
                return outgoingNodes.Add(outgoing);
            }

            public bool RemoveOutgoing(GraphNode outgoing)
            {
                return outgoingNodes.Remove(outgoing);
            }

        }


        private readonly Dictionary<T, GraphNode> storedItems = new Dictionary<T, GraphNode>();

        protected Dictionary<T, GraphNode> StoredItems => storedItems;

        int ICollection<T>.Count => storedItems.Count;

        bool ICollection<T>.IsReadOnly => false;

        public Graph()
        {

        }

        public Graph(IEnumerable<T> items)
        {
            this.storedItems = new Dictionary<T, GraphNode>();
            foreach (T item in items)
            {
                storedItems.Add(item, new GraphNode(item));
            }
        }

        public virtual void Add(T item)
        {
            storedItems.Add(item, new GraphNode(item));
        }

        public void Clear()
        {
            storedItems.Clear();
        }

        public bool Contains(T item)
        {
            return storedItems.ContainsKey(item);
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            T[] keyArray = storedItems.Keys.ToArray();

            keyArray.CopyTo(array, arrayIndex);
        }

        public virtual bool Remove(T item)
        {
            GraphNode graphNode = storedItems[item];
            foreach (GraphNode incoming in graphNode.IncomingNodes.ToArray())
            {
                incoming.RemoveOutgoing(graphNode);
                graphNode.RemoveIncoming(incoming);
            }

            foreach (GraphNode outgoing in graphNode.OutgoingNodes.ToArray())
            {
                outgoing.RemoveIncoming(graphNode);
                graphNode.RemoveOutgoing(outgoing);
            }

            return storedItems.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return storedItems.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return storedItems.Keys.GetEnumerator();
        }

        public abstract bool Connect(T a, T b);


        public bool IsConnected(T a, T b)
        {
            if (!this.Contains(a) || !this.Contains(b))
            {
                return false;
            }

            GraphNode graphNodeA = storedItems[a];
            GraphNode graphNodeB = storedItems[b];
            return graphNodeA.IsConnectedWith(graphNodeB);
        }

        public abstract bool Disconnect(T a, T b);

        public IEnumerable<T> GetConnected(T item)
        {
            if (!this.Contains(item))
            {
                return new T[0];
            }

            return storedItems[item].OutgoingNodes.Select(x => x.Data);
        }

        public IEnumerable<IEnumerable<T>> GetConnectedComponents()
        {
            IDictionary<T, GraphNode> notVisited = new Dictionary<T, GraphNode>(storedItems);
            ICollection<IEnumerable<T>> components = new List<IEnumerable<T>>();

            while (notVisited.Count > 0)
            {
                ICollection<T> component = new HashSet<T>();

                SearchAlgorithms.DepthFirstSearch(notVisited.First().Value,
                                                    x => x.OutgoingNodes.Where(y => notVisited.ContainsKey(y.Data)),
                                                    x => { component.Add(x.Data); notVisited.Remove(x.Data); });
                components.Add(component);
            }

            return components;
        }

        public bool IsConnected()
        {
            return GetConnectedComponents().Count() == 1;
        }

        public bool HasCycle()
        {
            Queue<T> notVisited = new Queue<T>(storedItems.Keys.ToArray());
            ICollection<T> visited = new HashSet<T>();
            bool hasCycle = false;

            while (notVisited.Count > 0)
            {
                T current = notVisited.Dequeue();
                if (visited.Contains(current))
                {
                    continue;
                }


                SearchAlgorithms.BreadthFirstSearch(current, x => GetConnected(x),
                                    x => {
                                        visited.Add(x);
                                        if (GetConnected(x).Any(x => visited.Contains(x))) hasCycle = true;
                                    });
                if (hasCycle)
                {
                    break;
                }
            }

            return hasCycle;
        }

        public int GetMaximumDegree()
        {
            return storedItems.Select(x => GetConnected(x.Key)).Max(x => x.Count());
        }

        public int GetDiameter()
        {
            return SearchAlgorithms.BreadthFirstSearch(storedItems.First().Key, x => GetConnected(x), _ => { }) - 1;
        }

        public abstract bool CanWalkFromTo(T a, T b);
    }
}