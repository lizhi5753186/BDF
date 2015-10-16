using System;

namespace Bdf.Domain.Entities
{
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }

        #region Override Object Members
        public override bool Equals(object obj)
        {
            if (!(obj is Entity<TPrimaryKey>))
                return false;
           
            // Same intances must be considerd as equal
            if (ReferenceEquals(this, obj))
                return true;

            var other = (Entity<TPrimaryKey>)obj;

            // Must have a IS-A relation of types or must be same type
            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.IsAssignableFrom(typeOfOther) && !typeOfOther.IsAssignableFrom(typeOfThis))
                return false;

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[{0} {1}]", GetType().Name, Id);
        }

        #endregion
 
        #region Static Methods
        public static bool operator ==(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            return !(left == right);
        }
        #endregion 
    }
}
