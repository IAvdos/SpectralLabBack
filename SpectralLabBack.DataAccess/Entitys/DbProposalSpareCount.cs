using System.ComponentModel.DataAnnotations;

public class DbProposalSpareCount
{
	public Guid Id { get; set; }
	public int Count { get; set; }
	public int ReceivedCount { get; set; }

	[Required]
	public Guid ProposalId { get; set; }
	public DbProposal Proposal { get; set; } = null!;

	[Required]
	public Guid SpareId { get; set; }
	public DbSpare Spare { get; set; } = null!;
}