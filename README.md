# sprout assignment project
How to run the application:
1. First load SproutDb.bak
2. Create a copy of the database for the test environment before running xunit.
3. Change directory to "Sprout.Exam.WebApp\ClientApp" then run "npm install".

Answer to interview question:

"If we are going to deploy this on production, what do you think is the next
improvement that you will prioritize next? This can be a feature, a tech debt, or
an architectural design."

1. I will first separate the web api code base and front end source code.
2. I will implement a logging feature.
3. I will implement an audit trail feature.
4. The critical/high severity findings error on "npm audit" must be addressed
5. I will normalize the Employee Table by seperating the full name into three seperate fields (ex. FirstName, MiddleName, LastName)
6. Create a yaml file for docker in preparation for CI/CD.
