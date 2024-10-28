using System;
using System.Collections.Generic;

public class BeltAssemblyService
{
    public double Width { get; set; }
    public double Length { get; set; }
    public double PartWidth { get; set; }
    public double PartLength { get; set; }
    public int InterlockRequirement { get; set; } = 1;

    // Grid to represent the coordinate system for part placement
    private List<Part> parts;
    private double[,] grid;

    public BeltAssemblyService(double width, double length, double partWidth, double partLength, int interlockRequirement = 1)
    {
        Width = width;
        Length = length;
        PartWidth = partWidth;
        PartLength = partLength;
        InterlockRequirement = interlockRequirement;

        parts = new List<Part>();
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        // Calculate grid dimensions based on part size
        int rows = (int)(Length / PartLength);
        int cols = (int)(Width / PartWidth);
        grid = new double[rows, cols];
    }

    public void AddPart(double x, double y, bool isEdgePart = false)
    {
        if (IsInsideBoundary(x, y) && IsInterlocked(x, y))
        {
            Part newPart = new Part(x, y, PartWidth, PartLength, isEdgePart);
            parts.Add(newPart);
            MarkGrid(newPart);
        }
    }

    private bool IsInsideBoundary(double x, double y)
    {
        // Check if the part is within the boundaries of the belt assembly
        return x >= 0 && x + PartWidth <= Width && y >= 0 && y + PartLength <= Length;
    }

    private bool IsInterlocked(double x, double y)
    {
        // Check if part has at least one neighboring part in the grid to ensure interlocking
        int row = (int)(y / PartLength);
        int col = (int)(x / PartWidth);
        int interlockCount = 0;

        // Check adjacent cells in the grid
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int newRow = row + i;
                int newCol = col + j;
                if (newRow >= 0 && newRow < grid.GetLength(0) &&
                    newCol >= 0 && newCol < grid.GetLength(1) &&
                    !(i == 0 && j == 0) && grid[newRow, newCol] == 1)
                {
                    interlockCount++;
                }
            }
        }

        return interlockCount >= InterlockRequirement;
    }

    private void MarkGrid(Part part)
    {
        // Mark the part's position in the grid as occupied
        int row = (int)(part.Y / PartLength);
        int col = (int)(part.X / PartWidth);
        grid[row, col] = 1;
    }

    public void AutoFill()
    {
        // Automatically fill the assembly area with parts while checking interlocking and boundary
        for (double y = 0; y < Length; y += PartLength)
        {
            for (double x = 0; x < Width; x += PartWidth)
            {
                if (IsInsideBoundary(x, y) && IsInterlocked(x, y))
                {
                    AddPart(x, y);
                }
            }
        }
    }

    public void CutToFit()
    {
        // Identify parts that exceed the boundaries and adjust dimensions as necessary
        foreach (var part in parts)
        {
            if (part.X + PartWidth > Width)
            {
                part.Width = Width - part.X; // Cut width to fit boundary
            }
            if (part.Y + PartLength > Length)
            {
                part.Length = Length - part.Y; // Cut length to fit boundary
            }
        }
    }
}

// Part class to represent individual parts within the assembly
public class Part
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
    public bool IsEdgePart { get; set; }

    public Part(double x, double y, double width, double length, bool isEdgePart = false)
    {
        X = x;
        Y = y;
        Width = width;
        Length = length;
        IsEdgePart = isEdgePart;
    }
}
