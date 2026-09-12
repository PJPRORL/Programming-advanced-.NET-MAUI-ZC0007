IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Publishers')
BEGIN
CREATE DATABASE [Publishers]
END

GO
USE [Publishers]
GO

SET NOCOUNT ON;
GO

DROP TABLE IF EXISTS TitleAuthor;

DROP TABLE IF EXISTS Sale;

DROP TABLE IF EXISTS Employee;

DROP TABLE IF EXISTS Book;

DROP TABLE IF EXISTS Store;

DROP TABLE IF EXISTS Publisher;

DROP TABLE IF EXISTS Job;

DROP TABLE IF EXISTS Author;

DROP SCHEMA IF EXISTS Pubs;

GO
    CREATE SCHEMA Pubs;

GO
    CREATE TABLE Publisher (
        id INT PRIMARY KEY NOT NULL,
        name varchar(40),
        city varchar(20),
        state char(2),
        country varchar(30) default 'USA'
    );

CREATE TABLE Job (
    id int NOT NULL IDENTITY (1, 1) PRIMARY KEY,
    description varchar(50) NULL,
    minLevel tinyint NOT NULL check (minLevel >= 10),
    maxLevel tinyint NOT NULL check (maxLevel <= 250)
);

CREATE TABLE Author (
    id int NOT NULL PRIMARY KEY IDENTITY (1, 1),
    lastName varchar(40) NOT NULL,
    firstName varchar(20) NOT NULL,
    phone char(12) NULL,
    address varchar(40),
    city varchar(20),
    state char(2),
    zip char(5),
    contract bit NOT NULL
);

create index INDEX_AUTHOR_NAME on Author (lastName, firstName);

CREATE TABLE Book (
    id int PRIMARY KEY NOT NULL,
    title varchar(80) NOT NULL,
    type char(12) default 'UNDECIDED' NOT NULL,
    publisherId int,
    price money,
    advance money,
    royalty int,
    sales int,
    notes varchar(200),
    releaseDate datetime default getdate() NOT NULL,
    CONSTRAINT FK_Title_Publisher FOREIGN KEY ([publisherId]) references Publisher (id),
);

CREATE TABLE TitleAuthor (
    id int NOT NULL PRIMARY KEY IDENTITY (1, 1),
    authorId int NOT NULL,
    bookId int NOT NULL CONSTRAINT FK_TitleAuthor_Author FOREIGN KEY (authorId) REFERENCES Author (id),
    CONSTRAINT FK_TitleAuthor_Book FOREIGN KEY (bookId) REFERENCES Book (id)
);

create index INDEX_Book_Title on Book (title);

CREATE TABLE Store (
    id int NOT NULL PRIMARY KEY,
    name varchar(40) NOT NULL,
    address varchar(40) NOT NULL,
    city varchar(20) NOT NULL,
    state char(2) NOT NULL,
    zip char(5) NOT NULL
);

CREATE TABLE Employee (
    id int NOT NULL PRIMARY KEY IDENTITY (1, 1),
    code varchar(20) NOT NULL,
    firstName varchar(20) NOT NULL,
    lastName varchar(30) NOT NULL,
    jobId int NULL,
    jobLevel tinyint default 10,
    publisherId int NULL,
    hireDate datetime default getdate() NOT NULL,
    CONSTRAINT FK_Employee_Job FOREIGN KEY (jobId) REFERENCES Job (id),
    CONSTRAINT FK_Employee_Publisher FOREIGN KEY (publisherId) REFERENCES Publisher (id)
);

CREATE TABLE Sale (
    id int PRIMARY KEY NOT NULL IDENTITY (1, 1),
    storeId int NOT NULL,
    orderNumber varchar(20) NOT NULL,
    orderDate datetime NOT NULL,
    amount smallint NOT NULL,
    bookId int NOT NULL,
    CONSTRAINT FK_Sale_Store FOREIGN KEY (storeId) REFERENCES Store (id),
    CONSTRAINT FK_Sale_Title FOREIGN KEY (bookId) REFERENCES Book (id)
);

INSERT INTO
    Publisher
VALUES
    (0736, 'New Moon Books', 'Boston', 'MA', 'USA'),
    (
        0877,
        'Binnet & Hardley',
        'Washington',
        'DC',
        'USA'
    ),
    (
        1389,
        'Algodata Infosystems',
        'Berkeley',
        'CA',
        'USA'
    ),
    (
        1622,
        'Five Lakes Publishing',
        'Chicago',
        'IL',
        'USA'
    ),
    (1756, 'Ramona Publishers', 'Dallas', 'TX', 'USA'),
    (9901, 'GGG&G', N'München', NULL, 'Germany'),
    (9952, 'Scootney Books', 'New York', 'NY', 'USA'),
    (
        9999,
        'Lucerne Publishing',
        'Paris',
        NULL,
        'France'
    );

INSERT INTO
    Job (description, minLevel, maxLevel)
VALUES
    ('New Hire - Job not specified', 10, 10),
    ('Chief Executive Officer', 200, 250),
    ('Business Operations Manager', 175, 225),
    (NULL, 175, 250),
    ('Publisher', 150, 250),
    ('Managing Editor', 140, 225),
    ('Marketing Manager', 120, 200),
    ('Public Relations Manager', 100, 175),
    ('Acquisitions Manager', 75, 175),
    ('Productions Manager', 75, 165),
    ('Operations Manager', 75, 150),
    ('Editor', 25, 100),
    ('Sales Representative', 25, 100),
    (NULL, 25, 100);

INSERT INTO
    Author (
        lastName,
        firstName,
        phone,
        address,
        city,
        state,
        zip,
        contract
    )
VALUES
    (
        'White',
        'Johnson',
        '408 496-7223',
        '10932 Bigge Rd.',
        'Menlo Park',
        'CA',
        '94025',
        1
    ),
    (
        'Green',
        'Marjorie',
        '415 986-7020',
        '309 63rd St. #411',
        'Oakland',
        'CA',
        '94618',
        1
    ),
    (
        'Carson',
        'Cheryl',
        '415 548-7723',
        '589 Darwin Ln.',
        'Berkeley',
        'CA',
        '94705',
        1
    ),
    (
        'O''Leary',
        'Michael',
        '408 286-2428',
        '22 Cleveland Av. #14',
        'San Jose',
        'CA',
        '95128',
        1
    ),
    (
        'Straight',
        'Dean',
        '415 834-2919',
        '5420 College Av.',
        'Oakland',
        'CA',
        '94609',
        1
    ),
    (
        'Smith',
        'Meander',
        '913 843-0462',
        '10 Mississippi Dr.',
        'Lawrence',
        'KS',
        '66044',
        0
    ),
    (
        'Bennet',
        'Abraham',
        '415 658-9932',
        '6223 Bateman St.',
        'Berkeley',
        'CA',
        '94705',
        1
    ),
    (
        'Dull',
        'Ann',
        '415 836-7128',
        '3410 Blonde St.',
        'Palo Alto',
        'CA',
        '94301',
        1
    ),
    (
        'Gringlesby',
        'Burt',
        '707 938-6445',
        'PO Box 792',
        'Covelo',
        'CA',
        '95428',
        1
    ),
    (
        'Locksley',
        'Charlene',
        '415 585-4620',
        '18 Broadway Av.',
        'San Francisco',
        'CA',
        '94130',
        1
    ),
    (
        'Greene',
        'Morningstar',
        '615 297-2723',
        '22 Graybar House Rd.',
        'Nashville',
        'TN',
        '37215',
        0
    ),
    (
        'Blotchet-Halls',
        'Reginald',
        '503 745-6402',
        '55 Hillsdale Bl.',
        'Corvallis',
        'OR',
        '97330',
        1
    ),
    (
        'Yokomoto',
        'Akiko',
        '415 935-4228',
        '3 Silver Ct.',
        'Walnut Creek',
        'CA',
        '94595',
        1
    ),
    (
        'del Castillo',
        'Innes',
        '615 996-8275',
        '2286 Cram Pl. #86',
        'Ann Arbor',
        'MI',
        '48105',
        1
    ),
    (
        'DeFrance',
        'Michel',
        '219 547-9982',
        '3 Balding Pl.',
        'Gary',
        'IN',
        '46403',
        1
    ),
    (
        'Stringer',
        'Dirk',
        '415 843-2991',
        '5420 Telegraph Av.',
        'Oakland',
        'CA',
        '94609',
        0
    ),
    (
        'MacFeather',
        'Stearns',
        '415 354-7128',
        '44 Upland Hts.',
        'Oakland',
        'CA',
        '94612',
        1
    ),
    (
        'Karsen',
        'Livia',
        '415 534-9219',
        '5720 McAuley St.',
        'Oakland',
        'CA',
        '94609',
        1
    ),
    (
        'Panteley',
        'Sylvia',
        '301 946-8853',
        '1956 Arlington Pl.',
        'Rockville',
        'MD',
        '20853',
        1
    ),
    (
        'Hunter',
        'Sheryl',
        '415 836-7128',
        '3410 Blonde St.',
        'Palo Alto',
        'CA',
        '94301',
        1
    ),
    (
        'McBadden',
        'Heather',
        '707 448-4982',
        '301 Putnam',
        'Vacaville',
        'CA',
        '95688',
        0
    ),
    (
        'Ringer',
        'Anne',
        '801 826-0752',
        '67 Seventh Av.',
        'Salt Lake City',
        'UT',
        '84152',
        1
    ),
    (
        'Ringer',
        'Albert',
        '801 826-0752',
        '67 Seventh Av.',
        'Salt Lake City',
        'UT',
        '84152',
        1
    );

INSERT INTO
    Book (
        id,
        title,
        type,
        publisherId,
        price,
        advance,
        royalty,
        sales,
        notes,
        releaseDate
    )
VALUES
    (
        1032,
        'The Busy Executive''s Database Guide',
        'business',
        1389,
        19.9900,
        5000.0000,
        10,
        4095,
        'An overview of available database systems with emphasis on common business applications. Illustrated.',
        '1991-06-12'
    ),
    (
        1111,
        'Cooking with Computers: Surreptitious Balance Sheets',
        'business',
        1389,
        11.9500,
        5000.0000,
        10,
        3876,
        'Helpful hints on how to use your electronic resources to the best advantage.',
        '1991-06-09'
    ),
    (
        2075,
        'You Can Combat Computer Stress!',
        'business',
        0736,
        2.9900,
        10125.0000,
        24,
        18722,
        'The latest medical and psychological techniques for living with the electronic office. Easy-to-understand explanations.',
        '1991-06-30'
    ),
    (
        7832,
        'Straight Talk About Computers',
        'business',
        NULL,
        19.9900,
        5000.0000,
        10,
        4095,
        'Annotated analysis of what computers can do for you: a no-hype guide for the critical user.',
        '1991-06-22'
    ),
    (
        2222,
        'Silicon Valley Gastronomic Treats',
        'mod_cook',
        0877,
        19.9900,
        0.0000,
        12,
        2032,
        'Favorite recipes for quick, easy, and elegant meals.',
        '1991-06-09'
    ),
    (
        3021,
        'The Gourmet Microwave',
        'mod_cook',
        0877,
        2.9900,
        15000.0000,
        24,
        22246,
        'Traditional French gourmet recipes adapted for modern microwave cooking.',
        '1991-06-18'
    ),
    (
        3026,
        'The Psychology of Computer Cooking',
        'UNDECIDED',
        0877,
        NULL,
        NULL,
        NULL,
        NULL,
        NULL,
        '2022-02-01'
    ),
    (
        1035,
        'But Is It User Friendly?',
        'popular_comp',
        1389,
        22.9500,
        7000.0000,
        16,
        8780,
        'A survey of software for the naive user, focusing on the ''friendliness'' of each.',
        '1991-06-30'
    ),
    (
        8888,
        'Secrets of Silicon Valley',
        'popular_comp',
        1389,
        20.0000,
        8000.0000,
        10,
        4095,
        'Muckraking reporting on the world''s largest computer hardware and software manufacturers.',
        '1994-06-12'
    ),
    (
        9999,
        'Net Etiquette',
        'popular_comp',
        NULL,
        NULL,
        NULL,
        NULL,
        NULL,
        'A must-read for computer conferencing.',
        '2022-02-01'
    ),
    (
        1372,
        'Computer Phobic AND Non-Phobic Individuals: Behavior Variations',
        'psychology',
        0877,
        21.5900,
        7000.0000,
        10,
        375,
        'A must for the specialist, this book examines the difference between those who hate and fear computers and those who don''t.',
        '1991-10-21'
    ),
    (
        2091,
        'Is Anger the Enemy?',
        'psychology',
        NULL,
        10.9500,
        2275.0000,
        12,
        2045,
        'Carefully researched study of the effects of strong emotions on the body. Metabolic charts included.',
        '1991-06-15'
    ),
    (
        2106,
        'Life Without Fear',
        'psychology',
        0736,
        7.0000,
        6000.0000,
        10,
        111,
        'New exercise, meditation, and nutritional techniques that can reduce the shock of daily interactions. Popular audience. Sample menus included, exercise video available separately.',
        '1991-10-05'
    ),
    (
        3333,
        'Prolonged Data Deprivation: Four Case Studies',
        'psychology',
        0736,
        19.9900,
        2000.0000,
        10,
        4072,
        'What happens when the data runs dry?  Searching evaluations of information-shortage effects.',
        '1991-06-12'
    ),
    (
        7777,
        'Emotional Security: A New Algorithm',
        'psychology',
        0736,
        7.9900,
        4000.0000,
        10,
        3336,
        'Protecting yourself and your loved ones from undue emotional stress in the modern world. Use of computer and nutritional aids emphasized.',
        '1991-06-12'
    ),
    (
        3218,
        'Onions, Leeks, and Garlic: Cooking Secrets of the Mediterranean',
        'trad_cook',
        0877,
        20.9500,
        7000.0000,
        10,
        375,
        'Profusely illustrated in color, this makes a wonderful gift book for a cuisine-oriented friend.',
        '1991-10-21'
    ),
    (
        4203,
        'Fifty Years in Buckingham Palace Kitchens',
        'trad_cook',
        0877,
        11.9500,
        4000.0000,
        14,
        15096,
        'More anecdotes from the Queen''s favorite cook describing life among English royalty. Recipes, techniques, tender vignettes.',
        '1991-06-12'
    ),
    (
        7778,
        'Sushi, Anyone?',
        'trad_cook',
        0877,
        14.9900,
        8000.0000,
        10,
        4095,
        'Detailed instructions on how to make authentic Japanese sushi in your spare time.',
        '2054-06-12'
    );

INSERT INTO
    TitleAuthor (authorId, bookId)
VALUES
    (1, 3333),
    (2, 1032),
    (2, 2075),
    (3, 1035),
    (4, 1111),
    (4, 7778),
    (5, 7832),
    (7, 1032),
    (8, 8888),
    (9, 7778),
    (10, 9999),
    (10, 7777),
    (12, 4203),
    (13, 7778),
    (14, 2222),
    (15, 3021),
    (17, 1111),
    (17, 1372),
    (18, 1372),
    (19, 3218),
    (20, 8888),
    (22, 3021),
    (22, 2091),
    (23, 2091),
    (23, 2106);

INSERT INTO
    Store (id, name, address, city, state, zip)
VALUES
    (
        6380,
        'Eric the Read Books',
        '788 Catamaugus Ave.',
        'Seattle',
        'WA',
        '98056'
    ),
    (
        7066,
        'Barnum''s',
        '567 Pasadena Ave.',
        'Tustin',
        'CA',
        '92789'
    ),
    (
        7067,
        'News & Brews',
        '577 First St.',
        'Los Gatos',
        'CA',
        '96745 '
    ),
    (
        7131,
        'Doc-U-Mat: Quality Laundry and Books',
        '24-A Avogadro Way',
        'Remulade',
        'WA',
        '98014'
    ),
    (
        7896,
        'Fricative Bookshop',
        '89 Madison St.',
        'Fremont',
        'CA',
        '90019'
    ),
    (
        8042,
        'Bookbeat',
        '679 Carson St.',
        'Portland',
        'OR',
        '89076'
    );

INSERT INTO
    Sale (storeId, orderNumber, orderDate, amount, bookId)
VALUES
    (6380, '6871', '1994-09-14', 5, 1032),
    (6380, '722a', '1994-09-13', 3, 2091),
    (7066, 'A2976', '1993-05-24', 50, 8888),
    (7066, 'QA7442.3', '1994-09-13', 75, 2091),
    (7067, 'D4482', '1994-09-14', 10, 2091),
    (7067, 'P2121', '1992-06-15', 40, 3218),
    (7067, 'P2121', '1992-06-15', 20, 4203),
    (7067, 'P2121', '1992-06-15', 20, 7778),
    (7131, 'N914008', '1994-09-14', 20, 2091),
    (7131, 'N914014', '1994-09-14', 25, 3021),
    (7131, 'P3087a', '1993-05-29', 20, 1372),
    (7131, 'P3087a', '1993-05-29', 25, 2106),
    (7131, 'P3087a', '1993-05-29', 15, 3333),
    (7131, 'P3087a', '1993-05-29', 25, 7777),
    (7896, 'QQ2299', '1993-10-28', 15, 7832),
    (7896, 'TQ456', '1993-12-12', 10, 2222),
    (7896, 'X999', '1993-02-21', 35, 2075),
    (8042, '423LL922', '1994-09-14', 15, 3021),
    (8042, '423LL930', '1994-09-14', 10, 1032),
    (8042, 'P723', '1993-03-11', 25, 1111),
    (8042, 'QA879.1', '1993-05-22', 30, 1035);

INSERT INTO
    Employee (
        code,
        firstName,
        lastName,
        jobId,
        jobLevel,
        publisherId,
        hireDate
    )
VALUES
    (
        'PMA42628M',
        'Paolo',
        'Accorti',
        13,
        35,
        0877,
        '1992-08-27'
    ),
    (
        'PSA89086M',
        'Pedro',
        'Afonso',
        14,
        NULL,
        1389,
        '1990-12-24'
    ),
    (
        'VPA30890F',
        'Victoria',
        'Ashworth',
        6,
        140,
        0877,
        '1990-09-13'
    ),
    (
        'H-B39728F',
        'Helen',
        'Bennett',
        12,
        35,
        0877,
        '1989-09-21'
    ),
    (
        'L-B31947F',
        'Lesley',
        'Brown',
        7,
        120,
        0877,
        '1991-02-13'
    ),
    (
        'F-C16315M',
        'Francisco',
        'Chang',
        4,
        227,
        9952,
        '1990-11-03'
    ),
    (
        'PTC11962M',
        'Philip',
        'Cramer',
        NULL,
        215,
        9952,
        '1989-11-11'
    ),
    (
        'A-C71970F',
        'Aria',
        'Cruz',
        10,
        87,
        1389,
        '1991-10-26'
    ),
    (
        'AMD15433F',
        'Ann',
        'Devon',
        3,
        200,
        9952,
        '1991-07-16'
    ),
    (
        'ARD36773F',
        'Anabela',
        'Domingues',
        8,
        100,
        0877,
        '1993-01-27'
    ),
    (
        'PHF38899M',
        'Peter',
        'Franken',
        10,
        75,
        0877,
        '1992-05-17'
    ),
    (
        'PXH22250M',
        'Paul',
        'Henriot',
        NULL,
        NULL,
        0877,
        '1993-08-19'
    ),
    (
        'CFH28514M',
        'Carlos',
        'Hernadez',
        5,
        211,
        9999,
        '1989-04-21'
    ),
    (
        'PDI47470M',
        'Palle',
        'Ibsen',
        NULL,
        195,
        0736,
        '1993-05-09'
    ),
    (
        'KJJ92907F',
        'Karla',
        'Jablonski',
        9,
        170,
        9999,
        '1994-03-11'
    ),
    (
        'KFJ64308F',
        'Karin',
        'Josephs',
        14,
        100,
        0736,
        '1992-10-17'
    ),
    (
        'MGK44605M',
        'Matti',
        'Karttunen',
        6,
        220,
        0736,
        '1994-05-01'
    ),
    (
        'POK93028M',
        'Pirkko',
        'Koskitalo',
        10,
        80,
        9999,
        '1993-11-29'
    ),
    (
        'JYL26161F',
        'Janine',
        'Labrune',
        5,
        172,
        9901,
        '1991-05-26'
    ),
    (
        'M-L67958F',
        'Maria',
        'Larsson',
        NULL,
        135,
        1389,
        '1992-03-27'
    ),
    (
        'Y-L77953M',
        'Yoshi',
        'Latimer',
        12,
        32,
        1389,
        '1989-06-11'
    ),
    (
        'LAL21447M',
        'Laurence',
        'Lebihan',
        5,
        175,
        0736,
        '1990-06-03'
    ),
    (
        'ENL44273F',
        'Elizabeth',
        'Lincoln',
        14,
        NULL,
        0877,
        '1990-07-24'
    ),
    (
        'PCM98509F',
        'Patricia',
        'McKenna',
        11,
        150,
        9999,
        '1989-08-01'
    ),
    (
        'R-M53550M',
        'Roland',
        'Mendel',
        11,
        150,
        0736,
        '1991-09-05'
    ),
    (
        'RBM23061F',
        'Rita',
        'Muller',
        5,
        198,
        1622,
        '1993-10-09'
    ),
    (
        'HAN90777M',
        'Helvetius',
        'Nagy',
        7,
        120,
        9999,
        '1993-03-19'
    ),
    (
        'TPO55093M',
        'Timothy',
        'O''Rourke',
        13,
        100,
        0736,
        '1988-06-19'
    ),
    (
        'SKO22412M',
        'Sven',
        'Ottlieb',
        5,
        150,
        1389,
        '1991-04-05'
    ),
    (
        'MAP77183M',
        'Miguel',
        'Paolino',
        11,
        112,
        1389,
        '1992-12-07'
    ),
    (
        'PSP68661F',
        'Paula',
        'Parente',
        8,
        125,
        1389,
        '1994-01-19'
    ),
    (
        'M-P91209M',
        'Manuel',
        'Pereira',
        8,
        101,
        9999,
        '1989-01-09'
    ),
    (
        'M-R38834F',
        'Martine',
        'Rance',
        9,
        75,
        0877,
        '1992-02-05'
    ),
    (
        'DWR65030M',
        'Diego',
        'Roel',
        6,
        192,
        1389,
        '1991-12-16'
    ),
    (
        'A-R89858F',
        'Annette',
        'Roulet',
        6,
        152,
        9999,
        '1990-02-21'
    ),
    (
        'MMS49649F',
        'Mary',
        'Saveley',
        8,
        175,
        0736,
        '1993-06-29'
    ),
    (
        'CGS88322F',
        'Carine',
        'Schmitt',
        13,
        64,
        1389,
        '1992-07-07'
    ),
    (
        'MAS70474F',
        'Margaret',
        'Smith',
        9,
        78,
        1389,
        '1988-09-29'
    ),
    (
        'HAS54740M',
        'Howard',
        'Snyder',
        12,
        100,
        0736,
        '1988-11-19'
    ),
    (
        'MFS52347M',
        'Martin',
        'Sommer',
        10,
        165,
        0736,
        '1990-04-13'
    ),
    (
        'GHT50241M',
        'Gary',
        'Thomas',
        9,
        170,
        0736,
        '1988-08-09'
    ),
    (
        'DBT39435M',
        'Daniel',
        'Tonini',
        11,
        75,
        0877,
        '1990-01-01'
    );