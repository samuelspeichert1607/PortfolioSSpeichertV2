using UnityEngine;

namespace ProjetSynthese
{
    public class Node : ScriptableObject
    {
        [SerializeField]
        private Vector2 position;

        [SerializeField]
        private bool isOnWall;

        [SerializeField]
        private Node[] neighbours;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool IsOnWall
        {
            get { return isOnWall; }
            set { isOnWall = value; }
        }

        public Node[] Neighbours
        {
            get { return neighbours; }
            set { neighbours = value; }
        }

        private bool isVisited;

        public bool IsVisited
        {
            get { return isVisited; }
            set { isVisited = value; }
        }

        public Node()
        {
            Neighbours = new Node[] { };
        }

        protected bool Equals(Node other)
        {
            return Position.Equals(other.Position) && IsOnWall == other.IsOnWall;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Node) obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return position.GetHashCode();
        }

        public static bool operator ==(Node left, Node right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Node left, Node right)
        {
            return !Equals(left, right);
        }
    }
}