

namespace ArsaRExCH.Model
{
    public class Pair
    {  /*Here about Class ID i chose for INT . its range is -2,147,483,648 to 2,147,483,647
         in fact we should use GUID but for now its ok */
        public int PairID { get; set; }
        public string PaiName { get; set; }
        public double ListPrice { get; set; }
        public DateTime ListedDate { get; set; }
        public string NetworkName { get; set; }
    }
}
