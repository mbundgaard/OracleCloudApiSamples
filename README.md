# Oracle FBGBU API OAuth2 Sample

This project shows a basic implementation of the authentication necessary to use the new cloud API's from Oracle F&B.

The source code has been written purely for educational purposes and should not be used in production without proper adjustment.

Feel free to comment here on GitHub or reach out at mb@muneris.dk

------

These three cloud APIs from Oracle give access to the three main components in their F&B ecosystem:

- Enterprise Management Console (EMC) stores all Simphony configurations. Every Simphony workstation gets its configuration from here. The new Configuration and Content API provides restful access to almost all configuration aspects. We still have access to the old import/export API, but the new one provides access to much more configuration and is the preferred one from now on.

- Reporting and Analytics (R&A) is Simphony's primary reporting tool. On each property, the CAPS (Check And Posting Service) sends all check details and totals to this service. Previously, it was only possible to schedule static exports in the portal. Now, with the Business Intelligence API, we can access data in a restful way.

- Simphony Transaction Services (TS, STS or POS API) is the API used to post new orders into Simphony. It also provides access to certain critical aspects of the configuration (mainly items, payments, discounts and so on) to facilitate a custom online ordering solution. Previously the API was hosted in Simphony as a legacy asmx service without authentication or authorization. Different partners made many different solutions to solve this (including Muneris), they all included some sort of reverse proxy or ssl tunneling. Now, with the STS gen2 API, we can access Simphony from the cloud securely and efficiently.



If you want to know more about the three cloud API's, the documentation can be found here:

* Configuration and Content API Guide
https://docs.oracle.com/en/industries/food-beverage/simphony/ccapi


* Simphony Transaction Services Gen2 API Guide
https://docs.oracle.com/en/industries/food-beverage/simphony/omsstsg2api


* Business Intelligence API Guide
https://docs.oracle.com/en/industries/food-beverage/back-office/20.1/biapi
