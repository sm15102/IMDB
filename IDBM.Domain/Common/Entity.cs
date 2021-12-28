namespace Imdb.Domain.Common
{
    public abstract class Entity
    {
        protected Entity()
        {

        }

        protected Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
        public string CreatedBy { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string? LastModifiedBy { get; private set; }
        public DateTime? LastModifiedDate { get; private set; }


        public void SetCreator(string createdBy, DateTime createdDate)
        {
            CreatedBy = createdBy;
            CreatedDate = createdDate;
        }

        public void SetModificator(string lastModifiedBy, DateTime lastModifiedDate)
        {
            LastModifiedBy = lastModifiedBy;
            LastModifiedDate = lastModifiedDate;
        }


        public override bool Equals(object obj)
        {
            if (!(obj is Entity other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetRealType() != other.GetRealType())
                return false;

            if (Id == default || other.Id == default)
                return false;

            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetRealType().ToString() + Id).GetHashCode();
        }

        private Type GetRealType()
        {
            Type type = GetType();

            if (type.ToString().Contains("Castle.Proxies."))
                return type.BaseType!;

            return type;
        }
    }
}
