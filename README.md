# Reflection Examples in .Net
## Reflection Examples in .Net

Reflection basic examples.

- **Technologies**
    - **.Net Framework 4.5**
    - **Visual Studio 2017** 

- **Capabilities**
	- Field
	- Property
	- Class
	

- **Details**
	- Reads class definitions from json file(my_classes.json).
	- Builds objects from defined classes.
	- Creates properties and fields.
	- Has mock data (AllTypesData, MockDataStatics, Category, Product)

- **Project Structure**
    - JsonReaderHelpers
        - ClassField.cs
        - ClassJson.cs
        - ClassJsonFile.cs
        - ClassJsonHelper.cs
    - MockData 
        - AllTypesData.cs
        - Category.cs
        - MockDataStatics.cs
        - Product.cs
    - ReflectionHelpers 
        - MyObjectBuilder.cs
        - ReflectionHelper.cs