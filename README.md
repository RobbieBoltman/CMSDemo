# CMSDemo

1. Create a SQL database server with the name "CMSDemo"

2. In './DBScripts' there are two files. 
	2.1 Run 1.CreateTables
	2.2 Enter directory for the "DemoImages" folder in the @BaseDirectory variable at the final insert
	2.3 Run PopulateTables

3. Ensure the StockManagementAPI's defaultConnectionString points to the newly created table

4. Start the Visual Studio solution running on https

5. Start the Angular project by running 'ng serve' in the root directory.