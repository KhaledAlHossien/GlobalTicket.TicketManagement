using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace GlobalTicket.TicketManagement.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> ValidatioErrors  { get; set; }
        public ValidationException( ValidationResult validationResult) { 
            ValidatioErrors = new List<string>();

            foreach (var validationError in validationResult.Errors)
            {
                ValidatioErrors.Add(validationError.ErrorMessage);
            }

        }
    }
}
