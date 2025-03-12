PRAGMA foreign_keys = ON;  -- Enable foreign key constraints

-- Drop tables if they exist (SQLite style)
DROP TABLE IF EXISTS Drawings;
DROP TABLE IF EXISTS BeltTypes;
DROP TABLE IF EXISTS BeltSeries;
DROP TABLE IF EXISTS MaterialColors;
DROP TABLE IF EXISTS Materials;
DROP TABLE IF EXISTS AdderMaterials; -- New lookup table
DROP TABLE IF EXISTS RodMaterials; -- New lookup table
DROP TABLE IF EXISTS BeltWidths; -- New table for values
DROP TABLE IF EXISTS FlightsRollersGrips;
DROP TABLE IF EXISTS BeltAccessories;
DROP TABLE IF EXISTS FrictionAntiStatics;
DROP TABLE IF EXISTS SidePLLaneDVCodes;
DROP TABLE IF EXISTS IndentCodes;
DROP TABLE IF EXISTS JobNumbers;
DROP TABLE IF EXISTS Users;  -- Assuming you have a Users table
DROP TABLE IF EXISTS Customers; -- Assuming you have a Customers table
DROP TABLE IF EXISTS ProcessSteps;
DROP TABLE IF EXISTS BeltParts;

-- Lookup Tables (Codes)
CREATE TABLE BeltTypes (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    BeltTypeCode TEXT UNIQUE NOT NULL,
    Value TEXT NOT NULL
);

CREATE TABLE BeltSeries (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    BeltSeriesCode TEXT UNIQUE NOT NULL,
    Value TEXT NOT NULL
);

CREATE TABLE MaterialColors (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    ColorCode TEXT UNIQUE NOT NULL,
    Value TEXT NOT NULL
);

CREATE TABLE Materials (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    MaterialCode TEXT UNIQUE NOT NULL,
    Value TEXT NOT NULL
);

CREATE TABLE AdderMaterials ( -- New lookup table
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    AdderMaterialCode TEXT UNIQUE NOT NULL,
    Value TEXT NOT NULL
);

CREATE TABLE RodMaterials ( -- New lookup table
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    RodMaterialCode TEXT UNIQUE NOT NULL,
    Value TEXT NOT NULL
);

CREATE TABLE FlightsRollersGrips (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    FlightsRollersGripsCode TEXT UNIQUE NOT NULL,
    Value TEXT NOT NULL
);

CREATE TABLE BeltAccessories (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    BeltAccessoriesCode TEXT UNIQUE NOT NULL,
    Value TEXT NOT NULL
);

CREATE TABLE FrictionAntiStatics (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    FrictionAntiStaticCode TEXT UNIQUE NOT NULL,
    Value TEXT NOT NULL
);

CREATE TABLE SidePLLaneDVCodes (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    SidePLLaneDVCode TEXT UNIQUE NOT NULL,
    Value TEXT NOT NULL
);

CREATE TABLE IndentCodes (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    IndentCode TEXT UNIQUE NOT NULL,
    Value TEXT NOT NULL
);

-- Tables for Values (Not Codes)
CREATE TABLE BeltWidths ( -- New table for values
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    BeltWidth REAL UNIQUE NOT NULL -- Store the width value here
);

-- Job Numbers Table
CREATE TABLE JobNumbers (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    JobNumber TEXT UNIQUE NOT NULL
);

-- Users Table (Assumed - Adapt as needed)
CREATE TABLE Users (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Username TEXT UNIQUE NOT NULL,
    Description TEXT NOT NULL
    -- Add other user-related columns
);

-- Customers Table (Assumed - Adapt as needed)
CREATE TABLE Customers (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    CustomerName TEXT NOT NULL
    -- Add other customer-related columns
);

-- Process Steps Table
CREATE TABLE ProcessSteps (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    ProcessStep TEXT UNIQUE NOT NULL
);

-- Belt Parts Table
CREATE TABLE BeltParts (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Part TEXT NOT NULL,
	Description TEXT NOT NULL
    -- Add other part-related columns as needed
);

-- Main Belt Data Table
CREATE TABLE Drawings (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    BeltTypeCode INTEGER NOT NULL REFERENCES BeltTypes(ID),
    BeltSeriesCode INTEGER NOT NULL REFERENCES BeltSeries(ID),
    ColorCode INTEGER NOT NULL REFERENCES MaterialColors(ID),
    MaterialCode INTEGER NOT NULL REFERENCES Materials(ID),
    AdderMaterialCode INTEGER NOT NULL REFERENCES AdderMaterials(ID),  -- Corrected foreign key
    RodMaterialCode INTEGER NOT NULL REFERENCES RodMaterials(ID),  -- Corrected foreign key
    BeltWidth REAL NOT NULL REFERENCES BeltWidths(ID), -- Corrected: value, not code
    FlightsRollersGripsCode INTEGER NOT NULL REFERENCES FlightsRollersGrips(ID),
    QtyRollersAcrossWidth REAL CHECK (QtyRollersAcrossWidth <= 256),
    FRGCenters REAL CHECK (FRGCenters <= 256),
    BeltAccessoriesCode INTEGER NOT NULL REFERENCES BeltAccessories(ID),
    FrictionAntiStaticCode INTEGER NOT NULL REFERENCES FrictionAntiStatics(ID),
    SidePLLaneDVCode INTEGER NOT NULL REFERENCES SidePLLaneDVCodes(ID),
    UniqueIdentification TEXT,
    IndentCode INTEGER NOT NULL REFERENCES IndentCodes(ID),
    JobNumber INTEGER NOT NULL REFERENCES JobNumbers(ID),
    ThumbnailImage BLOB,  -- Or TEXT for file path
    CreatedDate TEXT DEFAULT (DATETIME('now')),  -- SQLite timestamp
    SugarCRMLink TEXT,
    CADTemplateFilepath TEXT,
    DWGFilepath TEXT,
    PDFFilepath TEXT,
    Revision TEXT,
    TitleBlockLine1 TEXT,
    TitleBlockLine2 TEXT,
    TitleBlockLine3 TEXT,
    Drafter INTEGER NOT NULL REFERENCES Users(ID),  -- Corrected foreign key
    PartsList INTEGER NOT NULL REFERENCES BeltParts(ID),
    CurrentProcessStep INTEGER NOT NULL REFERENCES ProcessSteps(ID),
    ApprovedCustomerName INTEGER NOT NULL REFERENCES Customers(ID),
    ServerFolder TEXT,
    ActualWidthNominal REAL,
    NumberOfLinks REAL,
    EdgeCut INTEGER,  -- 0 or 1 for boolean
    ModularVsMachinedIndent TEXT,
    MinGapVsStandardGap TEXT,
    FTPVsFTM TEXT,
    ComplexityLevel INTEGER
);