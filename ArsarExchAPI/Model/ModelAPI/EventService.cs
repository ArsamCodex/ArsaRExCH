namespace ArsarExchAPI.Model.ModelAPI
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
