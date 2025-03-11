public class Employe
{
	public Employe(Guid id, string login, string pasword, string name, string surname, string laboratory)
	{
		Id = id;
		Login = login;
		Pasword = pasword;
		Name = name;
		Surname = surname;
		Laboratory = laboratory;
	}

	public Guid Id { get; }
	public string Login { get; } = string.Empty;
	public string Pasword { get; } = String.Empty;
	public string Name { get; } = String.Empty;
	public string Surname { get; } = String.Empty;
	public string Laboratory { get; } = String.Empty;
}