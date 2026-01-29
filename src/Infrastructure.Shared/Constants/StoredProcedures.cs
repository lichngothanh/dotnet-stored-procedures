namespace Infrastructure.Shared.Constants;

public static class StoredProcedures
{
    public static class SuperHero
    {
        public const string GetById = "sp_SuperHero_GetById";
        public const string GetAll = "sp_SuperHero_GetAll";
        public const string Insert = "sp_SuperHero_Insert";
        public const string Update = "sp_SuperHero_Update";
        public const string AssignToTeam = "sp_SuperHero_AssignToTeam";
    }
    
    public static class Team
    {
        public const string GetById = "sp_Team_GetById";
        public const string GetAll = "sp_Team_GetAll";
        public const string GetTeamsFromUniverse = "sp_Team_GetTeamsFromUniverse";
    }
}