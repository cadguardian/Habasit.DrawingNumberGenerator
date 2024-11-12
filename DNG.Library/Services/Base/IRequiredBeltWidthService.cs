public interface IRequiredBeltWidthService
{
    List<RequiredBeltWidth> GetRequiredBeltWidthStandards();

    Task InitializeAsync();
}