namespace CRUD.DDD.BackEnd.Net.Domain.AggregatesModel.PersonaAggregate
{
    public record IdPersona
    {
        internal IdPersona(int value)
        {
            Value = value;
        }

        public int Value { get; init; }

        public static IdPersona Create(int value)
        {
            Validate(value);

            return new IdPersona(value);
        }

        private static void Validate(int value)
        {
            if (value == 0)
            {
                throw new ArgumentException("El valor de la propiedad IdPersona no puede ser 0.");
            }
        }
    }
}
