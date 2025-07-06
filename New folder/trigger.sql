use dailyTiffinTrackerDB;


DELIMITER $$

CREATE TRIGGER trg_log_meal_insert
AFTER INSERT ON MealDistribution
FOR EACH ROW
BEGIN
    INSERT INTO MealAuditLog (StudentId, Action)
    VALUES (NEW.StudentId, 'INSERT');
END$$

DELIMITER ;
