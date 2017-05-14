namespace HospitalCeo.Building.Infrastructure.Interfaces
{
    public interface IBuildingBeforeBuild
    {
        bool ContinueBuild();
        void BeforeBuild(Building building);
    }
}
