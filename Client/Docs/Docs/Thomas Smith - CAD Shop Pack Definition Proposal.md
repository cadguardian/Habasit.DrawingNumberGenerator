# Standardizing CAD Drawings
> 👨🏿‍🦲 Thomas Smith II | 💻 CAD Drafter | 🌍 Habasit America - Suwanee, GA | 📅 10-15-2024

Below is an outline for standardizing CAD drawing shop packs, considering the complexity of the plastic modular belting product line at Habasit America and addressing issues like errors, rework, miscommunication, waste, and employee frustration. I organized doc by sections to ensure completeness and clarity across all stages of the process.

---

## Shop Pack Definition
> Defines the purpose of the shop pack as a comprehensive set of documents required for product fabrication.

A **Shop Pack** is a comprehensive collection of engineering and manufacturing documents provided to production for the accurate fabrication of a product. This includes drawings, bills of materials, specifications, and legal and customer information necessary for ensuring the product meets customer requirements and adheres to production standards.

---

## Transmittal

> Lists key project information, ensuring that all team members and stakeholders are aligned on the product details.
- **Project Name**
- **Quote Number**
- **Date**
- **Revision Number**
- **Sales Order Number**
- **Customer Purchase Order Number**

---

<div style="page-break-after: always"></div>

## Prepared For (Customer)
> Provides the customer's contact details to ensure clear communication and product delivery coordination.
- **Customer Name**
- **Address**
- **City, State, Zip**
- **Phone Number**
- **Attention (Contact Person)**

---

## Submitted By (Habasit America, Suwanee)
>Identifies the Habasit team responsible for the project, including key engineers and estimators.
- **Habasit America**
- **Address**
- **City, State, Zip**
- **Phone Number**
- **Estimator (CSR)**
- **Application Engineer**
- **Engineering Manager (if applicable)**

---

## Submittal Table
Summarizes the drawings and documents included in the shop pack for easy reference and organization.
- **Number of Copies**
- **Drawing Numbers Included**
- **Submittal Type**: (Elevation Sheet or Shop)
- **Drawing Status** (Preliminary, Released for Construction, As-Built, etc.)
- **List of Associated Deliverables**: (Manufacturing Instructions, Test/Inspection Procedures, etc.)

---

<div style="page-break-after: always"></div>

## Legal Section
>Contains approval signatures and legal disclaimers to document formal approval and clarify lead time conditions.
- **Transmittal Description**
- **Approval Signature (Customer or Project Manager)**
- **Signed Date**
- **Approval Status**:
  - APPROVED AS NOTED WITH MODIFICATIONS
  - APPROVED WITHOUT CHANGES
  - REJECTED (Reasons Specified)
- **Sales Rep**
- **Lead Time Clause**: (Example: "Quoted lead times do not start until Habasit receives final approved drawings with field-verified dimensions.")

---

## Drawing Sheets (General Requirements for All Sheets)
> Establishes the necessary details and format for all drawings, including revision history, title block, and tolerances.
- **Title Block Information**
  - Drawing Number
  - Revision Number
  - Sheet Name
  - Drafter Name
  - Date Created/Modified
  - Drawing Scale
  - Tolerance Information
  - Notes Block (General production notes, inspection criteria, etc.)
  - Material Specifications (where applicable)

---

<div style="page-break-after: always"></div>

## Elevation Sheet (Full Assembly)
> Displays the complete assembly with relevant views and a Bill of Material (BoM) for the entire product.
- **Views**: Front, Top, Side, Detailed Views
- **Full Bill of Material**: Including part numbers, descriptions, quantities, and materials
- **Module Balloons**: (Name, part number, quantity, with clear linkages to the Bill of Material)
- **Assembly Notes**: Specific instructions for assembly sequence or critical steps

---

## Exploded Elevation Modules Sheet
>Shows exploded views of module assemblies with part numbers and quantities to clarify assembly.
- **Part Balloons**: (Item number, module occurrence number, quantity)
- **Attachment Detail Views**: Include fasteners, brackets, and other joining methods
- **Interlock Detail Views**: Specific details on how modules interlock, ensuring correct alignment and assembly

---

## Module Sub-Assembly Sheets
> Provides detailed views of individual modules, showing how components fit together in sub-assemblies.
- **Views**: Front, Top, Side, Detailed Views
- **Exploded Breakout Views**: 
  - One relative to the main elevation
  - One focusing on the sub-components of each module
- **Assembly Steps**: Step-by-step instructions for sub-assembly if non-standard

---

<div style="page-break-after: always"></div>

## Module Sub-Assembly Exploded Sheet
>  Includes exploded views with part and quantity balloons, focusing on sub-component details.
- **Balloons**: (Item number, stock number, quantity)
- **Attachment Detail Views**: Specific joinery details for components
- **Rod Insertion Point Detail View**: Clear representation of insertion points to avoid misassembly

---

## Part Drawing/Specification Sheet
> Details individual parts with dimensions and specifications, pulled from databases or lookup tables.
- **Views**: Front, Top, Side, Detailed Views
- **Specification Table**: Automatically generated or populated via a lookup table or PDF extractor tool. This table should include:
  - Material Type
  - Dimensions
  - Tolerances
  - Surface Finish
  - Special Treatments (Coatings, Heat Treatments, etc.)
- **Special Process Table**: Any additional processes required for part fabrication (e.g., machining, welding, assembly instructions)

---

## Waste Table (Drop Analysis)
> Tracks material usage and waste to optimize efficiency and reduce production scrap.
- **Item Number**
- **Stock Number**
- **Stock Part Length**
- **Cut Length** (Waste Calculation)
- **Waste Sum Total**: In linear feet, inches, or millimeters
- **Total Yield Percentage**: A metric to track material utilization efficiency

---


## Title Block
> Lists essential project details, drawing information, and revision history for every drawing included in the shop pack.
- **Submittal By**
- **Submittal Checked By**
- **Shop Pack Created By**
- **Shop Pack Checked By**
- **Submittal Date**
- **Shop Pack Date**
- **Revision Date**
- **Customer Name**
- **Customer Address**
- **Customer Contact (Name, Phone)**
- **Drawing Status**
- **Sales Order Number**
- **Drawing Creation Date**
- **Job Name and Address**
- **Product Information**
- **Estimator**
- **Application Engineer**
- **Drawing Size (e.g., 18x24 inches)**
- **Scale**
- **Line Item Numbers and Quantities**
- **Sheet Count (Sheet x of y)**
- **File Location and Date Modified**

---

<div style="page-break-after: always"></div>

## Bill of Material (BoM)
> Hierarchical breakdown of all parts and materials required for the product, including quantities and specifications.
- **Hierarchy Tree Style**
  - References sub-assemblies and individual components
  - **Item Number**
  - **Stock Number**
  - **Description**
  - **Quantity**
  - **Material Specification**
  - **Color/Finish**
  - **Special Process Requirements**: Any treatments or processing beyond standard stock
  - **Part Source**: Manufacturer details or vendor for outsourced components

---

## Production-Specific Notes (Optional but Recommended)
> Highlights critical production instructions, quality control measures, and special assembly requirements.
- **Weld Symbols** (where applicable)
- **Fastener Callouts**: Ensure all fasteners are specified, including type, size, and material
- **Assembly Sequence Notes**: If required for modules that cannot be assembled out of sequence
- **Critical Inspection Points**: Key points where assembly needs validation to avoid errors
- **Quality Control Procedures**: Any specific QC procedures for the product in production
- **Field Verification Requirement**: Specify if field measurements need to be verified prior to production.

---

<div style="page-break-after: always"></div>

## Additional Suggestions:
### 1. **Error Tracking and Prevention**:
>Identifies past errors and outlines preventive measures to minimize rework and improve efficiency.
   - Integrate a section where common assembly errors are documented and linked to preventive actions. This could be a "Lessons Learned" part of the shop pack that identifies past rework causes and how the updated process addresses them.
  
### 2. **Standard Operating Procedures (SOPs)**:
> Provides or references SOPs relevant to the fabrication and assembly processes.
   - Attach relevant SOPs for fabrication processes or reference document numbers where production can access them.

### 3. **Automation and Integration**:
> Suggests automated data population to reduce manual errors and streamline the creation of shop packs.
   - Consider automating the population of standard fields, such as BoM, spec tables, and notes from PTC Windchill or SolidWorks PDM to reduce human error during the creation of shop packs.
  
---

This strategy covers essential details, adds critical checks and processes to minimize errors and miscommunication, and ensures traceability and standardization across all drawing sheets.