public class ProposalSpareCount
{
	//TODO: PROPOSALID is the question ned here or not
	public ProposalSpareCount(Guid id, int count, int receivedCount, Guid spareId)
	{
		Id = id;
		Count = count;
		ReceivedCount = receivedCount;
		//ProposalId = proposalId;
		SpareId = spareId;
	}

	public Guid Id { get; }
	public int Count { get; set; }
	public int ReceivedCount { get; }
	//public Guid ProposalId { get; }
	public Guid SpareId { get; }
}