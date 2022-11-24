using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinance.Transactions.ResultModels
{
	internal class ImportResult
	{
		public TransactionResult? Transaction { get; set; }

		public bool Keep { get; set; }
	}
}
