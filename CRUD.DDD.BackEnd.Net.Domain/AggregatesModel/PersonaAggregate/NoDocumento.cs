namespace CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate
{
    public record NoDocumento
    {
        internal NoDocumento(string value)
        {
            Value = value;
        }

        public string Value { get; init; }

        public static NoDocumento Create(string value)
        {
            Validate(value);

            return new NoDocumento(value);
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("El valor de la propiedad NoDocumento no puede ser nulo o vacío.");
            }
            else if (value.Length > 50)
            {
                throw new ArgumentException("El máximo número de caracteres permitidos es 50.");
            }
        }
    }
}
