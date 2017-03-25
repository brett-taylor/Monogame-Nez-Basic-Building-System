using Nez;

namespace HospitalCeo.Building
{
    interface IBuildingCustomRenderer
    {
        void DoCustomRenderer(BuildingBaseRenderer renderer);
        System.Type GetRenderer();
    }
}
