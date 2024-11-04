## **SolidWorks 2023 - Managing Part Metadata**

In **SolidWorks 2023**, the equivalent to Autodesk Inventor's **iProperties** for managing part metadata is the **Custom Properties** feature. This allows you to store all relevant information about individual parts, assemblies, and drawings, which can then automatically populate the **Bill of Materials (BOM)** and other documents.

Here's how to effectively store and manage your part metadata in SolidWorks:

### **1. Setting Up Custom Properties**

#### **1.1 Accessing Custom Properties**
1. **Open Your Part or Assembly**:
   - Start by opening the part or assembly file for which you want to set up the metadata.

2. **Open the Custom Properties Dialog**:
   - Go to **File > Properties** or right-click on the part/assembly in the **FeatureManager Design Tree** and select **Properties**.

#### **1.2 Adding Custom Properties**
- In the **Properties** dialog, navigate to the **Custom** tab. Here, you can enter the following types of data:

1. **Part Number**: Assign a unique identifier for each part.
   - **Name**: Enter "Part Number".
   - **Type**: Select **Text**.
   - **Value**: Enter the actual part number (e.g., `M2520-PB-Rod`).

2. **Material**: Specify the material of the part.
   - **Name**: Enter "Material".
   - **Type**: Select **Text**.
   - **Value**: Enter the material type (e.g., `Polypropylene`, `Acetal`).

3. **Size**: Provide size specifications.
   - **Name**: Enter "Size".
   - **Type**: Select **Text**.
   - **Value**: Enter the dimensions or other size identifiers (e.g., `2.0m length`).

4. **Special Process Notes**: Include any manufacturing or processing notes.
   - **Name**: Enter "Special Process".
   - **Type**: Select **Text**.
   - **Value**: Enter any specific instructions (e.g., `Heat treated`).

5. **Quantity**: Indicate how many of this part are used in an assembly.
   - **Name**: Enter "Quantity".
   - **Type**: Select **Number**.
   - **Value**: Set a numeric value as needed.

6. **Description**: Provide a brief description of the part.
   - **Name**: Enter "Description".
   - **Type**: Select **Text**.
   - **Value**: Enter a brief summary of the part's function (e.g., `Belt Rod for modular assembly`).

7. **Stock Number**: If applicable, enter the stock number for inventory tracking.
   - **Name**: Enter "Stock Number".
   - **Type**: Select **Text**.
   - **Value**: Enter the stock number (e.g., `10023`).

### **2. Linking Custom Properties to the BOM**

After setting the custom properties for your parts, these properties can be automatically pulled into the **Bill of Materials (BOM)**.

#### **2.1 Creating the BOM**
1. **Insert BOM in a Drawing**:
   - Open the drawing file where you want to include the BOM.
   - Go to **Insert > Tables > Bill of Materials**.

2. **Select the Assembly**:
   - Choose the assembly from which the BOM will be generated.

3. **Configuring BOM Columns**:
   - In the BOM dialog, you can select which properties to include in your BOM table. Common columns are:
     - Part Number
     - Description
     - Material
     - Quantity
     - Any other custom properties you've created (e.g., Size, Special Process, Stock Number).
   - You can customize the BOM table to fit your needs by right-clicking on the table and choosing **Insert Column** to add additional properties.

### **3. Using Templates for Custom Properties**

If you frequently create parts with the same metadata fields, you can set up a template with predefined custom properties.

#### **3.1 Creating a Part Template**
1. **Create a New Part**:
   - Open a new part file and define your custom properties as described earlier.

2. **Save as Template**:
   - Go to **File > Save As** and select **Part Template (*.prtdot)** from the dropdown.
   - Save the template in a directory where you can easily access it for future parts.

3. **Using the Template**:
   - When creating new parts, start from your custom template to ensure that your desired metadata fields are readily available.

### **4. Best Practices for Managing Metadata**

- **Consistency**: Use consistent naming conventions for your properties to avoid confusion. This helps when you're looking for specific attributes in your BOM.
- **Regular Updates**: Make sure to update your custom properties whenever changes are made to the part to keep your BOM accurate.
- **Template Usage**: Take advantage of part templates to maintain a standard workflow for entering metadata across similar components.

### **Conclusion**

By effectively using **Custom Properties** in **SolidWorks 2023**, you can ensure that all necessary metadata (part number, material, size, notes, etc.) is properly stored and automatically populated in your BOM. This streamlines your workflow and enhances the accuracy and efficiency of your designs, allowing you to generate detailed and well-organized documentation for your modular plastic belt assemblies.