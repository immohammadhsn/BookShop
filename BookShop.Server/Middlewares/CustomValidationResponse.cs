using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static BookShop.Shared.ServiceResponses;

namespace BookShop.Server.Middlewares;

public class CustomValidationResponse
{
    public IActionResult CustomErrorResponse(ActionContext actionContext)
    {
        var errorRecordList = actionContext.ModelState
            .Where(modelError => modelError.Value.Errors.Count > 0)
            .Select(modelError => new IdentityError
            {
                Code = modelError.Key,
                Description = modelError.Value.Errors.FirstOrDefault()?.ErrorMessage
            });

        var generalResponse = new GeneralResponse(false, "Validation error occurred", errorRecordList);

        return new BadRequestObjectResult(generalResponse);
    }
}
