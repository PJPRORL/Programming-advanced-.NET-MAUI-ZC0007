-----  Reset script ----
delete from GamePlatform;
delete from Game;
delete from [Platform];
delete from Review;
delete from AspNetUserRoles;
delete from AspNetRoles;
delete from AspNetUsers;
----------------------

INSERT INTO AspNetUsers 
(Id, UserName, NormalizedUserName, Email, NormalizedEmail, PasswordHash, PhoneNumber, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, LockoutEnd, ConcurrencyStamp, SecurityStamp, Voornaam, Achternaam, Telefoonnummer)
VALUES
('1', 'berta', 'BERTA', 'berta@example.com', 'BERTA@EXAMPLE.COM', 'AQAAAAIAAYagAAAAEMSteu70QjlWFp/up5pj96m1PrFLtkbT9rs6hqfOGp3mkh1FvZ1+kJZS9mhIsNg/Xg==', '0612345678', 1, 0, 0, 0, 0, NULL, 'SOME_CONCURRENCY_STAMP', 'SOME_SECURITY_STAMP', 'Bertanoise', 'Vermeer', '0612345678'),
('2', 'housemd', 'HOUSEMD', 'housemd@example.com', 'HOUSEMD@EXAMPLE.COM', 'AQAAAAIAAYagAAAAEMSteu70QjlWFp/up5pj96m1PrFLtkbT9rs6hqfOGp3mkh1FvZ1+kJZS9mhIsNg/Xg==', '0612345679', 1, 0, 0, 0, 0, NULL, 'SOME_CONCURRENCY_STAMP', 'SOME_SECURITY_STAMP', 'Bertje', 'Van den Bossche','0612345679'),
('3', 'louisse', 'LOUISSE', 'louisse@example.com', 'LOUISSE@EXAMPLE.COM', 'AQAAAAIAAYagAAAAEMSteu70QjlWFp/up5pj96m1PrFLtkbT9rs6hqfOGp3mkh1FvZ1+kJZS9mhIsNg/Xg==', '0612345680', 1, 0, 0, 0, 0, NULL, 'SOME_CONCURRENCY_STAMP', 'SOME_SECURITY_STAMP', 'Louisse', 'Van den Bossche', '');

INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
VALUES 
('1', 'Admin', 'ADMIN', NEWID()),
('2', 'Cyberhost', 'CYBERHOST', NEWID()),
('3', 'Cybercoach', 'CYBERCOACH', NEWID());

INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES 
('1', '1'),    -- berta = Admin
('2', '2'),    -- housemd = Cyberhost
('3', '3');    -- louisse = Cybercoach

SET IDENTITY_INSERT Game ON;
INSERT INTO Game (Id, Title, ReleaseDate, Price) 
VALUES
(1, 'The Witcher 3', '2015-05-19', 3900.99),
(2, 'Cyberpunk 2077', '2020-12-10', 59.99),
(3, 'Red Dead Redemption 2', '2018-10-26', 49.99),
(4, 'Elden Ring', '2022-02-25', 59.99),
(5, 'Grand Theft Auto V', '2013-09-17', 29.99),
(6, 'Minecraft', '2011-11-18', 26.95),
(7, 'Fortnite', '2017-07-21', 0.00),
(8, 'God of War', '2018-04-20', 39.99),
(9, 'Horizon Zero Dawn', '2017-02-28', 29.99),
(10, 'Spider-Man', '2018-09-07', 49.99),
(11, 'Assassins Creed Valhalla', '2020-11-10', 59.99),
(12, 'Call of Duty Modern Warfare', '2019-10-25', 59.99),
(13, 'FIFA 23', '2022-09-30', 69.99),
(14, 'NBA 2K23', '2022-09-09', 750.89),
(15, 'Resident Evil Village', '2021-05-07', 49.99),
(16, 'Dark Souls III', '2016-04-12', 39.99),
(17, 'Sekiro Shadows Die Twice', '2019-03-22', 59.99),
(18, 'Monster Hunter World', '2018-01-26', 29.99),
(19, 'Stardew Valley', '2016-02-26', 2.49),
(20, 'Terraria', '2011-05-16', 9.99),
(21, 'Among Us', '2018-06-15', 4.99),
(22, 'Fall Guys', '2020-08-04', 19.99),
(23, 'Halo Infinite', '2021-12-08', 59.99),
(24, 'Forza Horizon 5', '2021-11-09', 59.99),
(25, 'Zelda Breath of the Wild', '2017-03-03', 59.99),
(26, 'Super Mario Odyssey', '2017-10-27', 49.99);
SET IDENTITY_INSERT Game OFF;

SET IDENTITY_INSERT [Platform] ON;
INSERT INTO [Platform] (Id, Name) 
VALUES
(1, 'Windows'),
(2, 'Linux'),
(3, 'macOS'),
(4, 'Android'),
(5, 'iOS'),
(6, 'PlayStation'),
(7, 'Xbox'),
(8, 'Nintendo Switch'),
(9, 'Web'),
(10, 'ChromeOS'),
(11, 'HarmonyOS'),
(12, 'FireOS'),
(13, 'Tizen'),
(14, 'Solaris');
SET IDENTITY_INSERT [Platform] OFF;

SET IDENTITY_INSERT GamePlatform ON;

INSERT INTO GamePlatform (Id, GameId, PlatformId) VALUES
-- Game 1
(1, 1, 1),(2, 1, 3),(3, 1, 5),
-- Game 2
(4, 2, 2),(5, 2, 4),(6, 2, 6),
-- Game 3
(7, 3, 1),(8, 3, 2),(9, 3, 7),
-- Game 4
(10, 4, 3),(11, 4, 5),(12, 4, 8),
-- Game 5
(13, 5, 2),(14, 5, 4),(15, 5, 6),(16, 5, 9),
-- Game 6
(17, 6, 1),(18, 6, 3),(19, 6, 10),
-- Game 7
(20, 7, 2),(21, 7, 5),(22, 7, 11),
-- Game 8
(23, 8, 1),(24, 8, 4),(25, 8, 7),(26, 8, 12),
-- Game 9
(27, 9, 3),(28, 9, 6),(29, 9, 9),
-- Game 10
(30, 10, 2),(31, 10, 5),(32, 10, 8),(33, 10, 13),
-- Game 11
(34, 11, 1),(35, 11, 3),(36, 11, 4),
-- Game 12
(37, 12, 2),(38, 12, 6),(39, 12, 7),
-- Game 13
(40, 13, 1),(41, 13, 5),(42, 13, 8),
-- Game 14
(43, 14, 3),(44, 14, 4),(45, 14, 9),
-- Game 15
(46, 15, 2),(47, 15, 6),(48, 15, 11),
-- Game 16
(49, 16, 1),(50, 16, 3),(51, 16, 5),
-- Game 17
(52, 17, 2),(53, 17, 4),(54, 17, 7),
-- Game 18
(55, 18, 3),(56, 18, 6),(57, 18, 9),
-- Game 19
(58, 19, 1),(59, 19, 5),(60, 19, 10),
-- Game 20
(61, 20, 2),(62, 20, 4),(63, 20, 6),
-- Game 21
(64, 21, 1),(65, 21, 3),(66, 21, 7),
-- Game 22
(67, 22, 2),(68, 22, 5),(69, 22, 9),
-- Game 23
(70, 23, 1),(71, 23, 4),(72, 23, 6),
-- Game 24
(73, 24, 3),(74, 24, 5),(75, 24, 8),
-- Game 25
(76, 25, 2),(77, 25, 6),(78, 25, 10),
-- Game 26
(79, 26, 1),(80, 26, 3),(81, 26, 7);

SET IDENTITY_INSERT GamePlatform OFF;

SET IDENTITY_INSERT Review ON;

INSERT INTO Review (Id, GebruikerId, GamePlatformId, Rating, Comment, CreatedAt)
VALUES 
(1, '1', 1, 9, 'Amazing gameplay and graphics.', GETDATE()),
(2, '2', 1, 8, 'Very fun but a bit short.', GETDATE()),
(3, '3', 2, 7, 'Good game but has some bugs.', GETDATE()),
(4, NULL, 2, 6, 'Decent but nothing special.', GETDATE()),
(5, '1', 3, 10, 'One of the best games I played.', GETDATE()),
(6, '2', 3, 5, 'Average experience.', GETDATE()),
(7, NULL, 4, 7, 'Pretty enjoyable overall.', GETDATE()),
(8, '3', 4, 9, 'Loved the story and characters.', GETDATE()),
(9, '2', 5, 4, 'Not really my type of game.', GETDATE()),
(10, NULL, 5, 8, 'Surprisingly good!', GETDATE());

SET IDENTITY_INSERT Review OFF;