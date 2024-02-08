namespace DalApi
{
    public interface ITIme
    {
        public void SetStartDate(DateTime date);

        public DateTime? StartDate();

        public void reset();
    }
}