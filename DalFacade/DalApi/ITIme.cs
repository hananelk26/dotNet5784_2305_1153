namespace DalApi
{

    /// <summary>
    /// Public interface for managing time-related operations.
    /// </summary>
    public interface ITIme
    {
        /// <summary>
        /// Sets the start date for time tracking.
        /// </summary>
        /// <param name="date">The start date to set.</param>
        public void SetStartDate(DateTime date);

        /// <summary>
        /// Gets the start date for time tracking.
        /// </summary>
        /// <returns>The start date, or null if not set.</returns>
        public DateTime? StartDate();

        /// <summary>
        /// Resets the time-related information.
        /// </summary>
        public void reset();
    }
}