namespace BlApi
{
    public interface ITime
    {
        /// <summary>
        /// Sets the start date for the project.
        /// </summary>
        /// <param name="date">The DateTime representing the start date.</param>
        public void SetStartDate(DateTime date);

        /// <summary>
        /// Gets the start date for the project.
        /// </summary>
        /// <returns>The start date as a DateTime, or null if not set.</returns>
        public DateTime? StartDate();
    }
}
