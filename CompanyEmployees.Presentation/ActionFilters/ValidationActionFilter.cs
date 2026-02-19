using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CompanyEmployees.Presentation.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public ValidationFilterAttribute() { }


        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var action = filterContext.RouteData.Values["action"];
            var controller = filterContext.RouteData.Values["controller"];
            var param = filterContext.ActionArguments
                .SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;

            if (param == null)
            {
                filterContext.Result = new BadRequestObjectResult
                                        ($"Object Is Null. Controller:{controller}, Action:{action}");
                return;
            }
            if (!filterContext.ModelState.IsValid)
            {
                filterContext.Result = new UnprocessableEntityObjectResult(filterContext.ModelState);
            }

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }
    }
}
