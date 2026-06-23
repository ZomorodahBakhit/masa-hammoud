USE University;
GO

CREATE TABLE Users (
UserId INT PRIMARY KEY,
UserName VARCHAR(64) NOT NULL,
FirstName VARCHAR(64) NOT NULL,
LastName VARCHAR(64) NOT NULL,
EmailAddress VARCHAR(128) NOT NULL UNIQUE,
PhoneNumber VARCHAR(16) NOT NULL,
Role VARCHAR(32) NOT NULL
);

CREATE TABLE Syllabus (
SyllabusId INT PRIMARY KEY,
Description TEXT NULL,
);

CREATE TABLE Courses(
CourseId INT PRIMARY KEY, 
CourseName VARCHAR(100) NOT NULL,
TeacherId INT NULL,
StartDate DateTime NOT NULL,
EndDate DateTime NOT NULL,
SyllabusId INT NULL,

FOREIGN KEY (TeacherId) REFERENCES Users(UserId),
FOREIGN KEY (SyllabusId) REFERENCES Syllabus(SyllabusId),
);

CREATE TABLE Assignments (
AssignmentId INT PRIMARY KEY,
CourseId INT NOT NULL,
AssignmentTitle VARCHAR(128) NOT NULL,
Description TEXT NULL,
Weight float NOT NULL,
MaxGrade INT NOT NULL,
DueDate DATE NOT NULL,

FOREIGN KEY (CourseId) REFERENCES Courses(CourseId),
);

CREATE TABLE Comments(
CommentId INT PRIMARY KEY,
AssignmentId INT NOT NULL,
CreatedByUserId INT NOT NULL,
CreatedDate DATETIME NOT NULL, 
CommentContent TEXT NULL,

FOREIGN KEY (AssignmentId) REFERENCES Assignments(AssignmentId),
FOREIGN KEY (CreatedByUserId) REFERENCES Users(UserId),
);

CREATE TABLE Grades (
GradeId INT PRIMARY KEY,
AssignmentId INT NOT NULL,
StudentId INT NOT NULL,
Grade INT NULL,

FOREIGN KEY (AssignmentId) REFERENCES Assignments(AssignmentId),
FOREIGN KEY (StudentId) REFERENCES Users(UserId),
);

-- 1 Add students and teachers

INSERT INTO Users 
VALUES 
(1,'MASAHAMMOUD','MASA','HAMMOUD','MASAH@GMAIL.COM','0000000000','Student'),
(2,'SARAMOUHAMMAD','SARA','MOUHAMMAD','SARAH@GMAIL.COM','1111111111','Student'),
(3,'SALMAALMASRI','SALMA','ALMASRI','SALMA@GMAIL.COM','2222222222','Student'),
(4,'SAMIHIJAZI','SAMI','HIJAZI','SAMI@GMAIL.COM','3333333333','Teacher'),
(5,'FYRIAL','FYRIAL','FYRIAL','FYRIAL@GMAIL.COM','4444444444','Teacher');

--2 INSERT Syllabus for each Course.

INSERT INTO Syllabus 
VALUES 
(1,'SQL Course'),
(2,'C# Course'),
(3,'EF Course'),
(4,'WEB API Course'),
(5,'REACT Course');

--3  INSERT data for SQL, C#, Entity Framework, Web API and React Courses.

INSERT INTO Courses 
VALUES
(1,'SQL',4,'2026-06-21','2026-08-13',1),
(2,'C#',5,'2026-06-21','2026-08-13',2),
(3,'EF',4,'2026-06-21','2026-08-13',3),
(4,'WEB API',5,'2026-06-21','2026-08-13',4),
(5,'REACT',4,'2026-06-21','2026-08-13',5);

/*INSERT INTO Assignments
VALUES 
(1,1,'SQL Assignment ','JOINS',20,100,'2026-07-13'),
(2,2,'C# Assignment ','VARIABLES',30,90,'2026-07-13'),
(3,3,'EF Assignment ','OBJECT-RELATION MAPPER ',40,100,'2026-07-13'),
(4,4,'WEB API Assignment ','API',50,90,'2026-07-13'),
(5,5,'REACT Assignment ','WEP APPLICATION',60,100,'2026-07-13');*/

-- 4 INSERT at least 5 Assignments for each Course

DECLARE @CourseId INT = 1;
DECLARE @AssignmentN INT;
DECLARE @AssignmentId INT = 1;

WHILE @CourseId <= 5
BEGIN

    SET @AssignmentN = 1;
    WHILE @AssignmentN <= 5
    BEGIN
        INSERT INTO Assignments
        (
            AssignmentId,
            CourseId,
            AssignmentTitle,
            Description,
            Weight,
            MaxGrade,
            DueDate
        )
        VALUES
        (
            @AssignmentId,
            @CourseId,

            CASE
                WHEN @CourseId = 1 THEN 'SQL Assignment ' + CAST(@AssignmentN AS VARCHAR(10))
                WHEN @CourseId = 2 THEN 'C# Assignment ' + CAST(@AssignmentN AS VARCHAR(10))
                WHEN @CourseId = 3 THEN 'EF Assignment ' + CAST(@AssignmentN AS VARCHAR(10))
                WHEN @CourseId = 4 THEN 'WEB API Assignment ' + CAST(@AssignmentN AS VARCHAR(10))
                WHEN @CourseId = 5 THEN 'REACT Assignment ' + CAST(@AssignmentN AS VARCHAR(10))
            END,

            CASE
                WHEN @CourseId = 1 THEN 'JOINS'
                WHEN @CourseId = 2 THEN 'VARIABLES'
                WHEN @CourseId = 3 THEN 'OBJECT-RELATION MAPPER'
                WHEN @CourseId = 4 THEN 'API'
                WHEN @CourseId = 5 THEN 'WEB APPLICATION'
            END,

            20,
            100,

            DATEADD
            (
                DAY,
                CAST(RAND(CHECKSUM(NEWID())) * 60 - 30 AS INT),
                GETDATE()
            )
        );

        SET @AssignmentId = @AssignmentId + 1;
        SET @AssignmentN = @AssignmentN + 1;
    END

    SET @CourseId = @CourseId + 1;
END

/*INSERT INTO Comments
VALUES
(1,1,1,GETDATE(),'GOOD'),
(2,2,2,GETDATE(),'GOOD'),
(3,3,3,GETDATE(),'GOOD');*/

--5 INSERT at least 10 comments 

DECLARE @CommentId INT = 1;

WHILE @CommentId <= 10
BEGIN

    INSERT INTO Comments
    (
        CommentId,
        AssignmentId,
        CreatedByUserId,
        CreatedDate,
        CommentContent
    )
    VALUES
    (
        @CommentId,
        CAST(RAND(CHECKSUM(NEWID())) * 25 + 1 AS INT),
        CAST(RAND(CHECKSUM(NEWID())) * 3 + 1 AS INT),
        GETDATE(),

        CASE CAST(RAND(CHECKSUM(NEWID())) * 3 AS INT)
            WHEN 0 THEN 'GOOD'
            WHEN 1 THEN 'EASY'
            ELSE 'HARD'
        END
    );

    SET @CommentId = @CommentId + 1;
END

/*INSERT INTO Grades
VALUES
(1,1,1,90),
(2,2,2,100),
(3,3,3,80);*/

--6 INSERT random grades for all students 

DECLARE @StudentId INT = 1;
DECLARE @AssignmentId2 INT;
DECLARE @GradeId INT = 1;

WHILE @StudentId <= 3
BEGIN
    SET @AssignmentId2 = 1;

    WHILE @AssignmentId2 <= 25
    BEGIN

        INSERT INTO Grades
        (
            GradeId,
            AssignmentId,
            StudentId,
            Grade
        )
        VALUES
        (
            @GradeId,
            @AssignmentId2,
            @StudentId,
            CAST(RAND(CHECKSUM(NEWID())) * 101 AS INT)
        );

        SET @GradeId = @GradeId + 1;
        SET @AssignmentId2 = @AssignmentId2 + 1;
    END

    SET @StudentId = @StudentId + 1;
END

-- 7 SELECT query to list all courses
SELECT * FROM Courses

-- 8  SELECT query to find all assignments for a specific course.
SELECT * FROM Assignments WHERE CourseId = 2

--9 SELECT query to find all students (users with the role 'Student').
SELECT * FROM Users WHERE Role = 'Student'

--10 UPDATE statement to change a student's role.
UPDATE Users SET Role = 'Teacher' WHERE UserId = 3 

-- 11  DELETE statement to remove a specific comment.
DELETE FROM Comments WHERE CommentId = 2

-- 12  list all students along with their grades for a specific course.
SELECT U.UserName, A.AssignmentTitle, G.GRADE  FROM Grades G
JOIN Users U ON G.StudentId = U.UserId 
JOIN Assignments A ON G.AssignmentId = A.AssignmentId 
WHERE A.CourseId = 5

-- 13 calculate the average grade for each course.
SELECT C.CourseName , AVG(G.Grade) AS AvgGrade FROM Courses C
JOIN Assignments A ON C.CourseId = A.CourseId
JOIN Grades G ON A.AssignmentId = G.AssignmentId
GROUP BY C.CourseName;

-- 14 list all courses with their respective syllabuses.
SELECT C.CourseName, S.Description FROM Courses C
JOIN Syllabus S ON C.SyllabusId = S.SyllabusId;


-- 15 find all comments for a specific course.
SELECT C.CommentContent FROM Comments C
JOIN Assignments A ON C.AssignmentId = A.AssignmentId
WHERE A.CourseId = 4


GO
-- 16 add a new student.
CREATE PROCEDURE AddStudent (
 @UserId INT,
 @UserName VARCHAR(64),
 @FirstName VARCHAR(64),
 @LastName VARCHAR(64),
 @EmailAddress VARCHAR(128),
 @PhoneNumber VARCHAR(16)
)
AS BEGIN 

INSERT INTO Users 
VALUES 
(
 @UserId ,
 @UserName ,
 @FirstName ,
 @LastName ,
 @EmailAddress ,
 @PhoneNumber ,
 'Student'
);
END


EXEC AddStudent
6,
'MASSA MASSA ',
'MASSA',
'MASSA',
'MASSA@GMAIL.COM',
'9999999999';

GO 

-- 17  add a new Assignment.
CREATE PROCEDURE AddAssignment
(
    @AssignmentId INT,
    @CourseId INT,
    @AssignmentTitle VARCHAR(128),
    @Description TEXT,
    @Weight FLOAT,
    @MaxGrade INT,
    @DueDate DATE
)
AS BEGIN 

    DECLARE @TotalWeight FLOAT;
    SELECT @TotalWeight = SUM(Weight) FROM Assignments WHERE CourseId = @CourseId;
    IF (@TotalWeight + @Weight > 100)
    BEGIN
        PRINT 'The weight should not exceed 100';
        RETURN;
    END

    INSERT INTO Assignments
    VALUES
    (
        @AssignmentId,
        @CourseId,
        @AssignmentTitle,
        @Description,
        @Weight,
        @MaxGrade,
        @DueDate
    );

END

EXEC AddAssignment

    @AssignmentId = 1,
    @CourseId = 1,
    @AssignmentTitle = 'SQL Assignment 6',
    @Description = 'JOINS ',
    @Weight = 10,
    @MaxGrade = 100,
    @DueDate = '2026-08-13';

GO

-- 18 calculate the Student Grade in a Course
CREATE FUNCTION GetStudentGrade
(
    @Percentage FLOAT
)
RETURNS VARCHAR(1)
AS BEGIN

    DECLARE @Result VARCHAR(1);

    IF @Percentage >= 90
        SET @Result='A';
    ELSE IF @Percentage >= 80
        SET @Result='B';
    ELSE IF @Percentage >= 70
        SET @Result='C';
    ELSE IF @Percentage >= 60
        SET @Result='D';
    ELSE
        SET @Result='F';

    RETURN @Result;
END

GO

SELECT dbo.GetStudentGrade(95) AS Grade;

GO 
-- 19 calculate the GPA of a student.
CREATE FUNCTION GetStudentGPA
(
    @StudentId INT
)
RETURNS FLOAT
AS BEGIN

    DECLARE @GPA FLOAT;

    SELECT @GPA = SUM(G.Grade * A.Weight) * 1 /SUM(A.Weight)
    FROM Grades G INNER JOIN Assignments A ON G.AssignmentId = A.AssignmentId
    WHERE G.StudentId = @StudentId;

    RETURN @GPA;
END

  GO 

SELECT dbo.GetStudentGPA(5) AS Grade

