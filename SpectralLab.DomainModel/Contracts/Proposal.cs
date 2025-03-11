public class Proposal : IProposal
{
	public Proposal(Guid id, string laboratory, int proposalYearFor, DateOnly createDate, bool isFinal, List<ProposalSpareCount> sparesCount)
	{
		Id = id;
		Laboratory = laboratory;
		ProposalYearFor = proposalYearFor;
		CreateDate = createDate;
		IsFinal = isFinal;
		SparesCount = sparesCount;
	}

	public Guid Id { get; }
	public string Laboratory { get; } = String.Empty;
	public int ProposalYearFor { get; }
	public DateOnly CreateDate { get; }
	public bool IsFinal { get; }

	public List<ProposalSpareCount> SparesCount { get; }

}