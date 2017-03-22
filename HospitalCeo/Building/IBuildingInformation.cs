using System;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Textures;

namespace HospitalCeo.Building
{
    interface IBuildingInformation
    {
        String GetName();
        Subtexture GetTexture();
        BuildingType GetBuildingType();
        BuildingCategory GetBuildingCatergory();
        Vector2 GetTileSize();
        bool UsesCustomRenderer();
        bool IsOneSquareWidth();
    }
}
