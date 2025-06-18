using System.Text.RegularExpressions;

public class RequestValidator
{
	public (bool, string) ValidateNewSpare(Spare request)
	{
		bool isValid = true;
		string errorMassage = String.Empty;

		var nameRegex = new Regex(SparePropertysExpression.namePattern);

		if (!nameRegex.IsMatch(request.Name))
		{
			isValid = false;
			errorMassage = "Spare name is not valid. ";
		}

		var equipmentRegex = new Regex(SparePropertysExpression.equipmentPattern);

		if (!equipmentRegex.IsMatch(request.Equipment))
		{
			isValid = false;
			errorMassage += "Spere equipment name is not valid. ";
		}

		var ozmIdRegex = new Regex(SparePropertysExpression.ozmIdPattern);

		if (!equipmentRegex.IsMatch(request.OzmId.ToString()))
		{
			isValid = false;
			errorMassage += "Spere ozm id is not valid.";
		}

		return (isValid, errorMassage);
	}

	public (bool, string) ValidateProposal(IProposal updatedProposal)
	{
		bool isValid = true;
		string errorMassage = String.Empty;

		var nameRegex = new Regex(ProposalPropertysExtentions.laboratoryPattern);

		if (!nameRegex.IsMatch(updatedProposal.Laboratory))
		{
			isValid = false;
			errorMassage = "Proposal laboratory name is not valid ";
		}

		return (isValid, errorMassage);
	}
}