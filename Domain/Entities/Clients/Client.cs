namespace Domain.Entities.Clients
{
    using System;
    using System.Linq;

    public class Client : IEntity
    {
        [Obsolete("Only for reflection", true)]
        public Client() { }

        public Client(string name, string inn)
        {
            SetName(name);
            SetInn(inn);
        }

        public int Id { get; set; }

        public string Name { get; protected set; }

        public string Inn { get; protected set; }


        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        protected internal void SetInn(string inn)
        {
            if (string.IsNullOrWhiteSpace(inn))
                throw new ArgumentNullException(nameof(inn));

            if ((inn.Length != 10 && inn.Length != 12) || !inn.All(char.IsDigit))
            {
                throw new ArgumentException("Inn's length must be 10 or 12 digits");
            }

            Inn = inn;
        }
    }
}