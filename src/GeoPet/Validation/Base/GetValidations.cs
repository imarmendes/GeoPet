using FluentValidation.Results;


namespace GeoPet.Validation.Base;

public class GetValidations
{
    public static Response GetErrors(ValidationResult result)
    {
        var response = new Response();

        if (!result.IsValid)
        {
            foreach (var erro in result.Errors)
            {
                response.Report.Add(new Report()
                {
                    Code = erro.PropertyName,
                    Message = erro.ErrorMessage
                });
            }

            return response;
        }

        return response;
    }
}