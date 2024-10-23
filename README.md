# Habasit.CAD.DrawingCodeManager

To modernize the drawing code cheat sheet you provided and convert it into a .NET 8 application, I suggest building a **desktop or web application** using **C#** with the following components:

### 1. **Architecture and App Type**
- **App Type**: This can be either a **WPF (Windows Presentation Foundation)** desktop app or an **ASP.NET Core MVC/Blazor** web application. Given that this is a tool for reducing user error, either approach can support a dynamic and user-friendly interface.
- **Architecture**: A **Clean Architecture** (also called onion or hexagonal architecture) ensures separation of concerns and scalability. This would involve:
  - **UI Layer**: Handles user input and interaction.
  - **Application Layer**: Contains business logic and use cases.
  - **Domain Layer**: Manages entities and rules.
  - **Infrastructure Layer**: Handles data access and external dependencies.

### 2. **Design Patterns**
- **MVVM (Model-View-ViewModel)** for WPF or **MVC** for web would be ideal for separating concerns, particularly with **Blazor** components (for modular UI) in the web app.
- **Repository Pattern**: For data access and separation between business logic and data.
- **Factory Pattern**: For handling complex object creation (e.g., different material or belt types).
- **Strategy Pattern**: Can be used for validating different belt or code configurations.
- **Command Pattern**: To handle complex user commands (e.g., generating unique codes or filtering belt data).

### 3. **Features**
- **Searchable and Filterable Data Table**: For quickly looking up specific codes and details.
- **Code Generation Tool**: Users can input specific parameters, and the app generates the correct configuration or cheat code.
- **Validation Rules**: Add logic to validate that selected combinations of options are valid based on business rules.
- **Export/Import Features**: Users can export generated data or load existing configurations for further processing.
- **Undo/Redo**: Allow users to revert mistakes or reapply a sequence of actions.
- **Responsive Design (if Web)**: Ensure the app works on multiple devices if you go with a web solution.

### 4. **Data Structures**
- **Code Structure**: Represent codes (like “TIS610BAON02500200120”) as an object with distinct properties for each part.
    ```csharp
    class DrawingCode
    {
        public string Code { get; set; }
        public string Material { get; set; }
        public string Color { get; set; }
        public double Size { get; set; }
        // Other properties based on the PDF structure.
    }
    ```
- **Lookup Tables/Enums**: For storing predefined values like **material types**, **sizes**, **flight types**, and **accessories**. This reduces human error by restricting user input to valid choices.
    ```csharp
    enum MaterialType { POM, Polyethylene, Polypropylene, PA, StainlessSteel }
    enum BeltType { FlatGrip, HighFriction, Ribbed }
    ```

### 5. **Logic and Business Rules**
- **Input Validation**: Rules that ensure users cannot combine incompatible materials, sizes, or other attributes. These validations can be abstracted into services that act at the domain level.
    ```csharp
    public class CodeValidator
    {
        public bool IsValid(DrawingCode code)
        {
            // Apply specific validation rules here.
        }
    }
    ```
- **Code Parsing**: Logic to break down the existing codes into their components based on predefined rules and patterns from the cheat sheet.
- **Real-Time Suggestions**: Use predictive logic to suggest configurations or corrections based on input.

### 6. **App Flow and User Input**
- **User Input**:
  - Users can either input code components manually or select them from dropdowns.
  - They should also be able to dynamically see code suggestions and validation results as they build.
- **Data Flow**:
  - Input data is passed to the **ViewModel (for WPF)** or **Controller/Service (for Web)**.
  - Business logic is applied via services to either validate or generate new configurations.
  - The UI updates dynamically, showing the results (generated codes, validation messages).
  
### 7. **User Interface (UI/UX)**
- **Dynamic Input Forms**: Use forms that adjust based on user input (e.g., if the user selects "Polypropylene," only relevant size options show up).
- **Autocomplete Fields**: Helps users quickly find matching parts or materials by typing part of the name.
- **Data Visualization**: If necessary, graphs or diagrams can help visualize how the belt/material selections might work in the physical system.

### 8. **Modern C# Language Features**
- **Records**: Use records to represent immutable data structures like a belt configuration:
    ```csharp
    public record BeltConfiguration(string Material, string Color, double Width, string SpecialFeatures);
    ```
- **Pattern Matching**: Implement pattern matching for validations and input processing.
    ```csharp
    if (belt is { Material: "POM", Width: > 2.0 }) {
        // Business logic
    }
    ```
- **Async/Await**: Handle any external API calls or file I/O (like exporting data) using modern asynchronous programming.

---

By building this app, users will have a more robust and error-proof system for selecting and generating drawing codes, reducing the risk of manual mistakes.