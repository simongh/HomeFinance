namespace HomeFinance.Models
{
    public struct MatchesModel
    {
        public int Account { get; set; }

        public IEnumerable<Transactions.ResultModels.TransactionSummaryResult> Transactions { get; set; }
    }
}