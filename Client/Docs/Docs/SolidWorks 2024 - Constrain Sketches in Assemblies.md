## **SolidWorks 2024 - Constrain Sketches in Assemblies**

AutoCAD LT and DriveSight don't generate an automatic bill of material (BOM) so we need to eliminate  them from our process. Let’s focus on how to **constrain sketches** in **SolidWorks assemblies**, particularly when your parts are defined as **2D sketches** or simple profiles. This process involves applying **mates** to establish relationships between the sketches in your assembly to ensure proper alignment, positioning, behavior and will generate an associative bill of material automatically! 

### **Constrain Sketches in Assemblies with SolidWorks**

### **1. Understanding Assembly Constraints (Mates)**

In **SolidWorks**, assembly constraints (referred to as **mates**) control how components fit together. When you are using sketches for parts, it’s crucial to apply the appropriate mates to ensure that the assembly behaves correctly. Here are the key aspects of mates:

- **Mates** define the relationships between components, controlling how they move relative to each other.
- Each mate can restrict **translation** or **rotation** or allow for certain degrees of freedom.

### **2. Types of Mates**

Here are the primary types of mates you'll use when working with 2D sketches in assemblies:

1. **Coincident Mate**:
   - Forces two entities (like points or edges) to be at the same location.
   - Useful for aligning sketch points to sketch points or points to edges.

2. **Parallel Mate**:
   - Makes two faces, edges, or lines parallel to each other.
   - Helpful when aligning components that need to be parallel.

3. **Perpendicular Mate**:
   - Ensures that two lines, faces, or edges are at right angles (90 degrees) to each other.

4. **Distance Mate**:
   - Sets a specific distance between two components.
   - Useful for maintaining space between parts (e.g., the distance between two belt rollers).

5. **Angle Mate**:
   - Defines the angle between two faces, edges, or lines.

6. **Tangent Mate**:
   - Ensures that a circular edge (e.g., a belt sprocket) is tangent to a flat edge (e.g., a belt).

7. **Lock Mate**:
   - Prevents any relative movement between two components, effectively fixing their position.

### **3. Steps to Constrain Sketches in Assemblies**

Let’s walk through the process of creating an assembly using parts defined as 2D sketches, such as a modular belt with rollers.

#### **Step 1: Create 2D Sketches for Each Part**

1. **Sketch the Parts**:
   - Create individual 2D sketches for all components (e.g., belt profile, rollers, sprockets).
   - Save each sketch as a part file. You can create 2D sketches directly as parts by going to **File > New > Part**, and then use the sketching tools.

#### **Step 2: Start an Assembly**

1. **Create a New Assembly**:
   - Go to **File > New > Assembly**.
   - Insert your first part (e.g., the belt profile). This will serve as your base.

#### **Step 3: Insert Additional Components**

1. **Insert Additional Parts**:
   - Use **Insert Components** to add rollers, sprockets, or any other parts into the assembly.
   - Position the parts roughly where you think they should go. Don’t worry about exact positioning at this stage.

#### **Step 4: Apply Mates to Constrain the Assembly**

1. **Select Mates**:
   - Click on the **Mate** feature in the Assembly tab to open the **Mate PropertyManager**.

2. **Using Coincident Mate**:
   - Select a point on the belt sketch and a corresponding point on the roller sketch.
   - This will ensure that the roller is correctly positioned on the belt.

3. **Using Tangent Mate** (if applicable):
   - If your roller has a circular profile, select the edge of the roller and the corresponding surface of the belt.
   - This makes the roller tangent to the belt, ensuring smooth movement.

4. **Using Parallel Mate**:
   - If the rollers need to be aligned parallel to the belt, select the two faces or edges and apply the **Parallel Mate**.

5. **Using Distance Mate**:
   - Set the distance between the roller and the belt to control how far apart they are (e.g., the distance can be based on design requirements).

6. **Repeat for Other Components**:
   - Continue inserting and mating the remaining parts (e.g., sprockets and additional rollers).
   - Always ensure you use appropriate mates (coincident, tangent, etc.) to accurately position each component.

#### **Step 5: Review and Adjust Mates**

1. **Check for Conflicts**:
   - After applying mates, check if the assembly is fully constrained (indicated by a green checkmark in the **FeatureManager**).
   - If it’s under-defined (blue), you might need to add more mates.
   - If it’s over-defined (red), you may need to delete or modify some mates.

2. **Edit Mate Properties**:
   - If necessary, right-click on any mate in the **FeatureManager** and choose **Edit Feature** to adjust the parameters.

#### **Step 6: Finalize the Assembly**

1. **Move Components**:
   - Test the movement of your assembly by dragging components. They should move as expected based on the mates applied.
   - Ensure everything behaves correctly, especially for components that will interact during operation (like the belt and sprockets).

2. **Save Your Assembly**:
   - Save your assembly file, which will now include all parts and their defined relationships.

---

### **4. Deliverables from the Assembly Process**

After following the steps above, the final deliverables from your assembly using 2D sketches would include:

- **Assembled Model**: A 3D model of your modular belt system, correctly assembled with all parts positioned according to your design.
- **Bill of Materials (BOM)**: If you wish to create a BOM, you can insert one into your assembly drawing, which will pull the part names and quantities from the assembly.
- **2D Drawing Views**: Use the **Make Drawing from Assembly** function to create detailed 2D drawings with side, top, and front views.
- **Annotations and Dimensions**: Add dimensions and annotations to the drawings to detail the assembly for manufacturing or documentation purposes.

### **Conclusion**

Constraining sketches in SolidWorks assemblies involves applying appropriate mates to define how parts fit and move together. By leveraging mates effectively, you can ensure a robust assembly that performs accurately and efficiently. This process not only enhances the quality of your designs but also streamlines your workflow, enabling you to deliver top-quality assemblies and documentation quickly and efficiently.