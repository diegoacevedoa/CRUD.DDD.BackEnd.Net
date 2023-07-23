namespace CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate
{
    public record Apellidos
    {
        internal Apellidos(string value)
        {
            Value = value;
        }

        public string Value { get; init; }

        public static Apellidos Create(string value)
        {
            Validate(value);

            return new Apellidos(value);
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("El valor de la propiedad Apellidos no puede ser nulo o vacío.");
            }
            else if (value.Length > 100)
            {
                throw new ArgumentException("El máximo número de caracteres permitidos es 100.");
            }
        }
    }
}
