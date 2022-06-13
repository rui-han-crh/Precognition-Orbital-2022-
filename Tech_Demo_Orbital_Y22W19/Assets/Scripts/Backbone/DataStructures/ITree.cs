using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataStructures
{
    public interface ITree<T> : IGraphable<T>
    {
        public T GetParent(T data);
        public bool HasExactlyOneParent(T data);

        public bool HasExactlyNoParent(T data);
    }
}
