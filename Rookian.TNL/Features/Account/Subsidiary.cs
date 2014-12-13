using System.Runtime.ConstrainedExecution;

namespace Rookian.TNL.Features.Account
{
    public class Subsidiary
    {
        protected bool Equals(Subsidiary other)
        {
            return Id == other.Id && string.Equals(Name, other.Name) && IsPublic.Equals(other.IsPublic);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Subsidiary) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ IsPublic.GetHashCode();
                return hashCode;
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
    }
}