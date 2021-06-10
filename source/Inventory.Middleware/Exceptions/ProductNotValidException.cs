using FluentValidation.Results;
using System.Collections.Generic;

namespace Inventory.Middleware.Exceptions
{
    public class ProductNotValidException : ProductException
    {
        private IEnumerable<ValidationResult> _validations;

        public ProductNotValidException(uint sku, IEnumerable<ValidationResult> validations)
        {
            base.sku = sku;
            this._validations = validations;
        }

        public override string Message
        {
            get {
                List<string> errorMessages = new List<string> { string.Format("Os dados produto com o sku [{0}] não estão válidos:", base.sku) };

                foreach (ValidationResult validationResult in this._validations)
                    foreach (ValidationFailure validationError in validationResult.Errors)
                        errorMessages.Add(validationError.ErrorMessage);

                return string.Join("\n", errorMessages); 
            }
        }
    }
}
