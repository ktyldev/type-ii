using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TypeII {
    public class RecursiveTree<T> where T : MonoBehaviour {
        private List<Node> _allNodes = new List<Node>();
        private Node _root;
        private Func<T, IEnumerable<GameObject>> _getChildTemplates;

        public T root { get { return _root.data; } }

        public RecursiveTree(GameObject rootTemplate, Transform rootParent, Func<T, IEnumerable<GameObject>> getChildTemplates) {
            _getChildTemplates = getChildTemplates;

            var root = UnityEngine.Object.Instantiate(rootTemplate, rootParent);
            _root = CreateNode(root.GetComponent<T>());

            foreach (var template in _getChildTemplates(_root.data)) {
                AddChild(_root, template, _getChildTemplates);
            }
        }

        public Transform GetRootTransform() {
            return _root.data.transform;
        }

        public T GetParent(T data) {
            var parent = GetNode(data).parent;
            return parent != null ? parent.data : null;
        }

        public List<T> GetChildren(T parent) {
            return GetNode(parent).children
                .Select(n => n.data)
                .ToList();
        }

        private void AddChild(Node parent, GameObject dataTemplate, Func<T, IEnumerable<GameObject>> getChildTemplates) {
            var data = UnityEngine.Object.Instantiate(dataTemplate).GetComponent<T>();
            if (data == null)
                throw new Exception();

            data.transform.SetParent(parent.data.transform);
            var node = CreateNode(data, parent);

            foreach (var template in getChildTemplates(data)) {
                AddChild(node, template, getChildTemplates);
            }
        }

        public int GetDepth(T data) {
            return GetNode(data).depth;
        }

        private Node GetNode(T data) {
            var node = _allNodes.SingleOrDefault(n => n.data.Equals(data));
            if (node == null)
                throw new Exception();

            return node;
        }

        private Node CreateNode(T data, Node parent = null) {
            var node = new Node(parent, data);
            _allNodes.Add(node);
            return node;
        }

        class Node {
            public readonly int depth;
            public readonly T data;
            public readonly Node parent;
            public readonly List<Node> children;

            public Node(Node parent, T data) {
                this.data = data;
                this.parent = parent;
                // Debug.Log(depth);

                children = new List<Node>();

                depth = parent == null ? 0 : parent.depth + 1;

                if (parent != null) {
                    parent.children.Add(this);
                }
            }
        }
    }
}