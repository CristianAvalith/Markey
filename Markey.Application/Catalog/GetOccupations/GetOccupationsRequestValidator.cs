using FluentValidation;

namespace Markey.Application.Catalog.GetOccupations;

public class GetOccupationsRequestValidator : AbstractValidator<GetOccupationsRequest>
{
    public GetOccupationsRequestValidator()
    {
        // No hay validaciones específicas para este request ya que no tiene parámetros
    }
}