## **Drawing Requests - Standard Operating Procedure (SOP) for Plastic Modular Belting**

## **1. Purpose**
This **SOP** outlines the process for handling **drawing requests** related to the **plastic modular belt** product line at **Habasit America**. The document includes instructions for **2D** and **3D drawings**, utilizing software such as **AutoCAD LT**, **DraftSight**, **SolidWorks 2023**, and **PTC Creo with Windchill**. It also covers interactions with key **team members** and systems like **Epicor** for efficient and consistent delivery.

---

## **2. Workflow Overview**

### **2.1 Receive Drawing Request**
- **Review Email**: 
  - Open the SugarCRM link provided in the email.
  - Assign the task to yourself and change the status to **In Progress**.
  - Confirm the number of line items to determine how many drawings are required.

- **Determine Product Line**: 
  - Identify if the request pertains to **KVP** or **HabasitLink** (HabasitLink is typically easier to process).

- **Generate Drawing Number**: 
  - Use the specifications in the email.
  - Reference the **Drawing Code Chart** (available as an Excel file, PDF, or brochure).

---

## **3. Locating Existing Drawings**

### **3.1 Search the Server**
- **Server Location**: 
  - Search on `K:\Operations\Modular\Special Builds\`.
  - **Do Not Use** drawings from the **zObsolete** folder.
  
- **Search Criteria**: 
  - Use wildcards to locate drawings (e.g., `m2520wa*p0031*hdxx`).
  - Unless required, avoid existing drawings with:
    - Varying pitches.
    - Asymmetrical or uneven spacing.
    - "Falcon" configurations.

### **3.2 Check PTC Windchill**
- If the drawing is unavailable on the server, check **PTC Windchill**.
  - You may need to open **Related Objects** to view files in PTC Creo.

---

## **4. Review and Validate Drawing**

### **4.1 Usability Confirmation**
- Refer to drawing code specs and find the closest match to ease modification.
- **Check**:
  - Title block (use the **Attribute Editor**).
  - Static dimensions (ensure no overwritten values).
  - Overall format.
  - Dimension tolerance (refer to calculators if needed).
  - **2D or 3D drawing** appropriateness.
  - Modification potential.
  - Revision number (follow company conventions).
  - Material and color specifications.

---

## **5. Create or Modify Drawing**

### **5.1 Determine Drawing Type**
- **2D or 3D**: Assess if the request requires 2D or 3D based on complexity and availability of existing resources.

### **5.2 3D Drawing**
- If a 3D model is needed:
  - Use **SolidWorks** or **PTC Creo**.
  - Confirm required assembly detail (e.g., hardware, rods).
  - Part modeling:
    - Check if there’s an existing **STEP**, SolidWorks, or Creo part file.
  - Assembly modeling in SolidWorks:
    - Use **STEP files** for solid-body modeling when necessary.
    - Utilize **magnetic mates** and phantom parts to maintain assembly constraints.
    - Analyze seam placement to optimize assembly.
    - Use standard parts whenever possible and minimize custom modifications.

### **5.3 2D Drawing**
- If a 2D drawing is needed:
  - Use **AutoCAD LT** or **DraftSight**.
  - **Purge** the file to remove unnecessary elements.
  - Ensure **no bill of materials** (BOM) is included unless required.
  - Align views (top, side, and accessory detail views) for clarity.
  - Update the **title block** with:
    - Drawn by.
    - Remove scale (if not needed).
    - Drawn date.
    - Drawing number.
    - Revision number.
    - Title lines (including indent, rod material, and notes on accessories).

---

## **6. Place Accessories and Special Processes**

### **6.1 Belt Accessories**
- Place accessories (e.g., rollers, hold-down tabs) as per the requirements.

### **6.2 Special Process Components**
- Review belt standards and cut widths:
  - Refer to the **Base Std and Cut Width** spreadsheet (`Base Std and Cut Width 9-8-09.xlsx`).
  - Use the **HabasitLINK® Plastic Modular Belts** PDF for hold-down offsets and notch specs (page 14).

### **6.3 Final Dimensioning**
- Ensure the final dimensions match customer specifications.
- Review part configuration (“brick lay”) and seams for proper alignment.
- Consider waste reduction strategies for the complete assembly.

---

## **7. Collaborate and Seek Assistance**

### **7.1 Key Team Members**
- **Product Specialists**:
  - **Amesto Rodriguez**: Over 20 years of experience, previously worked for KVP. Offers product training videos.
  - **Jeremy McDonough**: Available for questions on product lines.
  - **Jeff Butler (Remote)**: Application Engineer and former KVP employee. Contact for missing 3D models or legacy parts.
  - **Tamika**: Marketing, formerly involved in technical drawings. Reach out for drawing-related questions.

- **Machine Shop**:
  - **Andrew**: Certified machinist for machining-related queries.
  - **Quey**: Handles the **Nesting program**.
  - **Bob**: KVP expert with knowledge on old and new part numbers, available before 1 o'clock.
  - For sprocket questions, consult Bob or Andrew.

- **Sample Room Access**:
  - **Frita**: Can provide access to the 6 x 6 sample room for fabric, plastic, posters, brochures, or material samples.

- **Production Control**:
  - **Layloff**, **Tom**, **Yanni** can assist with creating Bill of Materials (BOM).
  
- **Quality Control**:
  - **Michael** can assist with final drawing quality checks.

- **Epicor ERP Reports**:
  - **Nick Graff**: Data analyst for ERP-related inquiries.
  - **Brad**: Epicor expert and configurator creator.

---

## **8. Final Steps**

### **8.1 File Saving**
- Save the drawing as a **PDF** with the drawing number as the file name.
  - Ensure landscape orientation and proper path definition.

### **8.2 Printing**
- Print the drawing for review on **Kyocera Takalfa 5003i KX** (IP: `172.22.43.39`).

### **8.3 Error Handling**
- If a mistake is found, move the obsolete drawing to the **zObsolete** folder.

---

## **9. Completing the SugarCRM Task**

### **9.1 Task Completion**
- **Add the Drawing Name** to the subject and description in the SugarCRM task.
- **Attach the PDF** of the drawing to the task.
- Tag the request with the appropriate difficulty level (D1, D2, etc.) based on the complexity and number of drawings.
- Communicate with the sender if any deviations or recommendations are needed.
- Change the task status to **Complete**.

### **9.2 Time Tracking**
- Review and log the time taken to complete the drawing request for tracking purposes.

---

## **10. Additional Resources**

- **Parts List**: Refer to `all-purch-parts-2023-02-14.xlsx` for parts references.
- **Base Std and Cut Width Sheet**: Ensure correct standards are followed (`Base Std and Cut Width 9-8-09.xlsx`).
- **HabasitLINK® Plastic Modular Belts PDF**: Review belt offsets, notch specs, and other details for special processing (page 14).
- **Module Dimension Tool**: Refer to `0-Module-Dimension.xlsx` - MSeries Belts
- **Code List PDP**: Refer to `Code List PDP (Plastics).xlsx` - Windchill Search criteria

---

This SOP ensures a structured, consistent approach to managing drawing requests, collaborating with key team members, and delivering accurate technical drawings for the plastic modular belt product line. Follow the steps and consult the necessary resources to complete your tasks efficiently and effectively.