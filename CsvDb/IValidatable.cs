/* (C) 2016 Premysl Fara */

namespace CsvDb
{
    public interface IValidatable
    {
        /// <summary>
        /// Validates this instance.
        /// Throws ValidationException, if this instance is not valid.
        /// </summary>
        void Validate();
    }
}
