use dailyTiffinTrackerDB;
-- Insert Users (assume simple password for testing; you should hash it in real app)
INSERT INTO `User` (Username, PasswordHash, Role, IsActive) VALUES
('sanika.bhor', '123456', 'Student', TRUE),
('sumit.bhor', '123456', 'Student', TRUE),
('sarthak.kadam', '123456', 'Student', TRUE),
('pankaj.bhor', '123456', 'Student', TRUE),
('provider1', '123456', 'TiffinProvider', TRUE);

INSERT INTO Student (Name, UserId, IsActive) VALUES
('Sanika Bhor', 1, TRUE),
('Sumit Bhor', 2, TRUE),
('Sarthak Kadam', 3, TRUE),
('Pankaj Bhor', 4, TRUE);


-- Example: Sanika and Sumit got Tiffin today
INSERT INTO MealDistribution (StudentId, DistributionDate, MealType, Received, Remarks, CreatedBy)
VALUES
(1, CURDATE(), 'Tiffin', TRUE, 'Received on time', 5),
(2, CURDATE(), 'Tiffin', TRUE, 'Late arrival', 5);


-- Admin reviews actions
INSERT INTO `User` (Username, PasswordHash, Role, IsActive) VALUES
('admin', 'admin123', 'Admin', TRUE);

-- Insert log
INSERT INTO MealAuditLog (StudentId, Action, PerformedBy)
VALUES
(1, 'Added Meal', 6),
(2, 'Added Meal', 6);
