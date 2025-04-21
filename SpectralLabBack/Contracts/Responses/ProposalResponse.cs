public class ProposalResponse
{
	public Guid Id { get; set; }
	public string Lab { get; set; } = String.Empty;
	public int ProposalYear { get; set; }
	public DateOnly Date { get; set; }
	public bool Final { get; set; }

	public List<ProposalSpareResponse> ?Spares { get; set; }
}