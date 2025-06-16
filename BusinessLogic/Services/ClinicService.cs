using Core.Interfaces;

namespace BusinessLogic.Services;

public partial class ClinicService
{
    private readonly IDataAccessStrategy _strategy;

    public ClinicService(IDataAccessStrategy strategy)
    {
        _strategy = strategy;
    }
}