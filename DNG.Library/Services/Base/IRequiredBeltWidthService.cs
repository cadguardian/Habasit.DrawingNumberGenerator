namespace DNG.Library.Services.Base;

public interface IRequiredBeltWidthService
{
    List<RequiredBeltWidth> GetRequiredBeltWidthStandards();

    Task InitializeAsync();
}