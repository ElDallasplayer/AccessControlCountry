#region Dependencies
using System;
using Newtonsoft.Json;
#endregion

namespace PrincipalObjects.Objects
{
    public class Entity : IEquatable<Entity>
    {
        #region Properties
        [JsonProperty("id")]
        public Guid Id { get; private set; }
        #endregion

        public bool Equals(Entity other)
        {
            return other?.Id == Id;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as Entity);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return (left?.Id ?? null) == (right?.Id ?? null);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return left?.Id != right.Id;
        }
    }
}