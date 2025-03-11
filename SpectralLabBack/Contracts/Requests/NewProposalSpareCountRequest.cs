public class NewProposalSpareCountRequest
{
	public NewProposalSpareCountRequest( int count, int receivedCount, Guid spareId)
	{
		Count = count;
		ReceivedCount = receivedCount;
		SpareId = spareId;
	}

	public int Count { get; }
	public int ReceivedCount { get; }
	public Guid SpareId { get; }
}