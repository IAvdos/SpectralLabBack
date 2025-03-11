public class DbEmploye
{
	public Guid Id { get; set; }
	public string Login { get; set; } = String.Empty;
	public string Pasword { get; set; } = String.Empty;
	public string Name { get; set; } = String.Empty;
	public string Surname { get; set; } = String.Empty;
	public string Laboratory { get; set; } = String.Empty;
}