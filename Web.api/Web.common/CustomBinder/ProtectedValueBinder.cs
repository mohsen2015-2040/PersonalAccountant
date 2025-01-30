using Microsoft.AspNetCore.Mvc.ModelBinding;
using Services.Contract;
using Services.Implementation;
using System.Text;

namespace Web.Common.CustomBinder
{
    public class ProtectedValueBinder(IDataProtecting protection) : IModelBinder
    {
        //private readonly IDataProtecting _protection = protection;

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if(bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            var modelValue = bindingContext.ValueProvider.GetValue(modelName);

            if(modelValue == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            if(modelValue.FirstValue == "")
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, modelValue);

            var unprotectedBytes = protection.UnprotectData(modelValue.FirstValue);

            var result = BitConverter.ToInt32(unprotectedBytes);

            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
