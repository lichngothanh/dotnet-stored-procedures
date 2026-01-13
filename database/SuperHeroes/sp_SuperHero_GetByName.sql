CREATE PROCEDURE sp_SuperHero_GetByName
    @HeroName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        HeroId,
        HeroName,
        RealName,
        PowerLevel,
        Universe,
        TeamId
    FROM SuperHeroes
    WHERE HeroName LIKE '%' + @HeroName + '%'
    ORDER BY HeroName;
END