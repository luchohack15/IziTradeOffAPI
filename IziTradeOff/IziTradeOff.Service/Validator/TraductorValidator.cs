using FluentValidation;
using IziTradeOff.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace IziTradeOff.Service.Validator
{
    public class TraductorValidator : AbstractValidator<TraduccionDto>
    {
        public TraductorValidator()
        {
            RuleFor(x => x.Llave).NotEmpty();
            RuleFor(x => x.Valor).NotEmpty();
            RuleFor(x => x.Lang).NotEmpty();
            RuleFor(x => x.Tipo).NotEmpty();
            RuleFor(x => x.Estado).NotEmpty();
        }
    }
}
