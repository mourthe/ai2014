using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Assemble
{
    [DataContract]
    public class Point
    {
        protected bool Equals(Point other)
        {
            return I == other.I && J == other.J && Terrain == other.Terrain;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = I;
                hashCode = (hashCode*397) ^ J;
                hashCode = (hashCode*397) ^ (int) Terrain;
                return hashCode;
            }
        }

        [DataMember (Name="i")]
        public int I { get; set; }

        [DataMember(Name = "j")]
        public int J { get; set; }

        public Terrain Terrain { get; set; }

        public bool HasBug { get; set; }
        public bool HasCockroach { get; set; }
        public bool HasHole { get; set; }
        public bool HasAmmo { get; set; }
        public bool HasVortex { get; set; }

        public Point(int i, int j, Terrain terrain)
        {
            this.Terrain = terrain;
            this.I = i;
            this.J = j;
        }

        public Point(int i, int j)
        {
            this.Terrain = 0;
            this.I = i;
            this.J = j;
        }

        public Point(int i, int j, int content) : this(i, j)
        {
            this.HasAmmo = this.HasBug = this.HasCockroach = this.HasHole = this.HasVortex = false;

            switch (content)
            {
                case 1:
                    this.HasAmmo = true;
                    break;
                case 2:
                    this.HasBug = true;
                    break;
                case 3:
                    this.HasCockroach = true;
                    break;
                case 4:
                    this.HasHole = true;
                    break;
                case 5:
                    this.HasVortex = true;
                    break;
                default:
                    // nada
            }   
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Point) obj);
        }
    }
}
