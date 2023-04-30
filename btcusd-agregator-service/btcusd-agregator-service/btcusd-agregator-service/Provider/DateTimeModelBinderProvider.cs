using Microsoft.AspNetCore.Mvc.ModelBinding;

public class DateTimeModelBinderProvider : IModelBinderProvider
{
    private readonly string[] _formats;

    public DateTimeModelBinderProvider(string[] formats)
    {
        _formats = formats;
    }

    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        if (context.Metadata.ModelType == typeof(DateTime))
        {
            return new DateTimeModelBinder(_formats);
        }

        return null;
    }
}
