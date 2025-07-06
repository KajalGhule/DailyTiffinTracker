
-- 1. Create database
CREATE DATABASE dailyTiffinTrackerDB;
USE dailyTiffinTrackerDB;

-- 2. Users for Login & Roles
CREATE TABLE `User` (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Role ENUM('Admin', 'Student', 'TiffinProvider') NOT NULL,
    IsActive BOOLEAN DEFAULT TRUE
);

-- 3. Student linked to User (only if user is a student)
CREATE TABLE Student (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    UserId INT UNIQUE,
    IsActive BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (UserId) REFERENCES `User`(Id) ON DELETE SET NULL
);

-- 4. Daily Meal Distribution
CREATE TABLE MealDistribution (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    StudentId INT NOT NULL,
    DistributionDate DATE NOT NULL,
    MealType ENUM('Breakfast', 'Tiffin') NOT NULL,
    Received BOOLEAN DEFAULT TRUE,
    Remarks VARCHAR(255),
    CreatedBy INT, -- TiffinProvider who added this
    FOREIGN KEY (StudentId) REFERENCES Student(Id),
    FOREIGN KEY (CreatedBy) REFERENCES `User`(Id)
);


ALTER TABLE MealDistribution ALTER COLUMN Received DROP DEFAULT;

ALTER TABLE MealDistribution 
MODIFY MealType ENUM('Breakfast', 'AfternoonTiffin', 'EveningTiffin') NOT NULL;



SET SQL_SAFE_UPDATES = 0;

UPDATE MealDistribution 
SET MealType = 'AfternoonTiffin' 
WHERE MealType = 'Tiffin';

-- optional: re-enable safe updates if needed later
SET SQL_SAFE_UPDATES = 1;

-- 5. Meal Action Log (for Admin)
CREATE TABLE MealAuditLog (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    StudentId INT NOT NULL,
    Action VARCHAR(50),
    PerformedBy INT,
    ActionTime DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (StudentId) REFERENCES Student(Id),
    FOREIGN KEY (PerformedBy) REFERENCES `User`(Id)
);


CREATE TABLE MealDistributionBackup AS SELECT * FROM MealDistribution;

DROP TABLE MealDistribution;

CREATE TABLE MealDistribution (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    StudentId INT NOT NULL,
    DistributionDate DATE NOT NULL,
    MealType ENUM('Breakfast', 'AfternoonTiffin', 'EveningTiffin') NOT NULL,
    Received BOOLEAN,
    Remarks VARCHAR(255),
    CreatedBy INT,
    FOREIGN KEY (StudentId) REFERENCES Student(Id),
    FOREIGN KEY (CreatedBy) REFERENCES `User`(Id)
);


INSERT INTO MealDistribution (StudentId, DistributionDate, MealType, Received, Remarks, CreatedBy)
SELECT StudentId, DistributionDate, 
    CASE 
        WHEN MealType = 'Tiffin' THEN 'AfternoonTiffin' 
        ELSE MealType 
    END,
    Received, Remarks, CreatedBy
FROM MealDistributionBackup;

CREATE TABLE ProviderStudent (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    ProviderId INT NOT NULL, -- UserId of the provider
    StudentId INT NOT NULL,  -- Each student has one provider
    FOREIGN KEY (ProviderId) REFERENCES `User`(Id),
    FOREIGN KEY (StudentId) REFERENCES Student(Id),
    UNIQUE (StudentId)
);
