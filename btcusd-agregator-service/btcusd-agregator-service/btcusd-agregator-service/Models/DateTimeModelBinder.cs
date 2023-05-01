using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

public class DateTimeModelBinder : IModelBinder
{
    private readonly string[] _formats;

    public DateTimeModelBinder(string[] formats)
    {
        _formats = formats;
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        var value = valueProviderResult.FirstValue;

        if (string.IsNullOrEmpty(value))
        {
            bindingContext.Result = ModelBindingResult.Success(DateTime.MinValue);
            return Task.CompletedTask;
        }

        if (DateTime.TryParseExact(value, _formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDateTime))
        {
            if (parsedDateTime.Minute == 0 && parsedDateTime.Second == 0 && parsedDateTime.Millisecond == 0)
            {
                bindingContext.Result = ModelBindingResult.Success(parsedDateTime);
            }
            else
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "The provided datetime must have zero minutes, seconds, and milliseconds.");
                bindingContext.Result = ModelBindingResult.Failed();
            }
        }
        else
        {
            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid datetime format.");
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}
