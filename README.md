# CMSDemo

1. Create a SQL database server with the name "CMSDemo"

2. In './DBScripts' there are two files.
   
	2.1 Run '1.CreateTables'
   
	2.2 Enter directory for the "DemoImages" folder in the @BaseDirectory variable at the final insert
   
	2.3 Run '2.PopulateTables'

3. Ensure the StockManagementAPI's defaultConnectionString points to the newly created table

4. Start the Visual Studio solution running on https

5. Start the Angular project by running 'ng serve' in the root directory.  

**PLEASE NOTE**

**Completed Items**:

  • Searchable list with pagination and sorting

  • List should include thumbnails of the primary image of the vehicle

  • Ability to update existing stock item’s information and images


**Partially Completed items** (back end exists, yet no Angular interface yet):

  • Ability to add new stock items

  • Ability to delete existing stock items along with its images


**Incompleted items**:

  • Login and user authentication (Dependencies have been installed and startup has been configured, yet the item system does not interact with it.)
  
