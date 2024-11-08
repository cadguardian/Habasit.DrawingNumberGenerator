// Interface for the Belt Layout Service
using System.Collections.Immutable;

public interface IBeltLayoutService
{
    BeltLayoutResult GenerateBeltLayout(
        BeltLayoutRequest layoutRequest,
        ImmutableArray<ModuleSpec> modules
    );

    BeltLayoutRequest GetUserBeltLayoutRequest();
}