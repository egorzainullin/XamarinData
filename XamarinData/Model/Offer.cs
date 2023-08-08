using System.Collections.Generic;

namespace XamarinData.Model
{
    public class Offer
    {
        public int Id { get; }

        public IDictionary<string, string> Properties { get; }

        public Offer(int id, IDictionary<string, string> properties)
        {
            Properties = properties;
            Id = id;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}