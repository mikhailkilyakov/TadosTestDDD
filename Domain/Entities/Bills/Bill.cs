namespace Domain.Entities.Bills
{
    using System;
    using System.Threading;
    using Clients;

    public class Bill : IEntity
    {
        [Obsolete("Only for reflection", true)]
        public Bill() { }

        public Bill(Client client, decimal sum)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (sum <= 0.0m)
                throw new ArgumentOutOfRangeException(nameof(sum), "Sum must be greater than 0");

            Client = client;
            Sum = sum;
            CreatedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public Client Client { get; protected set; }

        public decimal Sum { get; protected set; }

        public int Number { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public DateTime? PayedAt { get; protected set; }

        public bool IsPayed => PayedAt != null;

        public string DisplayName => $"{CreatedAt:MM.yyyy}-{Number:D6}";

        protected internal void SetNumber(int number)
        {
            if (number < 1)
                throw new ArgumentOutOfRangeException(nameof(number), "Number must be greater than or equals 1");

            Number = number;
        }
    }
}