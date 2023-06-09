WITH EmployeeHierarchy AS (
  SELECT ID, NAME, CHIEF_ID, 1 AS HIERARCHY_LEVEL
  FROM EMPLOYEE
  WHERE CHIEF_ID IS NULL
  UNION ALL
  SELECT e.ID, e.NAME, e.CHIEF_ID, eh.HIERARCHY_LEVEL + 1
  FROM EMPLOYEE e
  INNER JOIN EmployeeHierarchy eh ON e.CHIEF_ID = eh.ID
)
SELECT MAX(HIERARCHY_LEVEL - 1) AS MAX_HIERARCHY_LEVEL
FROM EmployeeHierarchy;