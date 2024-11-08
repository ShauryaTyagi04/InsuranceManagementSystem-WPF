USE InsuranceManagementSystem;

--Customers
CREATE TABLE Customers (
    CustomerID INT IDENTITY(100, 1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    DOB DATE NOT NULL,
    ContactNumber NVARCHAR(15) NOT NULL,
    AlternateContactNumber NVARCHAR(15),
    EmailAddress NVARCHAR(100) UNIQUE,
    Address NVARCHAR(255),
    PostCode NVARCHAR(10),
    Sex CHAR(1) CHECK (Sex IN ('M', 'F', 'O')) NOT NULL,
    MaritalStatus NVARCHAR(10) CHECK (MaritalStatus IN ('married', 'single', 'divorced', 'engaged')) NOT NULL,
    Nationality NVARCHAR(50),
    Occupation NVARCHAR(100),
    NationalInsurance NVARCHAR(20) UNIQUE NOT NULL,
    IsApproved BIT DEFAULT 0 NOT NULL
);

--Employees
CREATE TABLE Staff (
    EmployeeID INT IDENTITY(100, 1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    UserName NVARCHAR(50) UNIQUE NOT NULL,
    EmailAddress NVARCHAR(100) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(10) CHECK (Role IN ('staff', 'manager')) NOT NULL,
    isApproved BIT DEFAULT 0 NOT NULL 
);

--Policy
CREATE TABLE Policies (
    PolicyID INT IDENTITY(100, 1) PRIMARY KEY,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    CarInsurance BIT DEFAULT 0 NOT NULL,
    HomeInsurance BIT DEFAULT 0 NOT NULL,
    LifeInsurance BIT DEFAULT 0 NOT NULL,
    TravelInsurance BIT DEFAULT 0 NOT NULL,
    PremiumAmount FLOAT NOT NULL,
	IsApproved BIT DEFAULT 0 NOT NULL,
    CustomerID INT UNIQUE,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

--Travel Insurance
	CREATE TABLE TravelInsurance (
    TInsuranceID INT PRIMARY KEY IDENTITY(1,1),         
    Destination VARCHAR(255) NOT NULL,        
    TravelDate DATE NOT NULL,                   
    ReturnDate DATE NOT NULL,                     
    InternationalContactNumber VARCHAR(20),        
    CoverageAmount FLOAT NOT NULL,                                            

    LostBags BIT NOT NULL DEFAULT 0,                  
    LegalCosts BIT NOT NULL DEFAULT 0,                 
    EmergencyMedicalExpenses BIT NOT NULL DEFAULT 0,   
    CancellationCost BIT NOT NULL DEFAULT 0, 
	
	Single BIT NOT NULL DEFAULT 0,
	Solo BIT NOT NULL DEFAULT 0,
	European BIT NOT NULL DEFAULT 0,
	Annual BIT NOT NULL DEFAULT 0,
	Family BIT NOT NULL DEFAULT 0,
	Summer BIT NOT NULL DEFAULT 0,
	Gap BIT NOT NULL DEFAULT 0,
	Worldwide BIT NOT NULL DEFAULT 0,
	Winter BIT NOT NULL DEFAULT 0,

	IsApproverd BIT NOT NULL DEFAULT 0,

	PolicyID INT NOT NULL, 
	FOREIGN KEY (PolicyID) REFERENCES Policies(PolicyID)
);

--Life Insurance
CREATE TABLE LifeInsurance (
    LInsuranceID INT PRIMARY KEY IDENTITY(100,1),         
    Height_cm VARCHAR(10) NOT NULL,        
    Weight_kg VARCHAR(10) NOT NULL,                                        
    NHSNumber VARCHAR(20) NOT NULL,    
	Health_Issues VARCHAR(255) ,
	Income VARCHAR (20) NOT NULL,
	Benefitiary VARCHAR (100) NOT NULL,
	Relationship_B VARCHAR (100) NOT NULL,
    CoverageAmount FLOAT NOT NULL,                                            

    Year_10 BIT NOT NULL DEFAULT 0,                  
    Year_20 BIT NOT NULL DEFAULT 0,                 
    Year_30 BIT NOT NULL DEFAULT 0, 
    Univesal BIT NOT NULL DEFAULT 0,
	Whole BIT NOT NULL DEFAULT 0,
	Variable BIT NOT NULL DEFAULT 0,
	
	Family BIT NOT NULL DEFAULT 0,
	Debt BIT NOT NULL DEFAULT 0,
	Estate BIT NOT NULL DEFAULT 0,
	Business BIT NOT NULL DEFAULT 0,
	Gaurantee BIT NOT NULL DEFAULT 0,
	
	Unemployed BIT NOT NULL DEFAULT 0,
	Self_E BIT NOT NULL DEFAULT 0,
	Employee BIT NOT NULL DEFAULT 0,

	IsApproverd BIT NOT NULL DEFAULT 0,

	PolicyID INT NOT NULL, 
	FOREIGN KEY (PolicyID) REFERENCES Policies(PolicyID)
);

--Car Insurance
CREATE TABLE CarInsurance (
	CInsuranceID INT PRIMARY KEY IDENTITY(100,1),             
	Make VARCHAR(10) NOT NULL,
	Model VARCHAR (50) NOT NULL,
	Colour VARCHAR(50) NOT NULL,
	Registration VARCHAR(100) NOT NULL,
	License_Plate VARCHAR(10) NOT NULL,
	Council VARCHAR(50) NOT NULL,
	License_Driver VARCHAR(20) NOT NULL,
    CoverageAmount FLOAT NOT NULL,                                            

	IsApproverd BIT NOT NULL DEFAULT 0,

	PolicyID INT NOT NULL, 
	FOREIGN KEY (PolicyID) REFERENCES Policies(PolicyID)
);

--Home Insurance
CREATE TABLE HomeInsurance (
	HInsuranceID INT PRIMARY KEY IDENTITY(100,1),             
	Property_Address VARCHAR(255) NOT NULL,
	Property_Value VARCHAR (50) NOT NULL,
	Lease DATE NOT NULL,
	Legal_Owner VARCHAR(100) NOT NULL,
	Owner_Contact VARCHAR(20) NOT NULL,
	Owner_Email VARCHAR(50) NOT NULL,
    CoverageAmount FLOAT NOT NULL,                                            

	IsApproverd BIT NOT NULL DEFAULT 0,

	PolicyID INT NOT NULL, 
	FOREIGN KEY (PolicyID) REFERENCES Policies(PolicyID)
);


SELECT * from TravelInsurance;
SELECT * from Policies;
DROP TABLE HomeInsurance;
