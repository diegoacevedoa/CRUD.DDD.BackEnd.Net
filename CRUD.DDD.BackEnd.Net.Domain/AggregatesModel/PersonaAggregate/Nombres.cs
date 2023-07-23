namespace CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate
{
    public record Nombres
    {
        internal Nombres(string value)
        {
            Value = value;
        }

        public string Value { get; init; }

        public static Nombres Create(string value)
        {
            Validate(value);

            return new Nombres(value);
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("El valor de la propiedad Nombres no puede ser nulo o vacío.");
            }
            else if (value.Length > 100)
            {
                throw new ArgumentException("El máximo número de caracteres permitidos es 100.");
            }
        }
    }
}
