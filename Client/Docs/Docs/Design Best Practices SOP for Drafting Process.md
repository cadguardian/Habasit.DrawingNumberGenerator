## Design Best Practices SOP for Drafting Process

> **Implementation of Design Best Practices for Habasit America: Drafting Process, Document Control, and Standards**

As we move forward with implementing a comprehensive design process, these best practices will ensure that all 2D drawings and designs are not only aesthetically pleasing but also fully functional and compliant with industry standards. Below is a detailed **Standard Operating Procedure (SOP)** that defines the drafting process, document control, dimensioning standards, and tolerances. This will help ensure that every drawing created has the required engineering details, enabling seamless communication between design, manufacturing, and external stakeholders.

---

## **1. Drafting Process: From Initial Concept to Final Approval**

The drafting process will be structured into the following steps to maintain consistency, quality, and accountability. This applies to the creation of all modular plastic belt designs and related components.

### **1.1 Initial Concept and Design Brief**

- **Start with Requirements**: The design process begins with gathering the **design requirements** from internal teams (sales, engineering, or manufacturing) or external clients.
  - Document essential details such as **dimensions**, **load specifications**, **material types**, **tolerances**, and any special requests (e.g., heat resistance, food-grade material requirements).

- **Create Conceptual Sketches**: Develop **rough sketches** to visualize the design based on the requirements. This can be done in **AutoCAD LT** for 2D layouts or **SolidWorks** if you want to prepare for 3D features later. Focus on key components like:
  - **Modular belt layouts**.
  - **Sprockets, rods, and fasteners**.
  - **Connections to the conveyor frame**.
  
- **Preliminary Review**: Submit the conceptual sketches for **initial review** by team leads or engineering managers. The goal here is to confirm that the proposed design aligns with project specifications.

### **1.2. Detailed 2D Drawing Creation**

Once the concept is approved, move on to detailed 2D drafting. Follow these steps:

#### **A. Setting up the Drawing Template**
- Use **predefined templates** in **AutoCAD LT** or **SolidWorks** that are based on **ISO, ANSI, or DIN standards**.
- Ensure the template includes:
  - **Title block** with fields for **drawing numbers**, **dates**, **revision history**, and **part numbers**.
  - **Company logo** and **project-specific metadata** (e.g., material, scale, tolerance).
  
#### **B. Sketching the Design**
- **2D Drafting**: For each modular belt component or assembly, create the necessary views (top, front, side, and any sections).
  - Ensure **functional relationships** are captured—e.g., the positioning of belt rods relative to sprockets.
  - **Dimensioning**: Apply precise dimensions and relations to sketches.
  
- **Apply Blocks for Repetitive Geometry**: Reuse **blocks** or **import views** (if available) to simplify creating repetitive or standardized parts like sprockets or belt segments.

### **1.3. Review and Feedback**

- **Internal Peer Review**: After creating the detailed 2D drawing, submit it to another engineer or senior drafter for a **peer review**. The reviewer should verify:
  - Accuracy of **dimensions and tolerances**.
  - Compliance with **standards** (ISO/ANSI/DIN).
  - Correct use of materials, fits, and any special processes.

- **Engineering Approval**: Once the peer review is completed, the drawing moves to the **Engineering Manager or VP** for final approval.

---

## **2. Document Control and Revision Management**

Document control is essential for ensuring that only the most up-to-date and approved versions of designs are used in production and shared with clients or stakeholders. Here’s how document control will be implemented:

### **2.1. File Naming Convention**
- **Standardized Naming Convention**:
  - Use a consistent file naming structure across all projects to ensure clarity and prevent duplication:
    - Format: `PartNumber_ProjectName_RevisionNumber.dwg` or `.slddrw` (e.g., `M2520_PB_Sprocket_RevA.dwg`).

- **Version Control**:
  - Use **SolidWorks PDM** and **Autodesk Vault** to manage file versions and prevent overwriting of designs.
  - Each new revision should be labeled (e.g., **Rev A, Rev B**) and documented with a **revision history** that includes:
    - **Change notes** explaining what was modified.
    - **Approval status** (who approved the changes).

### **2.2. Document Control Workflow**

1. **Draft Creation**:
   - The drafter creates a preliminary drawing and saves it in the PDM vault with an initial version (e.g., **V1.0 – Under Review**).

2. **Review Process**:
   - The drawing is assigned to an **internal reviewer** and marked as **Pending Review**.
   - Once reviewed, the drawing is either **approved** (and moves to the next step) or sent back with **revisions requested**.

3. **Final Approval**:
   - Once the drawing is approved by the **Engineering Manager** or **VP**, it is **released for production** and assigned a final version number (e.g., **Rev A**).

4. **Released to Manufacturing**:
   - The final drawing is stored in a **read-only format** (e.g., PDF) and made available to manufacturing, procurement, and external stakeholders as needed.

---

## **3. Dimensioning Standards, Tolerances, and Annotations**

Implementing proper dimensioning and tolerance practices ensures that designs are manufacturable, functional, and easily interpreted by clients and manufacturers. We’ll use **ISO**, **ANSI**, or **DIN standards** depending on the region or client requirements.

### **3.1. Dimensioning Standards**

- **Follow ISO, ANSI, or DIN Standards**:
  - Use the applicable standard for dimension placement, notation, and style.
  - **Dimensional Consistency**: Ensure all **critical dimensions** are included and visible from a standard view (Top, Front, Side).
  
- **Dimension Types**:
  - **Linear Dimensions**: Used for length, width, and height.
  - **Radial and Diameter Dimensions**: Applied for circular or arc features (e.g., sprockets, pulleys).
  - **Angular Dimensions**: Used for specifying the angle between two edges or lines.
  
- **Smart Dimensioning**:
  - Use **Smart Dimension** tools in SolidWorks to automatically adjust dimensions based on selected geometry.

### **3.2. Tolerances**

Tolerances ensure parts fit and function together properly, especially when dealing with mating components like **sprockets, rods, and belt segments**.

- **Geometric Dimensioning and Tolerancing (GD&T)**:
  - Use **GD&T** to define the acceptable range for features that affect the assembly or fit. Apply tolerances for:
    - **Flatness**, **parallelism**, **concentricity**, and **angularity**.
  
- **Fit Classifications**:
  - Specify the **fit type** (e.g., clearance, transition, interference) depending on the interaction between parts.
  
- **Tolerance Notation**:
  - Ensure tolerances are clearly stated for **critical dimensions**. For example:
    - `10.00 ± 0.05 mm` for linear dimensions.
    - Use **ISO 2768** or **ASME Y14.5** for tolerance tables.

### **3.3. Material and Special Process Notes**

- **Material Specification**:
  - Always specify the **material type** in the title block or drawing notes (e.g., **Polypropylene**, **Stainless Steel**).
  
- **Surface Finish and Special Process Notes**:
  - Include notes about **surface finishes**, **coatings**, or **special treatments** required (e.g., **heat-treated**, **corrosion-resistant coating**).

- **Welds or Assembly Notes**:
  - If applicable, add **welding symbols** or **assembly instructions** directly on the drawing.

---

## **4. Drafting Standards and Compliance**

### **4.1. ISO, ANSI, and DIN Standards**

**SolidWorks** and **AutoCAD LT** will both be configured to comply with **ISO**, **ANSI**, or **DIN standards**, depending on project and client requirements.

- **ISO Standards**:
  - Use **ISO 7200** for technical drawing layouts, title blocks, and basic dimensioning rules.
  - Apply **ISO 8015** for general tolerances, specifying default tolerance values for dimensions where specific tolerances aren’t defined.

- **ANSI Standards**:
  - Follow **ANSI Y14.5** for **GD&T** practices in the U.S. market, ensuring clear and accurate communication of tolerances and feature controls.
  
- **DIN Standards**:
  - For European clients, adhere to **DIN 406** for general technical drawing rules.

### **4.2. Compliance and Auditing**

- Regularly audit drawings to ensure that they meet **standards compliance** and contain all necessary engineering details.
- **ISO/ANSI/DIN certification** should be reflected in all deliverables (drawings, parts, and assemblies) to ensure compliance with industry standards.

---

## **5. Conclusion**

By implementing these design best practices, Habasit America will elevate the drafting process from simply creating visual representations to producing **functionally accurate**, **standards-compliant** technical drawings. This structured approach will improve communication with manufacturing, ensure design consistency, and meet external stakeholder expectations.