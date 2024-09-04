namespace ArsaRExCH.Model
{
    public class EventService
    {
        public event Action? RefreshBalances;

        public void TriggerRefreshBalances()
        {
            RefreshBalances?.Invoke();
        }
    }
}
