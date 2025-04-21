using System.Text.Json.Serialization;

public class NewProposalRequest : IProposal
{
	public NewProposalRequest(string laboratory, int proposalYearFor, DateOnly createDate, bool isFinal, List<NewProposalSpareCountRequest> sparesCount)
	{
		Laboratory = laboratory;
		ProposalYearFor = proposalYearFor;
		CreateDate = createDate;
		IsFinal = isFinal;
		SparesCount = sparesCount;
	}

	public string Laboratory { get; } = String.Empty;
	public int ProposalYearFor { get; }

	//[JsonConverter(typeof(DateOnlyJsonConverter))]
	public DateOnly CreateDate { get; }
	public bool IsFinal { get; }

	public List<NewProposalSpareCountRequest> SparesCount { get; }
}