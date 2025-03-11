public class NewProposalSpareCountRequest
{
	public NewProposalSpareCountRequest( int count, int receivedCount, Guid spareId)
	{
		Count = count;
		ReceivedCount = receivedCount;
		//ProposalId = proposalId;
		SpareId = spareId;
	}

	public int Count { get; }
	public int ReceivedCount { get; }
	//public Guid ProposalId { get; }
	public Guid SpareId { get; }
}