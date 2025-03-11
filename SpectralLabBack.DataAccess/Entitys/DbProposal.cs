public class DbProposal
{
	public Guid Id { get; set; }
	public string Laboratory { get; set; } = String.Empty;
	public int ProposalYearFor { get; set; }
	public DateOnly CreateDate { get; set; }
	public bool IsFinal { get; set; }

	public List<DbProposalSpareCount> ProposalSparesCount { get; set; }
}