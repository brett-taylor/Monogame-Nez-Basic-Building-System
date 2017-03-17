/*
* Brett Taylor
* Compasss enum
*/
namespace HospitalCeo.World
{
    public enum Compass
    {
        N,
        E,
        S,
        W,
        NE,
        SE,
        SW,
        NW
    }

    static class CompassnMethods
    {
        public static int ToInt(this Compass Compass)
        {
            switch (Compass)
            {
                case Compass.N:
                    return 0;
                case Compass.E:
                    return 1;
                case Compass.S:
                    return 2;
                case Compass.W:
                    return 3;
                case Compass.NE:
                    return 4;
                case Compass.SE:
                    return 5;
                case Compass.SW:
                    return 6;
                case Compass.NW:
                    return 7;
                default:
                    return 0;
            }
        }

        public static Compass ToCompass(int i)
        {
            switch (i)
            {
                case 0:
                    return Compass.N;
                case 1:
                    return Compass.E;
                case 2:
                    return Compass.S;
                case 3:
                    return Compass.W;
                case 4:
                    return Compass.NE;
                case 5:
                    return Compass.SE;
                case 6:
                    return Compass.SW;
                case 7:
                    return Compass.NW;
                default:
                    return 0;
            }
        }
    }
}