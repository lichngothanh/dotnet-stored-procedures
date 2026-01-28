/* ============================================================
   DATABASE
   ============================================================ */
IF DB_ID('SuperHeroDb') IS NOT NULL
BEGIN
    ALTER DATABASE SuperHeroDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE SuperHeroDb;
END
GO

CREATE DATABASE SuperHeroDb;
GO

USE SuperHeroDb;
GO

/* ============================================================
   TABLES
   ============================================================ */

-- Teams
CREATE TABLE Teams
(
    TeamId     UNIQUEIDENTIFIER  NOT NULL,
    TeamName   NVARCHAR(100)     NOT NULL,
    Universe   NVARCHAR(20)      NOT NULL,

    CONSTRAINT PK_Teams PRIMARY KEY (TeamId),
    CONSTRAINT CK_Teams_Universe CHECK (Universe IN ('Marvel', 'DC'))
);
GO

-- SuperHeroes
CREATE TABLE SuperHeroes
(
    HeroId      UNIQUEIDENTIFIER  NOT NULL,
    HeroName    NVARCHAR(100)     NOT NULL,
    RealName    NVARCHAR(150)     NOT NULL,
    PowerLevel  INT               NOT NULL,
    Universe    NVARCHAR(20)      NOT NULL,
    TeamId      UNIQUEIDENTIFIER  NULL,

    CONSTRAINT PK_SuperHeroes PRIMARY KEY (HeroId),
    CONSTRAINT CK_SuperHeroes_PowerLevel CHECK (PowerLevel BETWEEN 0 AND 100),
    CONSTRAINT CK_SuperHeroes_Universe CHECK (Universe IN ('Marvel', 'DC')),
    CONSTRAINT FK_SuperHeroes_Teams
        FOREIGN KEY (TeamId) REFERENCES Teams(TeamId)
);
GO

/* ============================================================
   SEED DATA
   ============================================================ */

-- Teams
INSERT INTO Teams (TeamId, TeamName, Universe)
VALUES
-- Marvel
('11111111-1111-1111-1111-111111111111', N'Avengers', 'Marvel'),
('22222222-2222-2222-2222-222222222222', N'X-Men', 'Marvel'),
('44444444-4444-4444-4444-444444444444', N'Guardians of the Galaxy', 'Marvel'),

-- DC
('33333333-3333-3333-3333-333333333333', N'Justice League', 'DC'),
('55555555-5555-5555-5555-555555555555', N'Teen Titans', 'DC'),
('66666666-6666-6666-6666-666666666666', N'Suicide Squad', 'DC');
GO

-- SuperHeroes
INSERT INTO SuperHeroes
(HeroId, HeroName, RealName, PowerLevel, Universe, TeamId)
VALUES
-- =========================
-- MARVEL - AVENGERS
-- =========================
('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', N'Iron Man', N'Tony Stark', 90, 'Marvel',
 '11111111-1111-1111-1111-111111111111'),

('bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb', N'Captain America', N'Steve Rogers', 85, 'Marvel',
 '11111111-1111-1111-1111-111111111111'),

('cccccccc-cccc-cccc-cccc-cccccccccccc', N'Thor', N'Thor Odinson', 95, 'Marvel',
 '11111111-1111-1111-1111-111111111111'),

('dddddddd-dddd-dddd-dddd-dddddddddddd', N'Hulk', N'Bruce Banner', 98, 'Marvel',
 '11111111-1111-1111-1111-111111111111'),

('eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee', N'Black Widow', N'Natasha Romanoff', 80, 'Marvel',
 '11111111-1111-1111-1111-111111111111'),

-- =========================
-- MARVEL - X-MEN
-- =========================
('ffffffff-ffff-ffff-ffff-ffffffffffff', N'Wolverine', N'Logan', 92, 'Marvel',
 '22222222-2222-2222-2222-222222222222'),

('12121212-1212-1212-1212-121212121212', N'Professor X', N'Charles Xavier', 88, 'Marvel',
 '22222222-2222-2222-2222-222222222222'),

('13131313-1313-1313-1313-131313131313', N'Magneto', N'Erik Lehnsherr', 97, 'Marvel',
 '22222222-2222-2222-2222-222222222222'),

-- =========================
-- MARVEL - GUARDIANS
-- =========================
('14141414-1414-1414-1414-141414141414', N'Star-Lord', N'Peter Quill', 78, 'Marvel',
 '44444444-4444-4444-4444-444444444444'),

('15151515-1515-1515-1515-151515151515', N'Gamora', N'Gamora', 85, 'Marvel',
 '44444444-4444-4444-4444-444444444444'),

('16161616-1616-1616-1616-161616161616', N'Groot', N'Groot', 90, 'Marvel',
 '44444444-4444-4444-4444-444444444444'),

-- =========================
-- DC - JUSTICE LEAGUE
-- =========================
('17171717-1717-1717-1717-171717171717', N'Superman', N'Clark Kent', 100, 'DC',
 '33333333-3333-3333-3333-333333333333'),

('18181818-1818-1818-1818-181818181818', N'Batman', N'Bruce Wayne', 88, 'DC',
 '33333333-3333-3333-3333-333333333333'),

('19191919-1919-1919-1919-191919191919', N'Wonder Woman', N'Diana Prince', 96, 'DC',
 '33333333-3333-3333-3333-333333333333'),

('20202020-2020-2020-2020-202020202020', N'Flash', N'Barry Allen', 92, 'DC',
 '33333333-3333-3333-3333-333333333333'),

-- =========================
-- DC - TEEN TITANS
-- =========================
('21212121-2121-2121-2121-212121212121', N'Robin', N'Dick Grayson', 82, 'DC',
 '55555555-5555-5555-5555-555555555555'),

('22222222-aaaa-bbbb-cccc-222222222222', N'Raven', N'Raven', 94, 'DC',
 '55555555-5555-5555-5555-555555555555'),

-- =========================
-- DC - SUICIDE SQUAD
-- =========================
('23232323-2323-2323-2323-232323232323', N'Harley Quinn', N'Harleen Quinzel', 75, 'DC',
 '66666666-6666-6666-6666-666666666666'),

('24242424-2424-2424-2424-242424242424', N'Deadshot', N'Floyd Lawton', 83, 'DC',
 '66666666-6666-6666-6666-666666666666');
GO

/* ============================================================
   QUICK CHECK
   ============================================================ */

SELECT * FROM Teams;
SELECT * FROM SuperHeroes;
GO


/* =========================
   GET BY ID
   ========================= */
CREATE PROCEDURE sp_SuperHero_GetById
    @HeroId UNIQUEIDENTIFIER
AS
BEGIN
SELECT *
FROM SuperHeroes
WHERE HeroId = @HeroId
END
GO

/* =========================
   GET ALL
   ========================= */
CREATE PROCEDURE sp_SuperHero_GetAll
    AS
BEGIN
SELECT *
FROM SuperHeroes
END
GO

/* =========================
   INSERT
   ========================= */
-- CREATE PROCEDURE sp_SuperHero_Insert
--     @HeroId UNIQUEIDENTIFIER,
--     @HeroName NVARCHAR(100),
--     @RealName NVARCHAR(150),
--     @PowerLevel INT,
--     @Universe NVARCHAR(20),
--     @TeamId UNIQUEIDENTIFIER = NULL 
-- AS
-- BEGIN
-- INSERT INTO SuperHeroes
-- VALUES (@HeroId, @HeroName, @RealName, @PowerLevel, @Universe, @TeamId)
-- END
-- GO

-------- NEW VERSION INSERT WITH RETURNING HEROID --------
CREATE PROCEDURE sp_SuperHero_Insert
    @HeroName NVARCHAR(100),
    @RealName NVARCHAR(150),
    @PowerLevel INT,
    @Universe NVARCHAR(20),
    @TeamId UNIQUEIDENTIFIER = NULL 
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @HeroId UNIQUEIDENTIFIER = NEWID();

INSERT INTO SuperHeroes
(HeroId, HeroName, RealName, PowerLevel, Universe, TeamId)
VALUES
    (@HeroId, @HeroName, @RealName, @PowerLevel, @Universe, @TeamId);

SELECT @HeroId AS HeroId;
END
GO

/* =========================
   UPDATE
   ========================= */
CREATE PROCEDURE sp_SuperHero_Update
    @HeroId UNIQUEIDENTIFIER,
    @HeroName NVARCHAR(100),
    @RealName NVARCHAR(150),
    @PowerLevel INT,
    @Universe NVARCHAR(20)
AS
BEGIN
UPDATE SuperHeroes
SET HeroName = @HeroName,
    RealName = @RealName,
    PowerLevel = @PowerLevel,
    Universe = @Universe
WHERE HeroId = @HeroId
END
GO

/* =========================
   ASSIGN TO TEAM
   ========================= */
CREATE PROCEDURE sp_SuperHero_AssignToTeam
    @HeroId UNIQUEIDENTIFIER,
    @TeamId UNIQUEIDENTIFIER
AS
BEGIN
UPDATE SuperHeroes
SET TeamId = @TeamId
WHERE HeroId = @HeroId
END
GO
