using ArsaRExCH.Model;

namespace ArsaRExCH.Interface
{
    public interface IPair
    {
        Task<bool> AddPair(Pair pair);
        Task<List<Pair>> GetAllPairs();
        Task RemovePairAsync(int pairId);
    }
}
