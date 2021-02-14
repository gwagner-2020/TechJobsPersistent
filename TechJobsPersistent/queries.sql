--Part 1
--EmployerId int foreign key from Employer class
--Id int auto-increment
--Name longtext

--Part 2

--SELECT Name
--	FROM techjobs.employers
--	WHERE (Location = "St. Louis");

--Part 3
--SELECT techjobs.skills.Name, techjobs.skills.Description
	--FROM techjobs.skills
    --INNER JOIN techjobs.jobskills ON techjobs.skills.Id = techjobs.jobskills.SkillId
    --WHERE techjobs.jobskills.jobId IS NOT NULL
	--ORDER BY techjobs.skills.Name ASC;


