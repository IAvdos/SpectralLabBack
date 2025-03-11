public interface IProposal
{
	public string Laboratory { get; } 
	public int ProposalYearFor { get; }
	public DateOnly CreateDate { get; }
	public bool IsFinal { get; }
}