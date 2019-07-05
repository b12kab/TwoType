using System;
using System.Collections.Generic;
using FluentValidation;
using TwoTypeExample.Models;

namespace TwoTypeExample.Validator
{
    public class MessageValidator : AbstractValidator<MessageInfo>
    {
        private IList<int> foreignKeyIds;
        public MessageValidator(IList<int> messageIds)
        {
            foreignKeyIds = messageIds;
            RuleFor(c => c.Message).Must(n => ValidateStringEmpty(n)).WithMessage("Message cannot be empty.");
            RuleFor(c => c.FromContactId).Must(a => ValidateForeignKey(a)).WithMessage("Invalid or missing contact.");
        }

        bool ValidateStringEmpty(string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
                return true;

            return false;
        }

        bool ValidateForeignKey(int id)
        {
            if (foreignKeyIds != null && foreignKeyIds.Count > 0)
            {
                bool result = foreignKeyIds.Contains(id);
                if (result)
                    return true;
                else
                    return false;
            }

            // Default to OK
            return true;
        }
    }
}
